using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Extensions
{
    public static class ContainerDragAndDrop
    {
        public static DependencyProperty ModeProperty =
            DependencyProperty.RegisterAttached("Mode", typeof(ContainerDragAndDropMode), typeof(ContainerDragAndDrop));

        public static ContainerDragAndDropMode GetMode(Control control)
        {
            return (ContainerDragAndDropMode)control.GetValue(ModeProperty);
        }

        public static void SetMode(Control control, ContainerDragAndDropMode value)
        {
            control.SetValue(ModeProperty, value);
        }

    }

    public enum ContainerDragAndDropMode
    {

    }
}
