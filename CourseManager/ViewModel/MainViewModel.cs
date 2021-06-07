using CourseManager.Common;
using CourseManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManager.ViewModel
{
    public class MainViewModel:INotifyBase
    {

        public UserModel UserInfo { get; set; } = new UserModel();

        private string _searchText;

        /// <summary>
        /// 搜索框的文本
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value;DoNotify(); }
        }   

        private FrameworkElement _mainContent;

        /// <summary>
        /// 主体页面
        /// </summary>
        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value;DoNotify(); }
        }

        /// <summary>
        /// MainView界面导航切换命令
        /// </summary>
        public CommandBase NavChangeCommand { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MainViewModel()
        {
            this.NavChangeCommand = new CommandBase();
            this.NavChangeCommand.DoExecute = new Action<object>(DoNavChangeCommand);
            this.NavChangeCommand.DoCanExecute = new Func<object, bool>((o) => true);

            //初始化页面，默认显示首页
            DoNavChangeCommand("HomePageView");
        }

        /// <summary>
        /// MainView界面导航切换命令的回调方法,切换页面
        /// </summary>
        private void DoNavChangeCommand(object obj)
        {
            Type type = Type.GetType(string.Format("CourseManager.View.{0}",obj.ToString()));
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}
