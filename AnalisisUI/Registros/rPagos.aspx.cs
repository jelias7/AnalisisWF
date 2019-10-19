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
    public partial class rPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ValoresDeDropdowns();

                ViewState["Pagos"] = new Pagos();
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

            RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();
            var list = new List<Analisis>();
            list = repositorio.GetList(p => true);
            AnalisisDropDown.DataSource = list;
            AnalisisDropDown.DataValueField = "AnalisisId";
            AnalisisDropDown.DataTextField = "AnalisisId";
            AnalisisDropDown.DataBind();
        }
        protected void BindGrid()
        {
            if (ViewState["Pagos"] != null)
            {
                Grid.DataSource = ((Pagos)ViewState["Pagos"]).Detalle;
                Grid.DataBind();
            }
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Pagos> Repositorio = new RepositorioBase<Pagos>();
            Pagos P = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));
            return (P != null);
        }
        private Pagos LlenaClase()
        {
            Pagos P = new Pagos();

            P = (Pagos)ViewState["Pagos"];
            P.PagosId = Utils.ToInt(IDTextBox.Text);
            P.AnalisisId = Utils.ToInt(AnalisisDropDown.SelectedValue);
            P.Pagado = Utils.ToDecimal(PagadoTextBox.Text);

            return P;
        }
        private void LlenaCampo(Pagos P)
        {
            ((Pagos)ViewState["Pagos"]).Detalle = P.Detalle;
            IDTextBox.Text = P.PagosId.ToString();
            AnalisisDropDown.SelectedValue = P.AnalisisId.ToString();
            PagadoTextBox.Text = P.Pagado.ToString();
            this.BindGrid();
        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Pagos P = new Pagos();

            P = (Pagos)ViewState["Pagos"];

            ViewState["Detalle"] = P.Detalle;

            int Fila = e.RowIndex;

            P.Detalle.RemoveAt(Fila);

            this.BindGrid();

            MontoPagadoTextBox.Text = string.Empty;
            decimal Total = 0;
            foreach (var item in P.Detalle.ToList())
            {
                Total += item.MontoPagado;
            }
            PagadoTextBox.Text = Total.ToString();
        }

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.DataSource = ViewState["Detalle"];

            Grid.PageIndex = e.NewPageIndex;

            Grid.DataBind();
        }

        protected void AgregarGrid_Click(object sender, EventArgs e)
        {
            Pagos P = new Pagos();
            P = (Pagos)ViewState["Pagos"];
            Analisis A = new RepositorioBase<Analisis>().Buscar(Utils.ToInt(AnalisisDropDown.SelectedValue));

            int id = Utils.ToInt(AnalisisDropDown.SelectedValue);

            foreach (var item in P.Detalle.ToList())
            {
                if (id == item.AnalisisId)
                {
                    MostrarMensaje(TiposMensaje.Warning, "Este Analisis ya esta en el grid.");
                    return;
                }
            }

            P.Detalle.Add(new PagosDetalle(
                Utils.ToInt(AnalisisDropDown.SelectedValue),
                A.Balance,
                Utils.ToDecimal(MontoPagadoTextBox.Text)
                ));

            ViewState["Detalle"] = P.Detalle;

            this.BindGrid();

            Grid.Columns[1].Visible = false;

            MontoPagadoTextBox.Text = string.Empty;

            decimal Total = 0;
            foreach (var item in P.Detalle.ToList())
            {
                Total += item.MontoPagado;
            }
            PagadoTextBox.Text = Total.ToString();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            Pagos P = new Pagos();
            bool paso = false;


            P = LlenaClase();

            if (Utils.ToInt(IDTextBox.Text) == 0)
            {
                paso = PagosBLL.Guardar(P);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {

                    MostrarMensaje(TiposMensaje.Error, "Error al guardar.");
                    return;
                }
                paso = PagosBLL.Modificar(P);
                Response.Redirect(Request.RawUrl);
            }

            if (paso)
            {
                MostrarMensaje(TiposMensaje.Success, "Exito al guardar.");
                return;
            }
            else
                MostrarMensaje(TiposMensaje.Error, "Error al guardar.");
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Pagos> Repositorio = new RepositorioBase<Pagos>();

            var P = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (P != null)
            {
                if (PagosBLL.Eliminar(Utils.ToInt(IDTextBox.Text)))
                {
                    MostrarMensaje(TiposMensaje.Success, "Eliminado con exito.");
                    Response.Redirect(Request.RawUrl);
                }
                else
                    MostrarMensaje(TiposMensaje.Error, "No se ha podido eliminar.");
            }
            else
                MostrarMensaje(TiposMensaje.Error, "No se ha podido eliminar.");

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Pagos> Repositorio = new RepositorioBase<Pagos>();

            Pagos P = new Pagos();

            P = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (P != null)
                LlenaCampo(P);
            else
            {
                MostrarMensaje(TiposMensaje.Warning, "Problemas inesperados.");

            }
        }
    }
}