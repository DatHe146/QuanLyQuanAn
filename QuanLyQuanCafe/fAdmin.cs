using QuanLyQuanCafe.DAO;
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

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();

            load();
        }

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        BindingSource accountList = new BindingSource();

        BindingSource tableList = new BindingSource();

        BindingSource categoryList = new BindingSource();

        void load()
        {
            dtgvAccount.DataSource = accountList;

            dtgvTable.DataSource = tableList;

            dtgvCategory.DataSource = categoryList;

            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadDateTimePickerBill();

            LoadListFood();
            addAccountBinding();
            loadAccount();
            addTableBinding();
            loadTable();

            addCategoryList();
            loadCategory();
        }

        void addAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            txbAccountType.DataBindings.Add(new Binding("text", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void addTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("text", dtgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("text", dtgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbStatusTable.DataBindings.Add(new Binding("text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }

        void addCategoryList()
        {
            txbCategoryID.DataBindings.Add(new Binding("text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbNameCategory.DataBindings.Add(new Binding("text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
        }

        void loadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void loadTable()
        {
            tableList.DataSource = TableDAO.Instance.GetListTable();
        }

        void loadCategory() 
        {
            categoryList.DataSource = CategoryDAO.Instance.GetCategoryList();
        }

        #region methods

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void LoadListFood()
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
        }

        #endregion

        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        #endregion

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            loadAccount();
        }
    }
}
