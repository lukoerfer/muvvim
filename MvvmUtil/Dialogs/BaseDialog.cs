using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MvvmUtil.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseDialog : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(BaseDialog),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsActiveChanged)));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(MessageBox),
                new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        protected abstract void Run();

        protected void Reset()
        {
            IsActive = false;
        }

        private static void OnIsActiveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue.Equals(true))
            {
                BaseDialog dialog = (BaseDialog)obj;
                Application.Current.Dispatcher.BeginInvoke((Action) (() => {
                    dialog.Run();
                }));
            }
        }
    }
}
