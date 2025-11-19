using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        // thành công   : trả về bill ID
        // thất bại     : trả về -1

        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from bill where idTable = " + id + " and status = 0");
            
            if (data.Rows.Count > 0) 
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            
            return -1;
        }

        public void checkOut(int id, float totalPrice)
        {
            string query = "update bill set dateCheckOut = getdate(),status = 1, totalPrice = " + totalPrice + "  where id = "+ id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idTable", new object[] { id });
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @checkIn, @checkOut", new Object[] { checkIn, checkOut });
        }

        public int getMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(id) from bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
