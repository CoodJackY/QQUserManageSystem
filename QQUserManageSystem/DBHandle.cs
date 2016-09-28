using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QQUserManageSystem
{
    /// <summary>
    /// 操作数据库类
    /// </summary>
    class DBHandle
    {
        //连接字符串
        private const string strConn = @"Data Source=DESKTOP-GMMJE92;Initial Catalog=QQDB;Integrated Security=True";

        #region 检查管理员信息
        /// <summary>
        /// 检查管理员信息
        /// </summary>
        /// <param name="userName">管理员用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="strMsg">需返回的处理信息</param>
        /// <returns>成功&失败</returns>
        public bool CheckAdminInfo(string userName, string pwd, ref string strMsg)
        {
            //创建数据库连接
            SqlConnection conn = new SqlConnection(strConn);

            try
            {
                //创建Sql语句
                string strSql = "select count(*) from Admin where LoginId='" + userName + "' and LoginPwd='" + pwd + "'";
                conn.Open();
                //创建Command命令
                SqlCommand comm = new SqlCommand(strSql, conn);
                int iRet = (int)comm.ExecuteScalar();
                if (iRet != 1)
                {
                    strMsg = "输入无效！";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                strMsg = "发生异常！";
                return false;
            }
            finally
            {
                //关闭数据库连接
                conn.Close();
            }
        }
        #endregion

        #region 取得用户信息列表
        /// <summary>
        /// 取得学生用户列表
        /// </summary>
        /// <returns>DataReader</returns>
        public SqlDataReader GetUserList()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" SELECT");
                sb.AppendLine("           a.[UserId]");
                sb.AppendLine("          ,a.[UserName]");
                sb.AppendLine("          ,b.[LevelName]");
                sb.AppendLine("          ,a.[Email]");
                sb.AppendLine("          ,a.[OnLineDay]");
                sb.AppendLine(" FROM");
                sb.AppendLine("             [UserInfo] a, [Level] b ");
                sb.AppendLine(" WHERE");
                sb.AppendLine("           a.[LevelId] = b.[LevelId]");
                SqlCommand comm = new SqlCommand(sb.ToString(), conn);

                return comm.ExecuteReader();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 取得所有用户的用户编号和用户等级
        /// <summary>
        /// 取得所有用户的用户编号和用户等级
        /// </summary>
        /// <returns>DataReader</returns>
        public SqlDataReader GetUserIdAndOnlineDay()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" SELECT");
                sb.AppendLine("           [UserId]");
                sb.AppendLine("          ,[OnLineDay]");
                sb.AppendLine(" FROM");
                sb.AppendLine("           [UserInfo] ");
                SqlCommand comm = new SqlCommand(sb.ToString(), conn);
                return comm.ExecuteReader();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 更新在线天数
        /// <summary>
        /// 更新在线天数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="onlineDay">在线天数</param>
        /// <returns>受影响的行数&-1：异常</returns>
        public int UpdateOnlineDay(int userId, double newOnlineDay)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE");
                sb.AppendLine("           [UserInfo]");
                sb.AppendLine("SET");
                sb.AppendLine("          [OnLineDay]=" + newOnlineDay);
                sb.AppendLine("WHERE");
                sb.AppendLine("          [UserId]=" + userId);
                SqlCommand comm = new SqlCommand(sb.ToString(), conn);
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region 更新用户等级
        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="iLevel">用户等级</param>
        /// <returns>受影响的行数&-1：异常</returns>
        public int UpdateUserLevel(int userId, int iLevel)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" UPDATE");
                sb.AppendLine("           [UserInfo]");
                sb.AppendLine(" SET");
                sb.AppendLine("           [LevelId]=" + iLevel);
                sb.AppendLine(" WHERE");
                sb.AppendLine("           [UserId]=" + userId);
                SqlCommand comm = new SqlCommand(sb.ToString(), conn);
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userName">昵称</param>
        /// <param name="userPwd">密码</param>
        /// <param name="email">邮箱</param>
        /// <returns>受影响行数&-1:异常</returns>
        public object InsertUserInfo(string userName, string userPwd, string email)
        {
            SqlConnection conn = new SqlConnection(strConn);
            try
            {
                conn.Open();

                StringBuilder sb = new StringBuilder();
                //插入用户记录
                sb.AppendLine(" INSERT INTO");
                sb.AppendLine("          [UserInfo]");
                sb.AppendLine(" VALUES");
                sb.AppendLine("          ('" + userName + "','" + userPwd + "',1,'" + email + "',0);");
                //获得插入记录的用户编号
                sb.AppendLine(" SELECT @@Identity;");

                SqlCommand comm = new SqlCommand(sb.ToString(), conn);
               // return comm.ExecuteNonQuery();
                return comm.ExecuteScalar();
            }
            catch (Exception)
            {               
                return -1;
            }
        }
        #endregion
        
        #region 按用户编号查询用户信息
        public SqlDataReader GetUserByID(int UserID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" SELECT");
                sb.AppendLine("           a.[UserId]");
                sb.AppendLine("          ,a.[UserName]");
                sb.AppendLine("          ,b.[LevelName]");
                sb.AppendLine("          ,a.[Email]");
                sb.AppendLine("          ,a.[OnLineDay]");
                sb.AppendLine( "FROM");
                sb.AppendLine("             [UserInfo] a, [Level]  b");
                sb.AppendLine(" WHERE");
                sb.AppendLine("           a.[UserId] = " + UserID);
                sb.AppendLine(" AND");
                sb.AppendLine("           a.[LevelId] = b.[LevelId]");
                SqlCommand comm = new SqlCommand(sb.ToString(), conn);
                return comm.ExecuteReader();
            }
            catch(Exception)
            {
                return null;
            }
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="strUserId">用户编号</param>
        /// <returns>受影响的行数&-1：失败</returns>
        public int DeleteUserInfo(int strUserId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" DELETE FROM");
                sb.AppendLine("           [UserInfo]");
                sb.AppendLine( "WHERE");
                sb.AppendLine("          [UserId]=" + strUserId);
                SqlCommand comm = new SqlCommand(sb.ToString(), conn);
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion
    }
}
