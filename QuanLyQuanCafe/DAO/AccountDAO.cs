using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using QuanLyQuanCafe.DTO;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if(instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string username, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {username, passWord});
            
            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName) 
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where username = '" + userName+"'");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

       public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("Select userName, displayName, type from account");
        }
    }
}
