using AnalisisUI.Utilitarios;
using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnalisisUI.Registros
{
    public partial class rAnalisis : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ValoresDeDropdowns();
                Limpiar();

                int ID = Utils.ToInt(Request.QueryString["id"]);

                if (ID > 0)
                {
                    RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();

                    var AnalisisBuscado = repositorio.Buscar(ID);

                    if (AnalisisBuscado == null)
                    {
                        // MostrarMensaje(TiposMensaje.Error, "Registro no encontrado");
                    }
                    else
                    {
                        // LlenaCampo(AnalisisBuscado);
                    }
                }

                ViewState["Analisis"] = new Analisis();
                BindGrid();
            }
        }
        private void ValoresDeDropdowns()
        {
            RepositorioBase<Pacientes> db = new RepositorioBase<Pacientes>();
            var listado = new List<Pacientes>();
            listado = db.GetList(p => true);
            PacienteDropDown.DataSource = listado;
            PacienteDropDown.DataValueField = "PacienteId";
            PacienteDropDown.DataTextField = "Nombres";
            PacienteDropDown.DataBind();

            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            var list = new List<TiposAnalisis>();
            list = repositorio.GetList(p => true);
            TiposAnalisisDropDown.DataSource = list;
            TiposAnalisisDropDown.DataValueField = "TiposId";
            TiposAnalisisDropDown.DataTextField = "Analisis";
            TiposAnalisisDropDown.DataBind();
        }
        protected void BindGrid()
        {
            if (ViewState["Analisis"] != null) {
                Grid.DataSource = ((Analisis)ViewState["Analisis"]).Detalle;
                Grid.DataBind();
            }
        }
        private void Limpiar()
        {
            IDTextBox.Text = "0";
            FechaTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            PacienteDropDown.SelectedIndex = 0;
            TiposAnalisisDropDown.SelectedIndex = 0;
            ResultadoTextBox.Text = string.Empty;
            this.BindGrid();
        }
        protected void AgregarGrid_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            Analisis.Detalle.Add(new AnalisisDetalle(TiposAnalisisDropDown.SelectedValue, ResultadoTextBox.Text));

            ViewState["Detalle"] = Analisis.Detalle;

            this.BindGrid();
        }
    }
}