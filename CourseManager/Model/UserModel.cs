using CourseManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Model
{
    /// <summary>
    /// 用户的model类，包含用户的基本信息
    /// </summary>
    public class UserModel:INotifyBase
    {
        private string _avatar;

        /// <summary>
        /// 用户的头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; DoNotify(); }
        }

        private string _userName;

        /// <summary>
        /// 用户的名字
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; DoNotify(); }
        }


        private int _gender;

        /// <summary>
        /// 用户的性别
        /// </summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value;DoNotify(); }
        }



    }
}
