using MvvmUtil.Util;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MvvmUtil.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    [DesignTimeVisible(false)]
    public class MessageBox : Dialog
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageProperty = 
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(MessageBox), 
                new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ButtonsProperty = 
            DependencyProperty.Register(nameof(Buttons), typeof(MessageBoxButton), typeof(MessageBox), 
                new FrameworkPropertyMetadata(MessageBoxButton.OK));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(Image), typeof(MessageBoxImage), typeof(MessageBox),
                new FrameworkPropertyMetadata(MessageBoxImage.None));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DefaultButtonProperty = 
            DependencyProperty.Register(nameof(DefaultButton), typeof(MessageBoxResult), typeof(MessageBox), 
                new FrameworkPropertyMetadata(MessageBoxResult.None));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OnOkProperty = 
            DependencyProperty.Register(nameof(OnOk), typeof(ICommand), typeof(MessageBox));


        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OnYesProperty = 
            DependencyProperty.Register(nameof(OnYes), typeof(ICommand), typeof(MessageBox));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OnNoProperty = 
            DependencyProperty.Register(nameof(OnNo), typeof(ICommand), typeof(MessageBox));

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxButton Buttons
        {
            get { return (MessageBoxButton)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxImage Image
        {
            get { return (MessageBoxImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxResult DefaultButton
        {
            get { return (MessageBoxResult)GetValue(DefaultButtonProperty); }
            set { SetValue(DefaultButtonProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand OnOk
        {
            get { return (ICommand)GetValue(OnOkProperty); }
            set { SetValue(OnOkProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand OnYes
        {
            get { return (ICommand)GetValue(OnYesProperty); }
            set { SetValue(OnYesProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand OnNo
        {
            get { return (ICommand)GetValue(OnNoProperty); }
            set { SetValue(OnNoProperty, value); }
        }

        protected override void Run()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show(Window.GetWindow(this), Message, Title, Buttons, Image, DefaultButton);
            Reset();
            OnResult?.ExecuteIfPossible(result);
            switch (result)
            {
                case MessageBoxResult.OK: OnOk?.ExecuteIfPossible(null); break;
                case MessageBoxResult.Cancel: OnCancel?.ExecuteIfPossible(null); break;
                case MessageBoxResult.Yes: OnYes?.ExecuteIfPossible(null); break;
                case MessageBoxResult.No: OnNo?.ExecuteIfPossible(null); break;
            }
        }

    }
}
