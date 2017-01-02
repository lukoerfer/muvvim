using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtil.PropertyChanged
{
    public class ItemObservableCollection<T> : ObservableCollection<T>
    {
        private const string Separator = ".";
        private const string ItemPropertyKey = "Item[]";

        public ItemObservableCollection() : base() { }
        public ItemObservableCollection(IEnumerable<T> startItems) : base(startItems)
        {
            this.Register(startItems);
        }
        public ItemObservableCollection(List<T> startItems) : base(startItems)
        {
            this.Register(startItems);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                this.Register(args.NewItems.OfType<T>());
            }
            if (args.OldItems != null)
            {
                this.Unregister(args.OldItems.OfType<T>());
            }
            base.OnCollectionChanged(args);
        }

        private void Register(IEnumerable<T> items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                item.PropertyChanged += this.ItemPropertyChanged;
            }
        }

        private void Unregister(IEnumerable<T> items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                item.PropertyChanged -= this.ItemPropertyChanged;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            string itemPropertyName = string.Join(Separator, ItemPropertyKey, args.PropertyName);
            base.OnPropertyChanged(new PropertyChangedEventArgs(itemPropertyName));
        }
    }
}
