using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using MvvmUtil.Util;

namespace MvvmUtil.Extensions
{
    /// <summary>
    /// Provides attached properties to implement drag and drop in MVVM
    /// </summary>
    public static class DragAndDrop
    {
        // Start positions of drags (required for minimum drag distance)
        private static Dictionary<FrameworkElement, Point> DragPreparation = 
            new Dictionary<FrameworkElement, Point>();

        /// <summary>
        /// Registers the attached property that can enable dragging
        /// </summary>
        public static DependencyProperty AllowDragProperty =
            DependencyProperty.RegisterAttached("AllowDrag", typeof(bool), typeof(DragAndDrop),
                new FrameworkPropertyMetadata(false, AllowDragChanged));

        /// <summary>
        /// Registers the attached property that defines the dragged data
        /// </summary>
        public static DependencyProperty DragDataProperty =
            DependencyProperty.RegisterAttached("DragData", typeof(object), typeof(DragAndDrop));

        /// <summary>
        /// Registers the attached property for the command to execute on a dropped element
        /// </summary>
        public static DependencyProperty OnFinishProperty =
            DependencyProperty.RegisterAttached("OnFinish", typeof(ICommand), typeof(DragAndDrop));

        /// <summary>
        /// Registers the attached property for the command to execute on the drop target
        /// </summary>
        public static DependencyProperty OnDropProperty = 
            DependencyProperty.RegisterAttached("OnDrop", typeof(ICommand), typeof(DragAndDrop),
                new FrameworkPropertyMetadata(null, OnDropChanged));

        /// <summary>
        /// Sets whether to allow dragging for an element
        /// </summary>
        /// <param name="element">An element</param>
        /// <param name="allowDrag">Whether to allow dragging</param>
        public static void SetAllowDrag(FrameworkElement element, bool allowDrag)
        {
            element.SetValue(AllowDragProperty, allowDrag);
        }

        public static bool GetAllowDrag(FrameworkElement element)
        {
            return (bool)element.GetValue(AllowDragProperty);
        }

        /// <summary>
        /// Sets the dragged data for a draggable element
        /// </summary>
        /// <param name="element">The draggable element</param>
        /// <param name="dragData">The drag transfered data</param>
        public static void SetDragData(FrameworkElement element, object dragData)
        {
            element.SetValue(DragDataProperty, dragData);
        }

        public static object GetDragData(FrameworkElement element)
        {
            return element.GetValue(DragDataProperty);
        }

        /// <summary>
        /// Sets the drop command for a dragged element
        /// </summary>
        /// <param name="draggedElement">The draggeed element</param>
        /// <param name="dropCommand">The drop command</param>
        public static void SetOnFinish(FrameworkElement draggedElement, ICommand dropCommand)
        {
            draggedElement.SetValue(OnFinishProperty, dropCommand);
        }

        public static ICommand GetOnFinish(FrameworkElement draggedElement)
        {
            return (ICommand)draggedElement.GetValue(OnFinishProperty);
        }

        /// <summary>
        /// Sets the drop command for a target element
        /// </summary>
        /// <param name="targetElement">The target element</param>
        /// <param name="dropCommand">The drop command</param>
        public static void SetOnDrop(FrameworkElement targetElement, ICommand dropCommand)
        {
            targetElement.SetValue(OnDropProperty, dropCommand);
        }

        public static ICommand GetOnDrop(FrameworkElement targetElement)
        {
            return (ICommand)targetElement.GetValue(OnDropProperty);
        }

        private static void AllowDragChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Get the source element
            FrameworkElement element = (FrameworkElement)obj;
            // Check if drag is allowed
            if ((bool)args.NewValue)
            {
                // Register mouse events for drags
                element.PreviewMouseLeftButtonDown += OnDragPrepare;
                element.PreviewMouseLeftButtonUp += OnDragDiscard;
            }
            else
            {
                // Unregister mouse events
                element.PreviewMouseLeftButtonDown -= OnDragPrepare;
                element.PreviewMouseLeftButtonUp -= OnDragDiscard;
            }
        }

        private static void OnDropChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            // Check for drop command
            if (args.NewValue != null)
            {
                // Allow drops
                element.AllowDrop = true;
                // Register drop events
                element.PreviewDrop += OnDrop;
            }
            else
            {
                // Forbid drops
                element.AllowDrop = false;
                // Unregister drop events
                element.PreviewDrop -= OnDrop;
            }
        }

        private static void OnDragPrepare(object sender, MouseButtonEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            // Save the start position
            DragPreparation[element] = args.GetPosition(null);
            // Register mouse move
            element.PreviewMouseMove += OnDragStart;
        }

        private static void OnDragDiscard(object sender, MouseButtonEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            // Unregister mouse move
            element.PreviewMouseMove -= OnDragStart;
            // Delete the saved start position
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
                DataObject dataObj = new DataObject();
                dataObj.SetData(typeof(object), data);
                dataObj.SetData(typeof(FrameworkElement), element);
                // Start the drag and drop operation
                DragDrop.DoDragDrop(element, dataObj, DragDropEffects.All);
            }
        }

        private static void OnDrop(object sender, DragEventArgs args)
        {
            FrameworkElement target = (FrameworkElement)sender;
            // Extract the drag data
            object data = args.Data.GetData(typeof(object));
            // Determine the drop handler
            ICommand dropped = (ICommand)target.GetValue(OnDropProperty);  
            // Execute the finish handler if possible
            if (dropped != null && dropped.CanExecute(data))
            {
                dropped.Execute(data);
                // Extract the source element
                FrameworkElement source = (FrameworkElement)args.Data.GetData(typeof(FrameworkElement));
                // Determine the finish handler
                ICommand finished = (ICommand)source.GetValue(OnFinishProperty);
                // Execute the finish handler if possible
                if (finished != null && finished.CanExecute(data))
                {
                    finished.Execute(data);
                }
            }
        }
    }

    /// <summary>
    /// Stores the position and the data of a drop
    /// </summary>
    public class Drop
    {
        /// <summary>
        /// Gets or sets the drop position
        /// </summary>
        public Point Position { get; private set; }
        /// <summary>
        /// Gets or sets the drop data
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Creates a new drop
        /// </summary>
        /// <param name="position">The drop position</param>
        /// <param name="data">The drop data</param>
        public Drop(Point position, object data)
        {
            this.Position = position;
            this.Data = data;
        }
    }
}
