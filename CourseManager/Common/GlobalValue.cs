using CourseManager.DataAccess.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Common
{
    /// <summary>
    /// 用于存储当前登录的用户信息的类
    /// </summary>
    public  class GlobalValue
    {
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public static UserEntity UserInfo { get; set; }
    }
}
