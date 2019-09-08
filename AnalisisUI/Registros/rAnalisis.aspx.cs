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
                LimpiarAnalisis();
                LimpiarTiposAnalisis();

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
                        //LlenaCampo(AnalisisBuscado);
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
            if (ViewState["Analisis"] != null)
            {
                Grid.DataSource = ((Analisis)ViewState["Analisis"]).Detalle;
                Grid.DataBind();
            }
        }
        private void LimpiarAnalisis()
        {
            IDTextBox.Text = "0";
            FechaTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            PacienteDropDown.SelectedIndex = 0;
            TiposAnalisisDropDown.SelectedIndex = 0;
            ResultadoTextBox.Text = string.Empty;
            this.BindGrid();
        }
        private void LimpiarTiposAnalisis()
        {
            TiposIdTextBox.Text = "0";
            AnalisisTextBox.Text = string.Empty;
        }
        private bool TiposExisteEnLaBaseDeDatos()
        {
            RepositorioBase<TiposAnalisis> Repositorio = new RepositorioBase<TiposAnalisis>();
            TiposAnalisis Tipos = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));
            return (Tipos != null);
        }
        private TiposAnalisis LlenaClase()
        {
            TiposAnalisis Tipos = new TiposAnalisis();

            Tipos.TiposId = Utils.ToInt(TiposIdTextBox.Text);
            Tipos.Analisis = AnalisisTextBox.Text;

            return Tipos;
        }
        private void LlenaCampo(TiposAnalisis Tipos)
        {
            TiposIdTextBox.Text = Tipos.TiposId.ToString();
            AnalisisTextBox.Text = Tipos.Analisis;
        }
        protected void AgregarGrid_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            Analisis.Detalle.Add(new AnalisisDetalle(TiposAnalisisDropDown.SelectedItem.ToString(), ResultadoTextBox.Text));

            ViewState["Detalle"] = Analisis.Detalle;

            this.BindGrid();

            Grid.Columns[1].Visible = false;

            ResultadoTextBox.Text = string.Empty;
        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Fila = e.RowIndex;

            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            Analisis.Detalle.RemoveAt(Fila);

            ViewState["Detalle"] = Analisis.Detalle;

            this.BindGrid();

            ResultadoTextBox.Text = string.Empty;
        }

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.DataSource = ViewState["Detalle"];

            Grid.PageIndex = e.NewPageIndex;

            Grid.DataBind();
        }

        protected void TiposGuardarButton_Click(object sender, EventArgs e)
        {
            TiposAnalisis Tipo = new TiposAnalisis();
            RepositorioBase<TiposAnalisis> Repositorio = new RepositorioBase<TiposAnalisis>();

            bool paso = false;

            Tipo = LlenaClase();

            if (Utils.ToInt(TiposIdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(Tipo);
                LimpiarTiposAnalisis();
            }
            else
            {
                if (!TiposExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this, "Error al guardar.", "Error", "error");
                    return;
                }
                paso = Repositorio.Modificar(Tipo);
                LimpiarTiposAnalisis();
            }

            if (paso)
            {
                Utils.ShowToastr(this, "Se ha guardado exitosamente.", "Exito", "success");
                return;
            }
            else
                Utils.ShowToastr(this, "Error al guardar.", "Error", "error");
        }

        protected void TiposEliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TiposAnalisis> Repositorio = new RepositorioBase<TiposAnalisis>();

            var tiposAnalisis = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));

            if (tiposAnalisis != null)
            {
                if (Repositorio.Eliminar(Utils.ToInt(TiposIdTextBox.Text)))
                {
                    Utils.ShowToastr(this, "Se ha eliminado.", "Exito", "success");
                }
                else
                    Utils.ShowToastr(this, "No se ha podido eliminar.", "Error", "error");
            }
            else
                Utils.ShowToastr(this, "No se ha podido eliminar.", "Error", "error");

            LimpiarTiposAnalisis();
        }

        protected void TiposBuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TiposAnalisis> Repositorio = new RepositorioBase<TiposAnalisis>();

            TiposAnalisis Tipos = new TiposAnalisis();

            Tipos = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));

            if (Tipos != null)
                LlenaCampo(Tipos);
            else
            {
                Utils.ShowToastr(this, "No se ha encontrado.", "Advertencia", "warning");
                LimpiarTiposAnalisis();
            }    
        }

        protected void TiposNuevoButton_Click(object sender, EventArgs e)
        {
            LimpiarTiposAnalisis();
        }
    }
}