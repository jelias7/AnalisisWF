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
    public partial class cAnalisis : System.Web.UI.Page
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
            Expression<Func<Analisis, bool>> filtros = x => true;
            RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();

            DateTime Desde = Utils.ToDateTime(DesdeFecha.Text);
            DateTime Hasta = Utils.ToDateTime(HastaFecha.Text);

            int id;
            id = Utils.ToInt(CriterioTextBox.Text);

            if (CheckBoxFecha.Checked == true)
            {
                switch (FiltroDropDown.SelectedIndex)
                {
                    case 0: //Todo
                        break;
                    case 1: //ID                  
                        filtros = c => c.AnalisisId == id && c.Fecha >= Desde && c.Fecha <= Hasta;
                        break;
                    case 2: //Paciente
                        filtros = c => c.Paciente.Contains(CriterioTextBox.Text) && c.Fecha >= Desde && c.Fecha <= Hasta;
                        break;
                }
            }
            else
            {
                switch (FiltroDropDown.SelectedIndex)
                {
                    case 0: //Todo
                        break;
                    case 1: //ID                  
                        filtros = c => c.AnalisisId == id;
                    break;
                    case 2: //Paciente
                        filtros = c => c.Paciente.Contains(CriterioTextBox.Text);
                    break;
                }
            }
            Grid.DataSource = repositorio.GetList(filtros);
            Grid.DataBind();
        }
    }
}