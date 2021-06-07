using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CourseManager.Common
{
    /// <summary>
    /// 密码框的附加属性类
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// 是否更新
        /// </summary>
        public static bool _isUpdating = false;



        public static string GetPasswordProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPasswordProperty(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

       /// <summary>
       /// 密码框的附加属性
       /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("PasswordProperty", typeof(string),
                typeof(PasswordHelper), new FrameworkPropertyMetadata("",new PropertyChangedCallback(OnPasswordPropertyChanged)));

        /// <summary>
        /// 当密码框的属性发生改变是回调的方法
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="e"></param>
        private static void OnPasswordPropertyChanged(DependencyObject dependency,DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = dependency as PasswordBox;
            password.PasswordChanged -= Password_PasswordChanged;
            if (!_isUpdating)
            {
                password.Password = e.NewValue?.ToString();
            }
            password.PasswordChanged += Password_PasswordChanged;
        }




        public static bool GetAttachProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static void SetAttachProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        // Using a DependencyProperty as the backing store for AttachPropert.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("AttachProperty", typeof(bool), typeof(PasswordHelper), 
                new FrameworkPropertyMetadata(false,new PropertyChangedCallback(OnAttachProperty)));

        private static void OnAttachProperty(DependencyObject dependency, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = dependency as PasswordBox;
            password.PasswordChanged += Password_PasswordChanged;
        }


        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _isUpdating = true;
            SetPasswordProperty(passwordBox, passwordBox.Password);
            _isUpdating = false;
        }
    }
}
