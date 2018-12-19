using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

using MvvmUtil.Util;

namespace MvvmUtil.PropertyChanged
{
    /// <summary>
    /// Provides an ObservableCollection which routes the PropertyChanged events of its items
    /// </summary>
    /// <typeparam name="T">The item type of this ObservableCollection</typeparam>
    public class ItemObservableCollection<T> : ObservableCollection<T>
    {
        private const string ItemPropertyKey = "Item[]";

        /// <summary>
        /// Creates a new ItemObservableCollection
        /// </summary>
        public ItemObservableCollection() : base() { }

        /// <summary>
        /// Creates a new ItemObservableCollection with initial items
        /// </summary>
        /// <param name="initialItems">The initial items for the collection</param>
        public ItemObservableCollection(IEnumerable<T> initialItems) : base(initialItems)
        {
            Register(initialItems);
        }

        /// <summary>
        /// Creates a new ItemObservableCollection with start items
        /// </summary>
        /// <param name="initialItems">The initial items for the collection</param>
        public ItemObservableCollection(List<T> initialItems) : base(initialItems)
        {
            Register(initialItems);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                Register(args.NewItems.OfType<T>());
            }
            if (args.OldItems != null)
            {
                Unregister(args.OldItems.OfType<T>());
            }
            base.OnCollectionChanged(args);
        }

        private void Register(IEnumerable<T> items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                item.PropertyChanged += ItemPropertyChanged;
            }
        }

        private void Unregister(IEnumerable<T> items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            string itemPropertyName = string.Join(Separators.Point, ItemPropertyKey, args.PropertyName);
            base.OnPropertyChanged(new PropertyChangedEventArgs(itemPropertyName));
        }
    }
}
