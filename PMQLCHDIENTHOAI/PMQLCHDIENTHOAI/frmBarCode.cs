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
    public partial class frmBarCode : Form
    {
        public frmBarCode()
        {
            InitializeComponent();
        }
        public DataTable getAllTB()
        {

            DataTable data = new DataTable();
            using (DATADataContext db = new DATADataContext())
            {
                List<sp_TB_GetlistResult> dsTatCaTB = db.sp_TB_Getlist().ToList();
                data = ConvertToDataTable(dsTatCaTB);
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
        private void frmBarCode_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            CrBarCodeProduct cr = new CrBarCodeProduct();
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.RefreshReport();
            /// lay nguon
            using (DATADataContext db = new DATADataContext())
            {
                DataTable tb = new DataTable();
                tb = getAllTB();
                cr.SetDataSource(tb);

                crystalReportViewer1.ReportSource = cr;

            }
            crystalReportViewer1.Show();
        }
    }
}
