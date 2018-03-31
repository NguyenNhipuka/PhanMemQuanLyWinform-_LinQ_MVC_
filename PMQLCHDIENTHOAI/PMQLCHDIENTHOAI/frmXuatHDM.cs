using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PMQLCHDIENTHOAI.Report;
using CrystalDecisions.CrystalReports.Engine;
using PMQLCHDIENTHOAI.Data;
namespace PMQLCHDIENTHOAI
{
    public partial class frmXuatHDM : Form
    {
        public string mahd;
        public frmXuatHDM()
        {
            InitializeComponent();
        }

        
        private void xuathdm()
        {
            CrHDM cr = new CrHDM();
            CryViewer.ReportSource = null;
            CryViewer.RefreshReport();
            /// lay nguon
            using (DATADataContext db = new DATADataContext())
            {
                DataTable tb = new DataTable();
                tb = getHDM();
                cr.SetDataSource(tb);
                double datra = double.Parse(tb.Rows[0][9].ToString());
                cr.SetParameterValue("NO", datra);
                CryViewer.ReportSource = cr;

            }
            CryViewer.Show();
        }

        public DataTable getHDM()
        {

            DataTable data = new DataTable();
            using (DATADataContext db = new DATADataContext())
            {
                List<sp_HDM_Getlist_ByMaHDM_ExportResult> ds = db.sp_HDM_Getlist_ByMaHDM_Export(mahd).ToList();
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
            xuathdm();
        }
    }
}
