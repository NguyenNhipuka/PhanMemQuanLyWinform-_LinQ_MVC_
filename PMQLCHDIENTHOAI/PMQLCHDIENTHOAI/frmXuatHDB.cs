using PMQLCHDIENTHOAI.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMQLCHDIENTHOAI.Data;

namespace PMQLCHDIENTHOAI
{
    public partial class frmXuatHDB : Form
    {
        public string mahdb = "";
        /// <summary>
        /// key =0: xuat hoa don ban,key =1: xuat hdm,key = 2:phieubao hanh
        /// key =3 : xuat bao hanh, key =4: sua chua
        /// </summary>
        public int key = 0;
        public frmXuatHDB()
        {
            InitializeComponent();
        }

        private void frmXuatHDB_Load(object sender, EventArgs e)
        {
            switch (key)
            {
                case 0:
                    xuathdb();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        private void xuathdb()
        {
            CryViewer.ReportSource = null;
            CryViewer.RefreshReport();
            CrHDB cr = new CrHDB();
            /// lay nguon
            using (DATADataContext db = new DATADataContext())
            {
                DataTable tb = new DataTable();
                tb = getHDB();
                cr.SetDataSource(tb);
                CryViewer.ReportSource = cr;

            }
            CryViewer.Show();
        }

        public DataTable getHDB()
        {

            DataTable data = new DataTable();
            using (DATADataContext db = new DATADataContext())
            {
                List<sp_HDB_Getlist_ByKeyResult> ds = db.sp_HDB_Getlist_ByKey(mahdb).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        //private void xuathdm()
        //{
        //    CachedCrHDM cr = new CachedCrHDM();
        //    CryViewer.ReportSource = null;
        //    CryViewer.RefreshReport();
        //    /// lay nguon
        //    using (DATADataContext db = new DATADataContext())
        //    {
        //        DataTable tb = new DataTable();
        //        tb = getHDM();
        //        cr.SetDataSource(tb);
        //        CryViewer.ReportSource = cr;

        //    }
        //    CryViewer.Show();
        //}

        //public DataTable getHDM()
        //{

        //    DataTable data = new DataTable();
        //    using (DATADataContext db = new DATADataContext())
        //    {
        //        List<sp_HDM_Getlist_ByMaHDM_ExportResult> ds = db.sp_HDM_Getlist_ByMaHDM_Export(mahdb).ToList();
        //        data = ConvertToDataTable(ds);
        //    }
        //    return data;
        //}
        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

    }
}
