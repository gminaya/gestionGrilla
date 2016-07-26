<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="colocacion.aspx.cs" Inherits="gestionSarmiento.pages.colocacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/content/jsForm/colocacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    </br>
    <hr class="thin bg-grayLighter" />
    <div class="cell auto-size padding20 bg-white" id="cell-content">
        <div class="grid">
            <div class="row">
                <div class="cells2">
                    <div class="cell place-left">
                        <h4 class="text-light no-margin no-padding">Asignar colocación</h4>
                    </div>
                    <div class="cell place-right">
                        <div class="">
                            <ul class="breadcrumbs no-margin no-padding">
                                <li><a href="/inicio.aspx"><span class="icon mif-home"></span></a></li>
                                <li><a href="#">Procesos</a></li>
                                <li><a href="#">Colocación</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr class="thin bg-grayLighter" />
        <div class="place-right">
        </div>
        <div class="container">
            </br>
        </br>
            <hr class="bg-gray">
            <div class="grid">
                <div class="row cells5 no-padding no-margin">
                    <div class="cell colspan2">
                        <div class="">
                            <div class="listview-outlook" data-role="listview">
                                <a class="list " href="#">
                                    <div class="list-content">
                                        <span class="list-title"><small class="">Circuito: <span id="" class="letra18 text-light nombreCircuito">Ningún circuito seleccionado</span> </small></span>
                                        <span class="list-subtitle"><small>Cliente: <span id="" class="letra18 text-light nombreCliente uppercase"></span></small></span>
                                        <span class="list-remark"></span>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="cell">
                        <div class="input-control text" data-role="datepicker">
                            <label for="fechaInicio">Inicio colocación</label>
                            <input id="fechaInicio" type="text" />
                            <button class="button"><span class="mif-calendar"></span></button>
                        </div>
                    </div>
                    <div class="cell ">
                        <div class="input-control text" data-role="datepicker">
                            <label for="fechaFin">Fin colocación</label>
                            <input id="fechaFin" type="text" />
                            <button class="button"><span class="mif-calendar"></span></button>
                        </div>
                    </div>


                    <div class="cell place-right">
                        <button id="btnConsultarCircuito" class="image-button fondoNaranja  fg-white text-light">Seleccionar circuito<span class="icon mif-search fondoIcono bg-active-darkBlue fg-white text-light"></span></button>
                    </div>
                </div>
                <hr class="bg-gray">
                <input id="idCircuito" type="text" hidden="hidden" />
                <input id="idCliente" type="text" hidden="hidden" />
                <div class="row cells12">
                    <div class="cell">
                        <div class="input-control text full-size" data-role="input">
                            <input id="idElemento" type="text" placeholder="ID" title="ID elemento" disabled="disabled" readonly="readonly" />
                            <button class="button helper-button clear"><span class="mif-cross"></span></button>
                        </div>
                    </div>
                    <div class="cell colspan3">
                        <div class="input-control text full-size" data-role="input">
                            <input id="Elento" type="text" placeholder="Elemento" title="Elemento" disabled="disabled" readonly="readonly" />
                            <button class="button helper-button clear"><span class="mif-cross"></span></button>
                        </div>
                    </div>
                    <div class="cell colspan8">
                        <div class="input-control text full-size" data-role="input">
                            <input id="arte" type="text" placeholder="Arte" title="Arte" />
                            <button class="button helper-button clear"><span class="mif-cross"></span></button>
                        </div>
                    </div>
                </div>
                <div class="row cell no-padding no-margin">
                    <div class="place-right ">
                        <button id="btnAsignar" class="image-button fondoNaranja  fg-white text-light">Guardar
                            <span class="icon mif-floppy-disk fondoIcono bg-active-darkBlue fg-white text-light"></span>

                        </button>
                    </div>
                </div>
            </div>

        </div>
    <%--Tabla detalle circuito--%>
        <div class="fondoPrincipal">
            <hr class="thin bg-grayLighter" />
            <h5 class="text-light fg-white align-center">Elementos del circuito <span class="nombreCircuito uppercase"></span></h5>
            <hr class="thin bg-grayLighter" />
        </div>
        <div id="consultaDetalleCircuito">Ningún circuito seleccionado</div>
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
</asp:Content>
