using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Dialogs
{
    public abstract class BaseDialog : Control
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(BaseDialog),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsActiveChanged)));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(MessageBox),
                new FrameworkPropertyMetadata(string.Empty));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        protected abstract void Run();

        private static void OnIsActiveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue.Equals(true))
            {
                (obj as BaseDialog).Run();
            }
        }

    }
}
