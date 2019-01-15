using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Muvvim.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Dialog : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TriggerProperty =
            DependencyProperty.Register(nameof(Trigger), typeof(bool), typeof(Dialog),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTriggerChanged)));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(Dialog),
                new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OnResultProperty =
            DependencyProperty.Register(nameof(OnResult), typeof(ICommand), typeof(Dialog));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OnCancelProperty =
            DependencyProperty.Register(nameof(OnCancel), typeof(ICommand), typeof(MessageBox));

        /// <summary>
        /// 
        /// </summary>
        public bool Trigger
        {
            get { return (bool)GetValue(TriggerProperty); }
            set { SetValue(TriggerProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand OnResult
        {
            get { return (ICommand)GetValue(OnResultProperty); }
            set { SetValue(OnResultProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand OnCancel
        {
            get { return (ICommand)GetValue(OnCancelProperty); }
            set { SetValue(OnCancelProperty, value); }
        }

        protected abstract void Run();

        protected void Reset()
        {
            Trigger = false;
        }

        private static void OnTriggerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue.Equals(true))
            {
                Dialog dialog = (Dialog)obj;
                Application.Current.Dispatcher.BeginInvoke((Action) (() => {
                    dialog.Run();
                }));
            }
        }
    }
}
