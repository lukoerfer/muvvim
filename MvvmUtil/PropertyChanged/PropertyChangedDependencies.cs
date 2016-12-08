using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtil.PropertyChanged
{
    public class PropertyChangedDependencies
    {
        private INotifyPropertyChanged NotifyInstance;
        private IRaisePropertyChanged RaiseInstance;

        private object DependencyLock;
        private List<PropertyChangedDependency> Dependencies;

        public PropertyChangedDependencies(object instance) 
            : this((INotifyPropertyChanged)instance, (IRaisePropertyChanged)instance) { }

        public PropertyChangedDependencies(INotifyPropertyChanged notify, IRaisePropertyChanged raise)
        {
            this.DependencyLock = new object();
            this.Dependencies = new List<PropertyChangedDependency>();
            this.NotifyInstance = notify;
            this.RaiseInstance = raise;
            this.NotifyInstance.PropertyChanged += this.HandlePropertyChanged;
        }

        public void AddDependency(string property, string dependsOn)
        {
            lock (this.DependencyLock)
            {
                this.Dependencies.Add(new PropertyChangedDependency(property, dependsOn));
            }
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            lock (this.DependencyLock)
            {
                this.Dependencies
                    .Where(dependency => dependency.DependsOn.Equals(args.PropertyName))
                    .ToList()
                    .ForEach(dependency => this.RaisePropertyChanged(dependency.Property));
            }
        }

        private void RaisePropertyChanged(string property)
        {
            this.RaiseInstance.RaisePropertyChanged(new PropertyChangedEventArgs(property));
        }
    }

    internal class PropertyChangedDependency
    {
        public string Property { get; set; }
        public string DependsOn { get; set; }

        public PropertyChangedDependency(string property, string dependsOn)
        {
            this.Property = property;
            this.DependsOn = dependsOn;
        }
    }
}
