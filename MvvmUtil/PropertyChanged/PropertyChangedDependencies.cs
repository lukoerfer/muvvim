using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MvvmUtil.PropertyChanged
{
    /// <summary>
    /// Provides the possibility to register property changed dependencies
    /// </summary>
    public class PropertyChangedDependencies
    {
        private readonly INotifyPropertyChanged NotifyInstance;
        private readonly IRaisePropertyChanged RaiseInstance;

        private readonly object RuleLock;
        private readonly List<DependencyRule> Rules;

        /// <summary>
        /// Creates a new PropertyChanged dependency register
        /// </summary>
        /// <param name="instance">
        /// The instance which both raises and notifies about PropertyChanged events,
        /// must be both INotifyPropertyChanged and IRaisePropertyChanged
        /// </param>
        public PropertyChangedDependencies(object instance)
            : this((INotifyPropertyChanged)instance, (IRaisePropertyChanged)instance) { }

        /// <summary>
        /// Creates a new PropertyChanged dependency register
        /// </summary>
        /// <param name="notifyInstance">The instance which notifies about property changes</param>
        /// <param name="raiseInstance">The instance which raises property change events</param>
        public PropertyChangedDependencies(INotifyPropertyChanged notifyInstance, IRaisePropertyChanged raiseInstance)
        {
            RuleLock = new object();
            Rules = new List<DependencyRule>();
            NotifyInstance = notifyInstance;
            RaiseInstance = raiseInstance;
            NotifyInstance.PropertyChanged += HandlePropertyChanged;
        }

        /// <summary>
        /// Registers a new PropertyChanged dependency
        /// </summary>
        /// <param name="property">The name of the depending property</param>
        /// <param name="dependency">The name of the causing property</param>
        public void RegisterDependency(string property, string dependency)
        {
            lock (RuleLock)
            {
                Rules.Add(new DependencyRule(property, dependency));
                if (AnyLoop(property, dependency))
                {
                    throw new InvalidOperationException($"Found dependency loop between {property} and {dependency}!");
                }
            }
        }

        private bool AnyLoop(string property, string dependency)
        {
            if (property.Equals(dependency)) return true;
            return Rules.Where(rule => rule.Property.Equals(dependency))
                .Select(rule => rule.Dependency).Any(dep => AnyLoop(property, dep));
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            lock (RuleLock)
            {
                Rules.Where(dependency => dependency.Dependency.Equals(args.PropertyName))
                    .Select(dependency => dependency.Property)
                    .ToList().ForEach(RaisePropertyChanged);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            RaiseInstance.RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class DependencyRule
    {
        public string Property { get; set; }
        public string Dependency { get; set; }

        public DependencyRule(string property, string dependency)
        {
            Property = property;
            Dependency = dependency;
        }
    }
}
