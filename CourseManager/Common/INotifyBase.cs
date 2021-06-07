using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Common
{
    /// <summary>
    /// 属性通知的基类
    /// </summary>
    public class INotifyBase :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 实现属性通知的方法，[CallerMemberName]表示该参数可以有默认值
        /// </summary>
        /// <param name="propName"></param>
        public void DoNotify([CallerMemberName] string propName="")
        {
            //通过这个方式可以实现属性的通知
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
