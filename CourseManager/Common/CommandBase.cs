using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseManager.Common
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return DoCanExecute?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            DoExecute.Invoke(parameter);
        }

        /// <summary>
        /// Action<>表示一个没有返回值的委托方法
        /// </summary>
        public  Action<object> DoExecute;

        /// <summary>
        /// 表示一个具有返回值的方法
        /// </summary>
        public Func<object,bool> DoCanExecute;
    }
}
