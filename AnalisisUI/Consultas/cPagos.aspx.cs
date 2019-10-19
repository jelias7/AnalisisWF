using AnalisisUI.Utilitarios;
using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnalisisUI.Consultas
{
    public partial class cPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DesdeFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                HastaFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Pagos, bool>> filtros = x => true;
            RepositorioBase<Pagos> repositorio = new RepositorioBase<Pagos>();

            DateTime Desde = Utils.ToDateTime(DesdeFecha.Text);
            DateTime Hasta = Utils.ToDateTime(HastaFecha.Text);

            int id;
            id = Utils.ToInt(CriterioTextBox.Text);


                switch (FiltroDropDown.SelectedIndex)
                {
                    case 0: //id
                    filtros = c => c.PagosId == id;
                    break;
                    case 1: //AnalisisID                  
                        filtros = c => c.AnalisisId == id;
                        break;
                    case 2: //Pagado
                        filtros = c => c.Pagado == id;
                        break;
                    case 3: //todo
                        break;
                }
            Grid.DataSource = repositorio.GetList(filtros);
            Grid.DataBind();
        }
    }
}