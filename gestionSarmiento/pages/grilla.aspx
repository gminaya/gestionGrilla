<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="grilla.aspx.cs" Inherits="gestionSarmiento.pages.grilla" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/content/jsForm/grilla.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        </br>
    <hr class="thin bg-grayLighter" />
        <div class="cell auto-size padding20 bg-white" id="cell-content">
            <hr class="thin bg-grayLighter" />
            <div class="grid">
                <div class="row">
                    <div class="cells2">
                        <div class="cell place-left">
                            <h4 class="text-light no-margin no-padding">Grilla</h4>
                        </div>
                        <div class="cell place-right">
                            <div class="">
                                <ul class="breadcrumbs no-margin no-padding">
                                    <li><a href="#"><span class="icon mif-home"></span></a></li>
                                    <li><a href="#">Procesos</a></li>
                                    <li><a href="#">Grilla</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="thin bg-grayLighter" />
            <div id="grupobntGrillas" data-role="group" data-group-type="one-state" data-button-style="class">
                <a onclick="grillaVallas(1,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                    <span class="icon mif-checkmark mif-ani-hover-heartbeat"></span>
                    <span class="title">Disponibles</span>
                    <span class="badge"><%= contadorVallas(1) %></span>
                </a>
                <a onclick="grillaVallas(3,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                    <span class="icon mif-event-available"></span>
                    <span class="title">Reservadas</span>
                    <span class="badge"><%= contadorVallas(3) %></span>
                </a>
                <a onclick="grillaVallas(2,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                    <span class="icon mif-event-busy"></span>
                    <span class="title">Ocupadas</span>
                    <span class="badge"><%= contadorVallas(2) %></span>
                </a>
                <a onclick="grillaVallas(4,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                    <span class="icon mif-stack2"></span>
                    <span class="title">Todas</span>
                    <span class="badge">10</span>
                </a>
                <a onclick="showDialog('#dvfiltro')" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                    <span class="icon mif-filter"></span>
                    <span class="title">Otros filtros</span>
                    <span class="badge"></span>
                </a>
                <div class="dropdown-button ">
                    <button class=" dropdown-toggle shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                        <span class="icon mif-spinner5"></span>
                        <span class="title">Circuitos</span>
                        <span class="badge"></span>
                    </button>
                    <ul class="split-content d-menu place-right fg-white" data-role="dropdown">
                        <li onclick="showDialog('#dvNuevoCircuito')"><a href="#">Nuevo</a></li>
                        <li id="btnConsultarCircuito"><a href="#">Seleccionar</a></li>
                    </ul>
                </div>
            </div>
            <input id="idCircuito" name="idCircuito" hidden="hidden" type="text" />
            <section>
                <div class="tabcontrol tabs-bottom " data-role="tabcontrol">
                    <div class="frames">
                        <%--tab grilla vallas--%>
                        <div class="frame bg-white no-padding" id="vallas">
                            <div id="dvGrilla"></div>
                        </div>
                        <div class="frame bg-white" id="frame_id2">
                            <div>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                               
                                
                            </div>
                        </div>
                        <%--tab detalle circuito--%>
                        <div class="frame bg-white no-padding" id="tabDetalleCircuito">
                           
                            <asp:Button ID="Button1" runat="server" Text="Guardar PDF" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Guardar EXCEL" OnClick="Button2_Click" />
                      <%--      <div id="grupobntcircuito" data-role="group" data-group-type="one-state" data-button-style="class">
                                <a  class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                                    <span class="icon mif-file-pdf"></span>
                                    <span class="title">PDF</span>
                                    <span class="badge">Guardar en</span>
                                </a>
                                <a id="btnGuardaExcel" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                                    <span class="icon mif-file-excel"></span>
                                    <span class="title">Excel</span>
                                    <span class="badge">Guardar en</span>
                                </a>
                                <a class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                                    <span class="icon mif-printer"></span>
                                    <span class="title">Imprimir</span>
                                    <span class="badge"></span>
                                </a>
                            </div>--%>
                            <div class="fondoPrincipal">
                                <hr class="thin bg-grayLighter" />
                                <h4 class="text-light fg-white nombreCircuito"></h4>
                                <hr class="thin bg-grayLighter" />
                            </div>
                            <div id="consultaDetalleCircuito"></div>
                        </div>
                    </div>
                    <ul class="tabs fixed-bottom bg-darkerGray">
                        <li id="btnGrillaVallas"><a href="#vallas" class="fg-white">Vallas</a></li>
                        <li><a href="#frame_id2">Muebles</a></li>
                        <li id="btnDetalleCircuito"><a href="#tabDetalleCircuito"><small class="fg-gray">Circuito seleccionado: <span id="" class="fg-white letra18 text-light nombreCircuito">Ningún circuito seleccionado</span> Vallas<span id="numeroVallas" class="fg-white letra18"> 0</span>Mobiliario urbano <span id="numeroMuebles" class="fg-white letra18">0</span></small></a></li>
                    </ul>
                </div>
                <%--Dialog de escoger fechas--%>
                <div id="dvfiltro" class="padding20 dialog" data-role="dialog" data-overlay="true" data-overlay-color="op-dark" data-close-button="true" data-windows-style="false">
                    <div>
                        <h2>Filtrar grilla</h2>
                        <hr class="thin bg-grayLighter" />

                        <div>
                            <div class="input-control ">
                                <label class="fg-darkerGray">Por: </label>
                                <select id="filtro">
                                    <option value="0" disabled="disabled" selected="selected"></option>
                                    <option value="1">Fecha</option>
                                    <option value="2">Rango de fecha</option>
                                    <option value="3">Calle</option>
                                    <option value="4">Fecha y calle</option>
                                    <option value="5">Rando de fecha y calle</option>
                                </select>
                            </div>
                        </div>
                        </br>
                <div class="grid">
                    <div class="row">
                        <div class="cell">
                            <div class="input-control text" data-role="datepicker">
                                <label for="fechaInicio">Fecha Inicio</label>
                                <input id="fechaInicio" type="text" />
                                <button class="button"><span class="mif-calendar"></span></button>
                            </div>
                            <div class="input-control text" data-role="datepicker">
                                <label for="fechaFin">Fecha Fin</label>
                                <input id="fechaFin" type="text" />
                                <button class="button"><span class="mif-calendar"></span></button>
                            </div>
                            <div class="input-control text">
                                <label for="calle">Calle</label>
                                <input id="calle" type="text" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="cell full-size align-center fg-white">
                            <a id="btnFiltrarGrilla" class="command-button icon-right fg-white fondoNaranja">
                                <span class="icon mif-spinner4 fg-white"></span>
                                Cargar grilla
                                 <small class="fg-white">Se mostratarán los elementos con los parámetros establecidos</small>
                            </a>
                        </div>
                    </div>
                </div>
                    </div>
                </div>
                <%-- FIN Dialog de escoger fechas--%>

                <%--Dialog de Nuevo circuito--%>
                <div id="dvNuevoCircuito" class="padding20 dialog" data-role="dialog" data-overlay="true" data-overlay-color="op-dark" data-close-button="true" data-windows-style="false">
                    <div>
                        <hr class="thin bg-grayLighter no-padding no-margin" />
                        <h2>Nuevo circuito</h2>
                        <hr class="thin bg-grayLighter no-padding no-margin" />
                        </br>
                <div class="grid">
                    <div class="row cells2">
                        <div class="">
                            <div class="cell">
                                <div class="input-control full-size " data-role="input">
                                    <select id="idCliente">
                                        <option value="0" disabled="disabled" selected="selected">Cliente</option>
                                    </select>
                                </div>
                            </div>
                            <div class="cell">
                                <div class="input-control text full-size" data-role="input">
                                    <input id="nombre" type="text" placeholder="Nombre del circuito" />
                                    <button class="button helper-button clear"><span class="mif-cross"></span></button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="cell full-size align-center fg-white">
                            <a id="btnNuevoCircuito" class="command-button icon-right fg-white fondoNaranja">
                                <span class="icon mif-spinner4 fg-white"></span>
                                Crear circuito
                                 <small class="fg-white">Presione + para agrear elementos a este circiuto</small>
                            </a>
                        </div>
                    </div>
                </div>
                    </div>
                </div>
                <%-- FIN Dialog nuevo circuito--%>

                <%--  Dialog seleccionar circuito--%>

                <div id="dvconsultaCircuito" style="height: 90% !important; overflow: scroll !important;" class="padding20 dialog" data-role="dialog"
                    data-overlay="true" data-overlay-color="op-dark" data-close-button="true" data-windows-style="true" data-height="80%">
                    <div class="fondoPrincipal">
                        <hr class="thin bg-grayLighter" />
                        <h4 class="text-light fg-white">Listado de circuitos</h4>
                        <hr class="thin bg-grayLighter" />
                    </div>
                    <div id="consultaCircuito"></div>
                </div>
                <%--  Dialog seleccionar circuito--%>

                <%--Report Viewer--%>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Visible="False">
                    <LocalReport ReportPath="ReportCircuito.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="gestionSarmiento.DSreporteTableAdapters.DataTable1TableAdapter"></asp:ObjectDataSource>
            </section>
        </div>
    </form>
</asp:Content>
