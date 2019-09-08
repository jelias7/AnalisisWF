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
                         <asp:LinkButton ID="AnalisisBuscarButton" CssClass="btn btn-dark btn-block btn-md" CausesValidation="false" runat="server" Text="Buscar" OnClick="AnalisisBuscarButton_Click"></asp:LinkButton>
                    </div>    
                    <label for="Fecha" class="col-xs-1 col-form-label">Fecha</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="FechaTextBox" type="date" runat="server" Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFecha" runat="server" MaxLength="200" ValidationGroup="Analisis" ControlToValidate="FechaTextBox" ErrorMessage="Campo obligatorio" ForeColor="Black" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo obligatorio"></asp:RequiredFieldValidator>
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
                        <asp:LinkButton runat="server" ID="TiposModal" CausesValidation="false" CssClass="btn btn-outline-success btn-md" data-toggle="modal" data-target="#rTiposAnalisis" Text="+"></asp:LinkButton>
                    </div>
                      <label for="Resultado" class="col-md-1 col-form-label">Resultado</label>
                      <div class="col-md-2">
                        <asp:TextBox ID="ResultadoTextBox" runat="server" Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVResultado" runat="server" ValidationGroup="Analisis" MaxLength="200" ControlToValidate="ResultadoTextBox" ErrorMessage="Campo obligatorio" ForeColor="Black" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo obligatorio"></asp:RequiredFieldValidator>
                      </div>
                      <div class="col-sm-2">
                        <asp:LinkButton runat="server" ID="AgregarGrid" ValidationGroup="Analisis" CssClass="btn btn-outline-info btn-md" Text="Agregar" OnClick="AgregarGrid_Click"></asp:LinkButton>
                    </div>
                </div>

                <%--GRID--%>
                <asp:GridView ID="Grid" CssClass=" col-md-offset-4 col-sm-offset-4" runat="server" DataKeyNames="DetalleId" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="false" AutoGenerateDeleteButton="true" CellPadding="4" ForeColor="#333333" GridLines="None" Width="767px" AutoGenerateColumns="false" OnRowDeleting="Grid_RowDeleting" OnPageIndexChanging="Grid_PageIndexChanging">                         
                    <Columns>
                        <asp:BoundField DataField="Analisis" HeaderText="Tipo de Analisis" /><asp:BoundField />
                        <asp:BoundField DataField="Resultado" HeaderText="Resultado" /><asp:BoundField />
                    </Columns>     
                    <EmptyDataTemplate><div style="text-align:center">No hay datos en el Grid.</div></EmptyDataTemplate>
                         <AlternatingRowStyle BackColor="White" />

                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

            <%--Registro de Tipos de Analisis--%>
            <div class="modal fade" id="rTiposAnalisis" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
            <div class="modal-header" style="background-color:#5d748a">
                <h5 class="modal-title" id="exampleModalLongTitle" style="color:#fff">Tipos de Analisis</h5>
            </div>

            <%--CONTENIDO--%>
            <div class="modal-body">
             <div class="container-fluid">
                 <div class="form-group row">
                 <label for="TiposId" class="col-sm-2 col-form-label">ID</label>
                 <div class="col-md-3 col-sm-2 col-xs-4">
                   <asp:TextBox type="number" ID="TiposIdTextBox" runat="server" min=0 class="form-control input-sm"></asp:TextBox>
                 </div>
                 <div class="col-md-3 col-sm-2 col-xs-4">
                 <asp:LinkButton ID="TiposBuscarButton" CssClass="btn btn-dark btn-block btn-md" CausesValidation="false" runat="server" Text="Buscar" OnClick="TiposBuscarButton_Click"></asp:LinkButton>
                 </div> 
                 </div>
                 <div class="form-group row">
                   <label for="Analisis" class="col-sm-2 col-form-label">Analisis</label>
                   <div class="col-md-6">
                    <asp:TextBox ID="AnalisisTextBox" runat="server" Class="form-control input-sm"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVAnalisis" runat="server" ValidationGroup="Tipos" MaxLength="200" ControlToValidate="AnalisisTextBox" ErrorMessage="Campo obligatorio" ForeColor="Black" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo obligatorio"></asp:RequiredFieldValidator>
                   </div>
                 </div>
                   <div class="form-group row">
                   <label for="Fecha" class="col-sm-2 col-form-label">Fecha</label>
                   <div class="col-md-6">
                    <asp:TextBox ID="TiposAnalisisFechaTextBox" type="date" runat="server" Class="form-control input-sm"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFV" runat="server" ValidationGroup="Tipos" MaxLength="200" ControlToValidate="TiposAnalisisFechaTextBox" ErrorMessage="Campo obligatorio" ForeColor="Black" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo obligatorio"></asp:RequiredFieldValidator>
                   </div>
                 </div>
             </div>
            </div>

            <div class="modal-footer">
            <asp:Button Text="Nuevo" class="btn btn-warning btn-sm" style="color:#FFF" runat="server" CausesValidation="false" id="TiposNuevoButton" OnClick="TiposNuevoButton_Click" />
            <asp:Button Text="Guardar" class="btn btn-success btn-sm" runat="server" ValidationGroup="Tipos" id="TiposGuardarButton" OnClick="TiposGuardarButton_Click" />
            <asp:Button Text="Eliminar" class="btn btn-danger btn-sm" runat="server" CausesValidation="false" id="TiposEliminarButton" OnClick="TiposEliminarButton_Click" />
            </div>

            </div>
            </div>
            </div>

         <%--FIN--%>

         </div>
         <asp:Label ID="Mensaje" runat="server" CssClass="col-form-label-lg" Text=""></asp:Label>
        </div>
        
          <br />
           <div class="panel-footer">
            <div class="text-center">
                <div class="form-group" style="display: inline-block">

                    <asp:Button Text="Nuevo" class="btn btn-warning btn-sm" CausesValidation="false" style="color:#fff" runat="server" ID="AnalisisNuevoButton" OnClick="AnalisisNuevoButton_Click" />
                    <asp:Button Text="Guardar" class="btn btn-success btn-sm" runat="server" ID="AnalisisGuardarButton" OnClick="AnalisisGuardarButton_Click"/>
                    <asp:Button Text="Eliminar" class="btn btn-danger btn-sm" CausesValidation="false" runat="server" ID="AnalisisEliminarButton" OnClick="AnalisisEliminarButton_Click" />

                </div>
            </div>

        </div>
    </div>
</asp:Content>
