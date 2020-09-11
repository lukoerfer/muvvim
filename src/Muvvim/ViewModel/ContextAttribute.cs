using System;
using System.Collections.Generic;
using System.Linq;

namespace Muvvim.ViewModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ContextAttribute : Attribute
    {
        public List<string> Contexts { get; private set; }

        public ContextAttribute(params string[] contexts) : base()
        {
            Contexts = contexts.ToList();
        }

        public ContextAttribute(params object[] contexts) : base()
        {
            Contexts = contexts.Select(context => context.ToString()).ToList();
        }

        internal static uint Evaluate(Type type, string context)
        {
            return (uint)(type.GetCustomAttributes(typeof(ContextAttribute), false)
                .Cast<ContextAttribute>()
                .FirstOrDefault()?.Contexts.IndexOf(context) ?? -1);
        }
    }
}
