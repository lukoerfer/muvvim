using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using MvvmUtil.Util;

namespace MvvmUtil.Binding
{
    /// <summary>
    /// Provides a binding in the data context of a parent ItemsControl
    /// </summary>
    public class ParentBinding : System.Windows.Data.Binding
    {
        /// <summary>
        /// Creates a new binding in the data context of the elements parent ItemsControl
        /// </summary>
        /// <param name="path">The property path in the parent DataContext</param>
        public ParentBinding(string path) : base(path)
        {
            this.ApplyParentContext();
        }

        private void ApplyParentContext()
        {
            // Set the path relative to the DataContext
            this.Path = new PropertyPath(string.Join(Separators.Point, "DataContext", this.Path?.Path ?? string.Empty));
            // Set the parent ItemsControl as relative source
            this.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ItemsControl), 1);
        }
    }
}
