using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QQUserManageSystem
{
    /// <summary>
    /// 处理QQ用户管理业务信息类
    /// </summary>
    class UserManager
    {
        private DBHandle _dbHandle = new DBHandle();//创建DBHandle的实例
        const String ERRMSG = "数据操作失败！";
        const String EXCEPT = "出现异常。请与系统管理员联系！";
        
        #region  执行验证管理员登录并处理结果信息
        /// <summary>
        /// 执行验证管理员登录并处理结果信息
        /// </summary>
        public void Login()
        {
            int count = 0;
            do
            {
                string strUserName = string.Empty;//初始化管理员登录名
                string strPwd = string.Empty;//初始化管理员密码

                count++; 
                
                Console.WriteLine("请输入用户名：");
                strUserName = Console.ReadLine();
                Console.WriteLine("请输入密码：");
                strPwd = Console.ReadLine();

                //非空验证
                if (strUserName.Equals(string.Empty) || strPwd.Equals(string.Empty))
                {
                    Console.WriteLine("输入错误，请重新输入！\n");
                    continue;//重新输入用户名和密码
                }
                else
                {
                    // 需返回的结果信息
                    string strMsg = string.Empty;
                    //数据库验证
                    bool bRet = _dbHandle.CheckAdminInfo(strUserName, strPwd, ref strMsg);
                    if (bRet)
                    {
                        Console.WriteLine("登录成功！");
                        // 显示菜单
                        ShowMenu();
                        break;//退出程序
                    }
                    else
                    {
                        Console.WriteLine("登录失败：" + strMsg + "\n");
                        continue;//重新输入用户名和密码
                    }
                }
            } while (count < 3);
            if (count == 3)
                Console.WriteLine("\n连续三次登录失败，退出本系统！\n");
        }
        #endregion

        #region 显示菜单
        /// <summary>
        /// 显示菜单
        /// </summary>
        private void ShowMenu()
        {
            string option = "";
            do
            {
                Console.WriteLine("");
                Console.WriteLine("=======欢迎登录QQ用户信息管理系统======");
                Console.WriteLine("----------------请选择菜单项----------");
                Console.WriteLine("1、显示用户清单");
                Console.WriteLine("2、更新在线天数");
                Console.WriteLine("3、添加用户新记录");
                Console.WriteLine("4、更新用户等级");
                Console.WriteLine("5、删除用户记录");
                Console.WriteLine("0、退出");
                Console.WriteLine("=======================================");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1"://显示用户信息
                        ShowUserInfo();
                        continue;
                    case "2"://更新在线天数
                        UpdateOnLineDay();
                        continue;
                    case "3"://添加用户
                        InsertUserInfo();
                        continue;
                    case "4"://更新用户等级
                        UpdateUserLevel();
                        continue;
                    case "5"://删除用户
                        DeleteUserInfo();
                        continue;
                    case "0":
                        if (IsExit())
                        {
                              break;//退出
                        }
                        else
                        {
                            continue;
                        }
                    default:
                        continue;
                }

                break;
            } while (true);

        }
        #endregion

        #region 显示用户列表
        /// <summary>
        ///  输出用户列表
        /// </summary>
        private void ShowUserInfo()
        {
            SqlDataReader reader = _dbHandle.GetUserList();
            if (reader == null)
            {
                Console.WriteLine(EXCEPT);
                return;
            }
            DisplayUserInfo(reader);
        }

        /// <summary>
        /// 输出用户信息
        /// </summary>
        /// <param name="reader">查询获得的用户记录</param>
        private void DisplayUserInfo(SqlDataReader reader)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("编号\t昵称\t\t等级\t\t邮箱\t\t在线天数");
            Console.WriteLine("--------------------------------------------------------------------------------");
            while (reader.Read())
            {
                Console.Write(reader["UserId"] + "\t");
                Console.Write(reader["UserName"] + "\t");
                Console.Write(ShowDesign((String)reader["LevelName"])+ "\t\t");
                Console.Write(reader["Email"] + "\t");
                Console.WriteLine(reader["OnLineDay"]);
            }
            Console.WriteLine("--------------------------------------------------------------------------------");

        }
        #endregion

        #region 根据等级名称输出相应符号
        /// <summary>
        /// 根据等级名称输出相应符号
        /// </summary>
        /// <param name="strLevel">等级名称</param>
        /// <returns></returns>
        private string ShowDesign(string strLevel)
        {
            string strDesign = string.Empty;
            switch (strLevel)
            {
                case "无等级":
                    strDesign = "―";
                    break;
                case "星星":
                    strDesign = "☆";
                    break;
                case "月亮":
                    strDesign = "€";
                    break;
                case "太阳":
                    strDesign = "◎";
                    break;
                default:
                    break;
            }
            return strDesign;
        }
        #endregion

        #region 更新用户在线天数
        /// <summary>
        /// 更新用户在线天数
        /// </summary>
        private void UpdateOnLineDay()
        {
            try
            {
                Console.WriteLine("请输入用户编号：");
                string strUserId = Console.ReadLine();
                int iUserId = Convert.ToInt32(strUserId);
                Console.WriteLine("请输入新的在线天数");
                string strNewOnlineDay = Console.ReadLine();
                double iNewOnlineDay = Convert.ToDouble(strNewOnlineDay);
                int iRet = _dbHandle.UpdateOnlineDay(iUserId, iNewOnlineDay);
                if (iRet == -1)
                    Console.WriteLine(ERRMSG);
                else if (iRet == 0)
                {
                    Console.WriteLine("用户记录不存在");
                }
                else
                {
                    Console.WriteLine("修改成功！");
                }
            }
            catch (Exception)
            {
                Console.WriteLine(EXCEPT);
            }
        }
        #endregion

        #region 添加一条用户记录
        /// <summary>
        /// 输出添加用户的结果
        /// </summary>
        private void InsertUserInfo()
        {
            Console.WriteLine("请输入用户昵称：");
            string strUserName = Console.ReadLine();
            Console.WriteLine("请输入用户密码：");
            string strUserPwd = Console.ReadLine();
            Console.WriteLine("请输入用户邮箱地址：");
            string strUserEmail = Console.ReadLine();
         
            int iRet = Convert.ToInt32(_dbHandle.InsertUserInfo(strUserName, strUserPwd, strUserEmail));
            if (iRet == -1)
            {
                Console.WriteLine(EXCEPT);
            }
            else if (iRet == 0)
            {
                Console.WriteLine("用户记录不存在");
            }
            else
            {
                Console.WriteLine("插入成功！用户编号是：" + iRet);
            }
        }
        #endregion

        #region 根据在线天数判定用户等级
        /// <summary>
        /// 根据在线天数判定用户等级
        /// </summary>
        /// <param name="iOnlineDay">在线天数</param>
        /// <returns>/计算后的用户等级</returns>
        private int JudgeLevelByOnLineDay(double iOnlineDay)
        {
            const int LEVEL1 = 5;
            const int LEVEL2 = 32;
            const int LEVEL3 = 320;
           
            int iNewLevel = 0;//计算后的等级

            if (iOnlineDay >= LEVEL1 && iOnlineDay < LEVEL2)//5<=在线天数<32更新为星星
            {
                iNewLevel = 2;
            }
            else if (iOnlineDay >= LEVEL2 && iOnlineDay < LEVEL3)//32<=在线天数<320更新为月亮
            {
                iNewLevel = 3;
            }
            else if (iOnlineDay >= LEVEL3)//在线天数>=320更新为太阳
            {
                iNewLevel = 4;
            }
            else
            {
                iNewLevel = 1;
            }
            return iNewLevel;
        }
        #endregion

        #region 更新用户等级
        /// <summary>
        ///  更新用户等级
        /// </summary>
        private void UpdateUserLevel()
        {
            //取得所有用户的用户编号和在线天数
            SqlDataReader reader = _dbHandle.GetUserIdAndOnlineDay();
            if (reader == null)
            {
                Console.WriteLine(EXCEPT);
                return;
            }

//            Console.WriteLine("----------------------开始更新--------------------------------");
            int iUserId = 0;     //用户编号
            double iLineDay = 0; //用户在线天数
            int iLevelId = 0;    //用户等级
            int count = 0;       //更新记录数
            //循环取得每行的用户编号和用户等级
            while (reader.Read())
            {
                iUserId = Convert.ToInt32(reader["UserId"]);//用户编号的类型转换
                iLineDay = Convert.ToDouble(reader["OnLineDay"]);//用户在线天数的类型转换
                iLevelId = JudgeLevelByOnLineDay(iLineDay);//根据在线天数判定用户等级
                _dbHandle.UpdateUserLevel(iUserId, iLevelId);
                count++;
            }
            Console.WriteLine("本次共更新用户记录数：{0}", count);
            Console.WriteLine("更新成功！");
  //          Console.WriteLine("----------------------更新结束---------------------------------");

        }
        #endregion

        #region 删除指定的用户记录
        /// <summary>
        /// 删除指定的用户记录
        /// </summary>
        public void DeleteUserInfo()
        {
            try
            {
                //接收要删除的用户编号
                Console.WriteLine("请输入删除的用户编号：");
                string strUserId = Console.ReadLine();
                int iUserId = Convert.ToInt32(strUserId);
                
                //按用户编号查询要删除的用户记录
                Console.WriteLine("将要删除的用户信息是：");
                SqlDataReader reader = _dbHandle.GetUserByID(iUserId);
                if (reader == null)
                {
                    Console.WriteLine(EXCEPT);
                    return;
                }

                //确认是否要删除用户记录
                DisplayUserInfo(reader);
                Console.WriteLine("要删除该用户记录吗？（Y/N）");
                if (Console.ReadLine().Trim().ToUpper() != "Y")
                {
                    Console.WriteLine("退出删除操作！");
                    return;
                }

                //执行删除操作
                int iRet = _dbHandle.DeleteUserInfo(iUserId);
                if (iRet == -1)
                {
                    Console.WriteLine(ERRMSG);
                }
                else
                {
                    Console.WriteLine("删除成功！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除失败：" + ex.Message);
            }
        }
        #endregion

        #region 是否退出
        /// <summary>
        ///  是否退出
        /// </summary>
        /// <returns>true：是；false：否</returns>
        private bool  IsExit()
        {
            Console.WriteLine("是否退出？（Y/N）");
            string strRet = Console.ReadLine();
            strRet = strRet.Trim().ToUpper();
            if (strRet.Equals ("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
