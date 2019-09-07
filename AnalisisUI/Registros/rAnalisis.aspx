﻿<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="rAnalisis.aspx.cs" Inherits="AnalisisUI.Registros.rAnalisis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="panel panel-primary">
        <div class="panel-body">
              <div class="form-horizontal col-md-14" role="form">
                  <%--ID & Fecha--%>
                <div class="form-group row">
                    <label for="ID" class="col-sm-1 col-form-label">ID</label>
                    <div class="col-md-1 col-sm-2 col-xs-4">
                        <asp:TextBox type="number" ID="IDTextBox" runat="server" min=0 class="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-2 col-xs-4">
                         <asp:LinkButton ID="BuscarButton" CssClass="btn btn-dark btn-block btn-md" CausesValidation="False" runat="server" Text="Buscar"></asp:LinkButton>
                    </div>    
                    <label for="Fecha" class="col-xs-1 col-form-label">Fecha</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="FechaTextBox" type="date" runat="server" Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFecha" runat="server" MaxLength="200" ControlToValidate="FechaTextBox" ErrorMessage="Campo obligatorio" ForeColor="Black" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo obligatorio"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <%--Paciente--%>
                <div class="form-group row">
                    <label for="Paciente" class="col-sm-1 col-form-label">Paciente</label>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="PacienteDropDown" CssClass="form-control input-sm"></asp:DropDownList>                 
                    </div>
                </div>

                  <%--DETALLE--%>
                  <div class="form-group row">
                    <label for="TiposAnalisis" class="col-sm-1 col-form-label">Tipo</label>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="TiposAnalisisDropDown" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                    <div class="col-xs-1">
                        <asp:LinkButton runat="server" ID="TiposModal" CausesValidation="false" CssClass="btn btn-outline-success btn-md" Text="+"></asp:LinkButton>
                    </div>
                      <label for="Resultado" class="col-sm-1 col-form-label">Resultado</label>
                      <div class="col-md-2">
                        <asp:TextBox ID="ResultadoTextBox" runat="server" Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVResultado" runat="server" MaxLength="200" ControlToValidate="ResultadoTextBox" ErrorMessage="Campo obligatorio" ForeColor="Black" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo obligatorio"></asp:RequiredFieldValidator>
                      </div>
                      <div class="col-sm-2">
                        <asp:LinkButton runat="server" ID="AgregarGrid" CausesValidation="false" CssClass="btn btn-outline-info btn-md" Text="Agregar" OnClick="AgregarGrid_Click"></asp:LinkButton>
                    </div>
                </div>
        <div class="col-md-9">
        <asp:Gridview ID="Grid" runat="server" class="table table-condensed table-responsive" ShowHeaderWhenEmpty="false" GridLines="None" DataKeyNames="DetalleId" Width="100%" ViewStateMode="Enabled" AutoGenerateDeleteButton="True">
        <EmptyDataTemplate><div style="text-align:center">No hay datos.</div></EmptyDataTemplate>

        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#3399ff" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />


         </asp:Gridview>

         </div>
               </div>
            </div>
        

           <div class="panel-footer">
            <div class="text-center">
                <div class="form-group" style="display: inline-block">

                    <asp:Button Text="Nuevo" class="btn btn-warning btn-sm" style="color:#fff" runat="server" CausesValidation="False" ID="NuevoButton" />
                    <asp:Button Text="Guardar" class="btn btn-success btn-sm" runat="server" ID="GuardarButton"/>
                    <asp:Button Text="Eliminar" class="btn btn-danger btn-sm" runat="server" CausesValidation="False" ID="EliminarButton" />

                </div>
            </div>

        </div>
    </div>
</asp:Content>