using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using System.IO;
using System.Diagnostics;
using Microsoft.Reporting.WinForms;

namespace CaixiLour_bd_v2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportViewer ReportViewer1 = new ReportViewer();

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportEmbeddedResource = "CaixiLour_bd_v2.catalogo.rdlc";
            //tela enxe
            List<ReportParameter> listreportparameter = new List<ReportParameter>();
            listreportparameter.Add(new ReportParameter("nome",textBox1.Text));
            ReportViewer1.LocalReport.SetParameters(listreportparameter);

            Warning[] warning1;
            string[] string1;
            string mimetype;
            string encoding;
            string extension;
            //relatorio
            byte[] bytePDF = ReportViewer1.LocalReport.Render("Pdf", null, out mimetype, out encoding,out extension,out string1, out warning1);

            FileStream filestreampdf;
            String nomearquivoPdf = Path.GetTempPath()+"catalogo"+ DateTime.Now.ToString("dd_MM_yyyy")+".pdf";
            filestreampdf = new FileStream(nomearquivoPdf, FileMode.Create);
            filestreampdf.Write(bytePDF, 0, bytePDF.Length);
            filestreampdf.Close();
            Process.Start(nomearquivoPdf);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            
        }
    }
}
