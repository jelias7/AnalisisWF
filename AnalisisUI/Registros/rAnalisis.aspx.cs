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

                ViewState["Analisis"] = new Analisis();
                BindGrid();
            }
        }
        void MostrarMensaje(TiposMensaje tipo, string mensaje)
        {
            Mensaje.Text = mensaje;

            if (tipo == TiposMensaje.Success)
                Mensaje.CssClass = "alert-success";
            else if (tipo == TiposMensaje.Error)
                Mensaje.CssClass = "alert-danger";
            else
                Mensaje.CssClass = "alert-warning";
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
            TiposAnalisisDropDown.DataValueField = "Precio";
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
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            PacienteDropDown.SelectedIndex = 0;
            TiposAnalisisDropDown.SelectedIndex = 0;
            ResultadoTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
            BalanceTextBox.Text = string.Empty;
            Grid.DataSource = null;
            Grid.DataBind();
        }
        private void LimpiarTiposAnalisis()
        {
            TiposIdTextBox.Text = "0";
            AnalisisTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
            TiposAnalisisFechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private bool TiposExisteEnLaBaseDeDatos()
        {
            RepositorioBase<TiposAnalisis> Repositorio = new RepositorioBase<TiposAnalisis>();
            TiposAnalisis Tipos = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));
            return (Tipos != null);
        }
        private bool AnalisisExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();
            Analisis Analisis = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));
            return (Analisis != null);
        }
        private TiposAnalisis TiposLlenaClase()
        {
            TiposAnalisis Tipos = new TiposAnalisis();

            Tipos.TiposId = Utils.ToInt(TiposIdTextBox.Text);
            Tipos.Analisis = AnalisisTextBox.Text;
            Tipos.Precio = Utils.ToDecimal(PrecioTextBox.Text);
            Tipos.Fecha = Utils.ToDateTime(TiposAnalisisFechaTextBox.Text);

            return Tipos;
        }
        private Analisis AnalisisLlenaClase()
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];
            Analisis.AnalisisId = Utils.ToInt(IDTextBox.Text);
            Analisis.Paciente = PacienteDropDown.SelectedItem.ToString();
            Analisis.Balance = Utils.ToDecimal(BalanceTextBox.Text);
            Analisis.Monto = Utils.ToDecimal(MontoTextBox.Text);
            Analisis.Fecha = Utils.ToDateTime(FechaTextBox.Text);

            return Analisis;
        }
        private void LlenaCampo(TiposAnalisis Tipos)
        {
            TiposIdTextBox.Text = Tipos.TiposId.ToString();
            AnalisisTextBox.Text = Tipos.Analisis;
            PrecioTextBox.Text = Tipos.Precio.ToString();
            TiposAnalisisFechaTextBox.Text = Tipos.Fecha.ToString("yyyy-MM-dd");
        }
        private void LlenaCampo(Analisis Analisis)
        {
            ((Analisis)ViewState["Analisis"]).Detalle = Analisis.Detalle;
            IDTextBox.Text = Analisis.AnalisisId.ToString();
            FechaTextBox.Text = Analisis.Fecha.ToString("yyyy-MM-dd");
            PacienteDropDown.SelectedValue = Analisis.Paciente;
            MontoTextBox.Text = Analisis.Monto.ToString();
            BalanceTextBox.Text = Analisis.Balance.ToString();
            this.BindGrid();
        }
        protected void AgregarGrid_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            Analisis.Detalle.Add(new AnalisisDetalle(
                Utils.ToInt(TiposAnalisisDropDown.SelectedValue),
                ResultadoTextBox.Text,
                Convert.ToDecimal(TiposAnalisisDropDown.SelectedValue),
                Utils.ToDateTime(FechaTextBox.Text)));

            ViewState["Detalle"] = Analisis.Detalle;

            this.BindGrid();

            Grid.Columns[1].Visible = false;

            ResultadoTextBox.Text = string.Empty;
            
            foreach(var item in Analisis.Detalle)
                MontoTextBox.Text = item.Precio.ToString();

            BalanceTextBox.Text = MontoTextBox.Text;
        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            ViewState["Detalle"] = Analisis.Detalle;

            int Fila = e.RowIndex;

            Analisis.Detalle.RemoveAt(Fila);

            this.BindGrid();

            ResultadoTextBox.Text = string.Empty;

            foreach (var item in Analisis.Detalle)
                MontoTextBox.Text = item.Precio.ToString();

            BalanceTextBox.Text = MontoTextBox.Text;
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

            Tipo = TiposLlenaClase();

            if (Utils.ToInt(TiposIdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(Tipo);
                LimpiarTiposAnalisis();
            }
            else
            {
                if (!TiposExisteEnLaBaseDeDatos())
                {

                    MostrarMensaje(TiposMensaje.Error, "Error al guardar.");
                    return;
                }
                paso = Repositorio.Modificar(Tipo);
                LimpiarTiposAnalisis();
            }

            if (paso)
            {
                MostrarMensaje(TiposMensaje.Success, "Exito al guardar.");
                return;
            }
            else
                MostrarMensaje(TiposMensaje.Error, "Error al guardar.");
        }

        protected void TiposEliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TiposAnalisis> Repositorio = new RepositorioBase<TiposAnalisis>();

            var tiposAnalisis = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));

            if (tiposAnalisis != null)
            {
                if (Repositorio.Eliminar(Utils.ToInt(TiposIdTextBox.Text)))
                {
                    MostrarMensaje(TiposMensaje.Success, "Eliminado con exito.");
                }
                else
                    MostrarMensaje(TiposMensaje.Error, "No se ha podido eliminar.");
            }
            else
                MostrarMensaje(TiposMensaje.Error, "No se ha podido eliminar.");

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
                MostrarMensaje(TiposMensaje.Warning, "Problemas inesperados.");
                LimpiarTiposAnalisis();
            }    
        }

        protected void TiposNuevoButton_Click(object sender, EventArgs e)
        {
            LimpiarTiposAnalisis();
        }

        protected void AnalisisNuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void AnalisisBuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();

            Analisis Analisis = new Analisis();

            Analisis = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (Analisis != null)
                LlenaCampo(Analisis);
            else
            {
                MostrarMensaje(TiposMensaje.Warning, "Problemas inesperados.");
                LimpiarTiposAnalisis();
            }
        }

        protected void AnalisisGuardarButton_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();
            bool paso = false;

            Analisis = AnalisisLlenaClase();

            if (Utils.ToInt(IDTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(Analisis);
                LimpiarAnalisis();
            }
            else
            {
                if (!AnalisisExisteEnLaBaseDeDatos())
                {

                    MostrarMensaje(TiposMensaje.Error, "Error al guardar.");
                    return;
                }
                paso = Repositorio.Modificar(Analisis);
                LimpiarAnalisis();
            }

            if (paso)
            {
                MostrarMensaje(TiposMensaje.Success, "Exito al guardar.");
                return;
            }
            else
                MostrarMensaje(TiposMensaje.Error, "Error al guardar.");

            LimpiarAnalisis();
        }

        protected void AnalisisEliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();

            var Analisis = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (Analisis != null)
            {
                if (Repositorio.Eliminar(Utils.ToInt(IDTextBox.Text)))
                {
                    MostrarMensaje(TiposMensaje.Success, "Eliminado con exito.");
                }
                else
                    MostrarMensaje(TiposMensaje.Error, "No se ha podido eliminar.");
            }
            else
                MostrarMensaje(TiposMensaje.Error, "No se ha podido eliminar.");

            LimpiarTiposAnalisis();
        }
    }
}