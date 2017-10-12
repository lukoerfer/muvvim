using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUtil.ViewModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewModelContextAttribute : Attribute
    {
        public List<string> Contexts { get; private set; }

        public ViewModelContextAttribute(params string[] contexts) : base()
        {
            this.Contexts = contexts.ToList();
        }

        public ViewModelContextAttribute(params object[] contexts) : base()
        {
            this.Contexts = contexts.Select(context => context.ToString()).ToList();
        }

        internal static uint Evaluate(Type type, string context)
        {
            return (uint)(type.GetCustomAttributes(typeof(ViewModelContextAttribute), false)
                .Cast<ViewModelContextAttribute>()
                .FirstOrDefault()?.Contexts.IndexOf(context) ?? -1);
        }
    }
}
