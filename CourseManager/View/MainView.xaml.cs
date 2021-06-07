using CourseManager.Common;
using CourseManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseManager.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            //设置当前窗体的MVVM模型的对于model
            MainViewModel model = new MainViewModel();
            this.DataContext = model;
            //设置并设置当前登录用户的信息
            model.UserInfo.Avatar = GlobalValue.UserInfo.Avatar;
            model.UserInfo.UserName = GlobalValue.UserInfo.RealName;
            model.UserInfo.Gender = GlobalValue.UserInfo.Gender;

            //当最大化程序时，不让程序遮挡电脑的状态栏
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
        }

        /// <summary>
        /// 控制窗口可以拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 最小化按钮点击事件，点击窗体最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 最大化按钮点击事件，点击窗体最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// 关闭按钮点击事件，点击窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender,RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
