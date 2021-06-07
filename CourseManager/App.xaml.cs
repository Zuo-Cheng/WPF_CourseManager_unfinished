using CourseManager.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 重写程序启动事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //打开登录窗体，注意：这里线程会停止在这个地方，
            //直到LoginView().ShowDialog() == true的时候，或者窗体关闭的时候
            //才会继续往下走。
            if (new LoginView().ShowDialog() == true)
            {
                new MainView().ShowDialog();
            }
            Application.Current.Shutdown();

        }
    }
}
