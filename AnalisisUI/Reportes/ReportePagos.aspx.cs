using Entidades;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnalisisUI.Reportes
{
    public partial class ReportePagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)//solo se carga si no se esta haciendo postback
            {
                BLL.RepositorioBase<Pagos> repositorio = new BLL.RepositorioBase<Pagos>();
                var lista = repositorio.GetList(x => true);

                MyReportViewer.ProcessingMode = ProcessingMode.Local;
                MyReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reportes\ListadoPagos.rdlc");

                MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Pagos", lista));
                MyReportViewer.LocalReport.Refresh();

            }
        }
    }
}