using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQUserManageSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //管理员登录
            UserManager manger = new UserManager();
            manger.Login();
        }
    }
}
