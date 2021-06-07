using CourseManager.Common;
using CourseManager.DataAccess;
using CourseManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManager.ViewModel
{
    /// <summary>
    /// 登录视图的ViewModel
    /// </summary>
    public class LoginViewModel:INotifyBase
    {
        /// <summary>
        /// 关闭命令
        /// </summary>
        public CommandBase CloseWindowCommand { get; set; }

        public CommandBase LoginButtonCommand { get; set; }

        /// <summary>
        /// LoginModel页面的通知属性model
        /// </summary>
        public LoginModel LoginModel { get; set; } = new LoginModel();


        private string _errorMessage;
        /// <summary>
        /// 页面登录错误提示消息
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value;this.DoNotify(); }
        }


        private Visibility _showProgress = Visibility.Collapsed;
        /// <summary>
        /// 显示滚动条
        /// </summary>
        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set { _showProgress = value;this.DoNotify(); }
        }



        public LoginViewModel()
        {
            #region 初始化页面所有命令命令
            this.CloseWindowCommand = new CommandBase();
            this.CloseWindowCommand.DoExecute = new Action<object>(OnCloseWindowExecute);
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>(OnCloseWindowCanExecute);

            this.LoginButtonCommand = new CommandBase();
            this.LoginButtonCommand.DoExecute = new Action<object>(OnLoginButtonExecute);
            this.LoginButtonCommand.DoCanExecute = new Func<object, bool>(OnLoginButtonCanExecute);
            #endregion


        }

        /// <summary>
        /// 关闭窗口按钮的命令回调
        /// </summary>
        /// <param name="parameter"></param>
        public void OnCloseWindowExecute(object parameter)
        {
            (parameter as Window).Close();

            Action<object> action = new Action<object>(t=> { });
            action(1);
            action.Invoke(1);
            EventHandler<int> e = new EventHandler<int>((t,z)=> { });
        }

        /// <summary>
        /// 关闭窗口按钮的命令验证
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool OnCloseWindowCanExecute(object parameter)
        {
            return true;
        }


        /// <summary>
        /// 登录按钮的命令
        /// </summary>
        /// <param name="parameter"></param>
        public void OnLoginButtonExecute(object parameter)
        {
            //登录时显示精度条
            this.ShowProgress = Visibility.Visible;
            ErrorMessage = "";

            if (string.IsNullOrEmpty(LoginModel.UserName))
            {
                ErrorMessage = "用户名不能为空！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.Password))
            {
                ErrorMessage = "密码不能为空！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.ValidationCode))
            {
                ErrorMessage = "验证码不能为空！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (LoginModel.ValidationCode.ToLower() != "1234")
            {
                ErrorMessage = "验证码不正确！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            
            //启动一个多线程，防止当访问数据过多时卡死
            Task.Run(new  Action(async () =>
            {
                await Task.Delay(2000);
                try
                {
                    //从数据库中查询用户是否存在
                    var userInfo = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    if (userInfo == null)
                    {
                        throw new Exception("登录失败，用户名或密码错误");
                    }

                    GlobalValue.UserInfo = userInfo;

                    //关闭登录窗体
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        (parameter as Window).DialogResult = true;
                    });
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message;
                }
                finally
                {
                    this.ShowProgress = Visibility.Collapsed;
                }
                
            }));
            
        }


        public bool OnLoginButtonCanExecute(object parameter)
        {
            return true;
        }
    }
}
