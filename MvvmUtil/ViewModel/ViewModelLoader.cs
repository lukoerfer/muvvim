using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using MvvmUtil.Util;

namespace MvvmUtil.ViewModel
{
    public class ViewModelLoader
    {
        private HashSet<Type> ViewModelTypes;

        public ViewModelLoader(params Type[] types)
        {
            ViewModelTypes = types
                .Assert(type => IsViewModelType(type))
                .ToHashSet();
        }

        private ViewModelLoader() { }

        public static ViewModelLoader CollectFrom(Assembly assembly, params string[] namespaces)
        {
            return CollectFrom(new[] { assembly }, namespaces);
        }

        public static ViewModelLoader CollectFrom(Assembly[] assemblies, params string[] namespaces)
        {
            bool anyNamespace = namespaces.Length == 0;
            var viewModelTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass)
                .Where(type => anyNamespace || 
                    namespaces.Any(@namespace => (type.Namespace ?? string.Empty).StartsWith(@namespace)))
                .Where(type => IsViewModelType(type))
                .ToHashSet();
            return new ViewModelLoader() { ViewModelTypes = viewModelTypes };
        }

        public object CreateFor(object model, string context = null)
        {
            Type viewModelType = GetViewModelType(model.GetType(), context);
            return Activator.CreateInstance(viewModelType, model);
        }

        public object CreateFor(object model, object context)
        {
            return CreateFor(model, context.ToString());
        }

        /*
        public object CreateViewModel<Model>(string context = null)
        {
            Type viewModelType = GetViewModelType(typeof(Model), context);
            return Activator.CreateInstance(viewModelType);
        }

        public object CreateViewModel<Model>(object context)
        {
            return CreateViewModel<Model>(context.ToString());
        }
        */

        private Type GetViewModelType(Type modelTypeTarget, string context)
        {
            return ViewModelTypes
                .Where(viewModelType => GetModelType(viewModelType).IsAssignableFrom(modelTypeTarget))
                .OrderBy(viewModelType => ContextAttribute.Evaluate(viewModelType, context))
                .ThenBy(viewModelType => GetInheritanceDistance(modelTypeTarget, GetModelType(viewModelType)))
                .First();
        }

        private static bool IsViewModelType(Type type)
        {
            for (; type != null; type = type.BaseType)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(ViewModel<>)))
                {
                    return true;
                }
            }
            return false;
        }

        private static Type GetModelType(Type type)
        {
            for (; type != null; type = type.BaseType)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(ViewModel<>)))
                {
                    return type.GetGenericArguments().Single();
                }
            }
            return null;
        }

        private static uint GetInheritanceDistance(Type type, Type parent)
        {
            uint i = 0;
            for (; type != null; type = type.BaseType)
            {
                if (type.Equals(parent))
                {
                    return i;
                }
                i++;
            }
            return uint.MaxValue;
        }

    }
}
