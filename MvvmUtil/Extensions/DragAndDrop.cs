using MvvmUtil.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MvvmUtil.Extensions
{
    public static class DragAndDrop
    {
        private static Dictionary<FrameworkElement, Point> DragPreparation;

        static DragAndDrop()
        {
            DragPreparation = new Dictionary<FrameworkElement, Point>();
        }

        private const string AllowDragPropertyKey = "AllowDrag";

        public static DependencyProperty AllowDragProperty =
            DependencyProperty.RegisterAttached(AllowDragPropertyKey, typeof(bool), typeof(DragAndDrop),
                new FrameworkPropertyMetadata(false, AllowDragChanged));

        private const string DragDataPropertyKey = "DragData";

        public static DependencyProperty DragDataProperty =
            DependencyProperty.RegisterAttached(DragDataPropertyKey, typeof(object), typeof(DragAndDrop));

        private const string OnDropPropertyKey = "OnDrop";

        public static DependencyProperty OnDropProperty = 
            DependencyProperty.RegisterAttached(OnDropPropertyKey, typeof(ICommand), typeof(DragAndDrop),
                new FrameworkPropertyMetadata(null, OnDropChanged));

        public static void SetAllowDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowDragProperty, value);
        }

        public static void SetDragData(DependencyObject obj, object value)
        {
            obj.SetValue(DragDataProperty, value);
        }

        public static void SetOnDrop(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OnDropProperty, value);
        }

        private static void AllowDragChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            if ((bool)args.NewValue)
            {
                element.PreviewMouseLeftButtonDown += OnDragPrepare;
                element.PreviewMouseLeftButtonUp += OnDragDiscard;
            }
            else
            {
                element.PreviewMouseLeftButtonDown -= OnDragPrepare;
                element.PreviewMouseLeftButtonUp -= OnDragDiscard;
            }
        }

        private static void OnDropChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            if (args.NewValue != null)
            {
                element.AllowDrop = true;
                element.PreviewDrop += OnDrop;
            }
            else
            {
                element.AllowDrop = false;
                element.PreviewDrop -= OnDrop;
            }
        }

        private static void OnDragPrepare(object sender, MouseButtonEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            DragPreparation[element] = args.GetPosition(null);
            element.PreviewMouseMove += OnDragStart;
        }

        private static void OnDragDiscard(object sender, MouseButtonEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.PreviewMouseMove -= OnDragStart;
            DragPreparation.Remove(element);
        }

        private static void OnDragStart(object sender, MouseEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            // Calculate the drag distance
            Vector distance = (DragPreparation[element] - args.GetPosition(null)).Abs();
            // Check for the minimum drag distance
            if (distance.X >= SystemParameters.MinimumHorizontalDragDistance ||
                distance.Y >= SystemParameters.MinimumVerticalDragDistance)
            {
                // Stop the preparation
                element.PreviewMouseMove -= OnDragStart;
                DragPreparation.Remove(element);
                // Pack the data
                object data = element.GetValue(DragDataProperty) ?? element.DataContext;
                DataObject dataObj = new DataObject(typeof(object), data);
                // Start the drag and drop operation
                DragDrop.DoDragDrop(element, dataObj, DragDropEffects.All);
            }
        }

        private static void OnDrop(object sender, DragEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            // Extract the dragged data
            object data = args.Data.GetData(typeof(object));
            // Extract the drop handler
            ICommand handler = (ICommand)element.GetValue(OnDropProperty);
            if (handler != null && handler.CanExecute(data))
            {
                handler.Execute(data);
            }
        }
    }
}
