using CourseManager.Common;
using CourseManager.DataAccess.DataEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.DataAccess
{
    /// <summary>
    /// 数据库连接类，一般都是进行单例处理
    /// </summary>
    public  class LocalDataAccess
    {
        /// <summary>
        /// 当前类的实例
        /// </summary>
        private static LocalDataAccess instance;

        /// <summary>
        /// 设置构造函数为私有，不能实例化，只能使用单例的方式
        /// </summary>
        private LocalDataAccess() { }

        /// <summary>
        /// 获得当前类的实例
        /// </summary>
        /// <returns></returns>
        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private SqlConnection conn;
        /// <summary>
        /// 数据库命令对象
        /// </summary>
        private SqlCommand comm;
        /// <summary>
        /// 数据库适配器
        /// </summary>
        private SqlDataAdapter adapter;

       /// <summary>
       /// 初始化数据库连接
       /// </summary>
       /// <returns>返回是否连接成功</returns>
        private bool DBConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            if(conn == null)
            {
                conn = new SqlConnection(connStr);
            }
            try
            {
                conn.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得当前登录用户的信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="Message">登录后的信息</param>
        /// <returns></returns>
        public UserEntity CheckUserInfo(string userName,string password)
        {
            try
            {
                if (DBConnection())
                {
                    string sql = "select * from users where user_name=@userName and password=@password and is_Validation=1";
                    adapter = new SqlDataAdapter(sql,conn);
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@userName", SqlDbType.VarChar) { Value = userName });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar) { Value = MD5Provider.GetMD5String(password+"@"+userName) });

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count <= 0)
                    {
                        throw new Exception( "用户名或密码错误");
                       
                    }

                    DataRow dr = table.Rows[0];
                    if (dr.Field<Int32>("is_can_login") == 0)
                    {
                        throw new Exception("当前用户没有权限使用此平台！");
                    }

                    UserEntity userInfo = new UserEntity();
                    userInfo.UserName = dr.Field<string>("user_name");
                    userInfo.RealName = dr.Field<string>("real_name");
                    userInfo.Password = dr.Field<string>("password");
                    userInfo.Avatar = dr.Field<string>("avatar");
                    userInfo.Gender = dr.Field<int>("gender");
                    return userInfo;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }


        /// <summary>
        /// 释放数据库连接等对象
        /// </summary>
        private void Dispose()
        {
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if(comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
    }
}
