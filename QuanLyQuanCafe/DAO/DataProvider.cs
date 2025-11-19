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
using System.Text.RegularExpressions;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        // này được gọi là phương pháp singleton
        // nó giúp chúng ta không cần phải khai báo đi khai báo lại nhiều lần 
        // cái data provider
        
        private static DataProvider instance;

        
        private string connectionSTR = "Data Source=DESKTOP-8TGHDSD;Initial Catalog=QuanLyQuanCafe;Integrated Security=True;Encrypt=False";



        public static DataProvider Instance 
        { 
            get { 
                if (instance == null)
                    instance = new DataProvider();  
                return DataProvider.instance; 
            }
            private set { DataProvider.instance = value; }
        
        }

        private DataProvider() { }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // PHẦN XỬ LÝ THAM SỐ (CHỈ CẦN THỰC HIỆN NẾU CÓ THAM SỐ)
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    var matches = Regex.Matches(query, @"@\w+");

                    int i = 0;
                    foreach (Match m in matches)
                    {
                        command.Parameters.AddWithValue(m.Value, parameter[i]);
                        i++;
                    }

                    /*int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }*/
                }

                // PHẦN THỰC THI QUERY VÀ ĐỔ DỮ LIỆU (LUÔN CẦN THỰC HIỆN)
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                // Không cần gọi connection.Close() vì khối using đã xử lý việc này
                // (Nhưng để nó ở đây cũng không gây lỗi)

            } // Khối using đảm bảo connection.Dispose() được gọi

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            { // khi sử dụng using thì cho dù có xảy ra vấn đề j đi chăng nữa thì khi mà kết thúc khối lệnh bên trong nó thì cái dữ liệu sql connection ở trên sẽ tự được giải phóng  
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // ví dụ như có lỗi ở phần đoạn này chẳng hạn thì nó sẽ tự động thu hồi tham số ở trên 

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }

                    }
                    
                }
                data = command.ExecuteNonQuery();

                connection.Close();

            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            { // khi sử dụng using thì cho dù có xảy ra vấn đề j đi chăng nữa thì khi mà kết thúc khối lệnh bên trong nó thì cái dữ liệu sql connection ở trên sẽ tự được giải phóng  
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // ví dụ như có lỗi ở phần đoạn này chẳng hạn thì nó sẽ tự động thu hồi tham số ở trên 

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }

                    }

                    
                }
                data = command.ExecuteScalar();

                connection.Close();

            }
            return data;
        }
    }
}
