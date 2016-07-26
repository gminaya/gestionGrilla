<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="grillaOperaciones.aspx.cs" Inherits="gestionSarmiento.pages.grillaOperaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/content/jsForm/grillaOperaciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <a onclick="grillaVallas(2,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                <span class="icon mif-warning "></span>
                <span class="title">Pendientes</span>
                <span class="badge">10</span>
            </a>
            <a onclick="grillaVallas(1,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                <span class="icon mif-checkmark"></span>
                <span class="title">Colocadas</span>
                <span class="badge">10</span>
            </a>
            <a onclick="grillaVallas(3,6)" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
                <span class="icon mif-notification"></span>
                <span class="title">No colocadas</span>
                <span class="badge">10</span>
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
                    <span class="icon mif-cogs"></span>
                    <span class="title">Brigada</span>
                    <span class="badge"></span>
                </button>
                <ul class="split-content d-menu place-right fg-white" data-role="dropdown">
                    <li onclick="showDialog('#dvNuevaBrigada')"><a href="#">Nueva</a></li>
                    <li id="btnConsultarBrigada"><a href="#">Seleccionar</a></li>
                </ul>
            </div>
        </div>
        <input id="idBrigada" name="idBrigada" hidden="hidden" type="text" />
        <section>
            <div class="tabcontrol tabs-bottom " data-role="tabcontrol">
                <div class="frames">
                    <%--tab grilla vallas--%>
                    <div class="frame bg-white no-padding" id="vallas">
                        <div id="dvGrilla"></div>
                    </div>
                    <div class="frame bg-white" id="frame_id2">
                        <div>
                        </div>
                    </div>
                    <%--tab detalle circuito--%>
                    <div class="frame bg-white no-padding" id="tabDetalleCircuito">
                        <div id="grupobntcircuito" data-role="group" data-group-type="one-state" data-button-style="class">
                            <a id="btnGuardaPDF" class="shortcut-button fondoNaranja bg-active-darkBlue fg-white">
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
                        </div>
                        <div class="fondoPrincipal">
                            <hr class="thin bg-grayLighter" />
                            <h4 class="text-light fg-white nombreBrigada"></h4>
                            <hr class="thin bg-grayLighter" />
                        </div>
                        <div id="consultaDetalleBrigada"></div>
                    </div>
                </div>
                <ul class="tabs fixed-bottom bg-darkerGray">
                    <li id="btnGrillaVallas"><a href="#vallas" class="fg-white">Grilla</a></li>
                    <li><a href="#frame_id2">Muebles</a></li>
                    <li id="btnDetalleBrigada"><a href="#tabDetalleCircuito"><small class="fg-gray">Brigada seleccionada: <span id="" class="fg-white letra18 text-light nombreBrigada">Ninguna brigada seleccionada</span> </small></a></li>
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

            <%--Dialog de nueva brigada--%>
            <div id="dvNuevaBrigada" class="padding20 dialog" data-role="dialog" data-overlay="true" data-overlay-color="op-dark" data-close-button="true" data-windows-style="false">
                <div>
                    <hr class="thin bg-grayLighter no-padding no-margin" />
                    <h2>Nueva brigada </h2>
                    <hr class="thin bg-grayLighter no-padding no-margin" />
                    </br>
                <div class="grid">
                    <div class="row cells2">
                        <div class="">
                            <div class="cell">
                                <div class="input-control text full-size" data-role="datepicker">
                                    <label for="fechaOperacion">Fecha de ejecución</label>
                                    <input id="fechaOperacion" type="text"/>
                                    <button class="button"><span class="mif-calendar"></span></button>
                                </div>
                            </div>
                            <div class="cell">
                                <div class="input-control text full-size" data-role="input">
                                    <input id="nombre" type="text" placeholder="Nombre de la brigada" />
                                    <button class="button helper-button clear"><span class="mif-cross"></span></button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="cell full-size align-center fg-white">
                            <a id="btnNuevaGrigada" class="command-button icon-right fg-white fondoNaranja">
                                <span class="icon mif-spinner4 fg-white"></span>
                                Crear brigada
                                 <small class="fg-white">Presione + para agrear elementos a esta grigada</small>
                            </a>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            <%-- FIN Dialog nueva brigada--%>

            <%--  Dialog seleccionar circuito--%>

            <div id="dvconsultaBrigada" style="height: 90% !important; overflow: scroll !important;" class="padding20 dialog" data-role="dialog"
                data-overlay="true" data-overlay-color="op-dark" data-close-button="true" data-windows-style="true" data-height="80%">
                <div class="fondoPrincipal">
                    <hr class="thin bg-grayLighter" />
                    <h4 class="text-light fg-white">Listado de brigadas activas </h4>
                    <hr class="thin bg-grayLighter" />
                </div>
                <div id="consultaBrigada"></div>
            </div>
            <%--  Dialog seleccionar circuito--%>

            <%--Report Viewer--%>
        </section>
    </div>
</asp:Content>
