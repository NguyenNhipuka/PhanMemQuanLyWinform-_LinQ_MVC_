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
using PMQLCHDIENTHOAI.Report;


namespace PMQLCHDIENTHOAI
{
    public partial class frmXuatSuaChua : Form
    {
        public string mahd;
        public int mabh;
        public int key = 0;
        public frmXuatSuaChua()
        {
            InitializeComponent();
        }
        private void xuathdsc()
        {
            CrySuaChua cr = new CrySuaChua();
            CryViewer.ReportSource = null;
            CryViewer.RefreshReport();
            /// lay nguon
            using (DATADataContext db = new DATADataContext())
            {
                DataTable tb = new DataTable();
                tb = getHDSC();
                cr.SetDataSource(tb);
               
                CryViewer.ReportSource = cr;

            }
            CryViewer.Show();
        }

        public DataTable getHDSC()
        {

            DataTable data = new DataTable();
            using (DATADataContext db = new DATADataContext())
            {
                List<sp_SUACHUA_GetlistexportResult> ds = db.sp_SUACHUA_Getlistexport(mahd).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        private void xuathdbh()
        {
             CrBaoHanh cr = new CrBaoHanh();
            CryViewer.ReportSource = null;
            CryViewer.RefreshReport();
            /// lay nguon
            using (DATADataContext db = new DATADataContext())
            {
                DataTable tb = new DataTable();
                tb = getHDBH();
                cr.SetDataSource(tb);

                CryViewer.ReportSource = cr;

            }
            CryViewer.Show();
        }

        public DataTable getHDBH()
        {

            DataTable data = new DataTable();
            using (DATADataContext db = new DATADataContext())
            {
                List<sp_BAOHANH_GetlistExResult> ds = db.sp_BAOHANH_GetlistEx(mabh).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }
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

        private void CryViewer_Load(object sender, EventArgs e)
        {
            switch (key)
            {
                case 0: xuathdsc(); break;
                case 1:xuathdbh(); break;
                case 2: xuatpbh();break;
                default: break;
            }
            
        }

        private void xuatpbh()
        {
            CrPBH cr = new CrPBH();
            CryViewer.ReportSource = null;
            CryViewer.RefreshReport();
            /// lay nguon
            using (DATADataContext db = new DATADataContext())
            {
                DataTable tb = new DataTable();
                var mapbh = (from n in db.PHIEUBAOHANHs where n.SERIALBH == mahd select n.IDPBH).Sum();
                List<sp_PHIEUBAOHANH_GetlistByID_ExportResult> ds = db.sp_PHIEUBAOHANH_GetlistByID_Export(mapbh).ToList();
                tb = ConvertToDataTable(ds);
                cr.SetDataSource(tb);
                cr.SetParameterValue("NGAYHETHAN", ds[0].NGAYHETHAN.ToString());
                cr.SetParameterValue("IDPBH", mapbh);

                CryViewer.ReportSource = cr;

            }
            CryViewer.Show();
        }
        
    }
}
