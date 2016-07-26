<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="espacios.aspx.cs" Inherits="gestionSarmiento.pages.espacios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/content/jsForm/espacios.js"></script>

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
                        <h4 class="text-light no-margin no-padding">Administración de cierres</h4>
                    </div>
                    <div class="cell place-right">
                        <div class="">
                            <ul class="breadcrumbs no-margin no-padding">
                                <li><a href="#"><span class="icon mif-home"></span></a></li>
                                <li><a href="#">Administración</a></li>
                                <li><a href="#">Mantenimientos</a></li>
                                <li><a href="#">Cierres</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr class="thin bg-grayLighter" />
        <div class="place-right">
            <button id="btnBuscar" class="image-button fondoNaranja  fg-white text-light">Buscar cierre<span class="icon mif-search fondoIcono bg-active-darkBlue fg-white text-light"></span></button>
            <button class="image-button fondoNaranja  fg-white text-light">Limpiar<span class="icon mif-file-empty fondoIcono bg-active-darkBlue fg-white text-light"></span></button>
            <button id="btnGuardar" class="image-button fondoNaranja  fg-white text-light">Guargar<span class="icon mif-floppy-disk fondoIcono bg-active-darkBlue fg-white text-light"></span></button>
            <button class="image-button fondoNaranja  fg-white text-light">Inicio<span class="icon mif-home fondoIcono bg-active-darkBlue fg-white text-light"></span></button>
        </div>
        </br>
        </br>
        <div class="grid">
            <%--panel Cierre--%>
            <div class="panel collapsible" data-role="panel">
                <div class="heading bg-grayDark">
                    <span class="bg-white"></span>
                    <span class="title">Cierre</span>
                </div>
                <div class="content">
                    <%--           <div class="panel collapsible" data-role="panel">
                        <div class="heading fondoSecundario">
                            <span class="icon mif-tag fondoPrincipal"></span>
                            <span class="title text-light">Identificación</span>
                        </div>
                        <div class="content ">--%>
                    <div class="row cells5 no-padding no-margin">
                        <div class="cell">
                            <div class="input-control  full-size">
                                <select id="idPropietario" title="Tipo">
                                    <option value="0" disabled="disabled" selected="selected">Propietario</option>
                                </select>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="idCierreC" type="text" placeholder="ID" title="ID cierre" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="licencia" type="text" placeholder="Licencia" title="Licencia Cierre" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                        <div class="cell colspan2">
                            <div class="input-control text full-size" data-role="input">
                                <input id="descripcionC" type="text" placeholder="Descripcion" title="Descripción cierre" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                    </div>
                    <%--      </div>
                    </div>--%>
                    <%--          <div class="panel collapsible" data-role="panel">
                        <div class="heading fondoSecundario">
                            <span class="icon mif-location FondoPrincipal"></span>
                            <span class="title text-light">Ubicación</span>
                        </div>
                        <div class="content">--%>
                    <div class="row cells5 no-padding no-margin">
                        <div class="cell">
                            <div class="input-control  full-size">
                                <select id="ciudadC" title="Ciudad cierre">
                                    <option value="0" disabled="disabled" selected="selected">Ciudad</option>
                                </select>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control  full-size">
                                <select id="zonaC" title="Zona cierre">
                                    <option value="0" disabled="disabled" selected="selected">Zona</option>
                                </select>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control  full-size">
                                <select id="calleC" title="Calle cierre">
                                    <option value="0" disabled="disabled" selected="selected">Calle</option>
                                </select>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="latitudC" type="text" placeholder="Latitud" title="Latitud cierre" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="longitudC" type="text" placeholder="Longitud" title="Longitud cierre" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                    </div>
                    <%--            </div>
                    </div>--%>
                </div>
            </div>
            <%--Fin panel Cierre--%>
            <%--panel valla--%>
            <div class="panel collapsible" data-role="panel">
                <div class="heading bg-grayDark">
                    <span class="bg-white"></span>
                    <span class="title">Valla</span>
                </div>
                <div class="content">
                    <%--<div class="panel collapsible" data-role="panel">
                        <div class="heading fondoSecundario">
                            <span class="icon mif-tag fondoPrincipal"></span>
                            <span class="title text-light">Identificación</span>
                        </div>
                        <div class="content ">--%>
                    <div class="row cells4 no-padding no-margin">
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="id" type="text" placeholder="ID" title="ID valla" readonly="readonly" />

                            </div>
                        </div>
                        <div class="cell colspan2">
                            <div class="input-control text full-size" data-role="input">
                                <input id="descripcion" type="text" placeholder="Descripcion valla" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control  full-size">
                                <select id="tipo" title="Tipo">
                                    <option value="0" disabled="disabled" selected="selected">Tipo</option>
                                    <option value="Valla">Valla</option>
                                    <option value="Super Valla">Súper Valla</option>
                                    <option value="Mega Valla">Mega Valla</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <%--                        </div>
                    </div>--%>
                    <%--    <div class="panel collapsible" data-role="panel">
                        <div class="heading fondoSecundario">
                            <span class="icon mif-location FondoPrincipal"></span>
                            <span class="title text-light">Ubicación</span>
                        </div>
                        <div class="content">--%>
                    <div class="row cells4 no-padding no-margin">


                        <div class="cell">
                            <div class="input-control  full-size">
                                <select id="calle" title="Calle valla">
                                    <option value="0" disabled="disabled" selected="selected">Calle</option>
                                </select>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="latitud" type="text" placeholder="Latitud" title="Latitud valla" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control text full-size" data-role="input">
                                <input id="longitud" type="text" placeholder="Longitud" title="Longitud valla" />
                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control full-size">
                                <select id="disponibilidad" title="Disponibilidad valla">
                                    <option value="0" disabled="disabled" selected="selected">Disponibilidad</option>
                                    <option value="1">Disponible</option>
                                    <option value="2">No disponible</option>
                                    <option value="3">Reservado</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <%--  </div>
                    </div>--%>
                    <%--               <div class="panel collapsible" data-role="panel">
                        <div class="heading fondoSecundario">
                            <span class="icon mif-cog fondoPrincipal"></span>
                            <span class="title text-light">Ficha Técnica</span>
                        </div>
                        <div class="content">--%>
                    <div class="row cells3 no-margin no-padding">


                        <div class="cell">
                            <div class="input-control file full-size" data-role="input">
                                <input id="rutaImagen" type="file" name="Foto" onchange="PreviewImage();" title="Foto" />
                                <input id="rutaReal" type="text" />
                                <button class="button fondoNaranja"><span class="mif-folder fg-white"></span></button>
                            </div>
                            <div>
                                <img src="" id="uploadPreview" />
                            </div>
                        </div>
                        <div class="cell">
                            <div class="input-control file full-size" data-role="input">
                                <input id="URLvideo" type="text" placeholder="URL video" title="URL video" />
                                <button class="button"><span class="mif-link "></span></button>
                            </div>
                        </div>
                        <div class="cel">
                            <div class=" place-right">
                                <button id="btnGuardarValla" class="image-button fondoNaranja  fg-white text-light">Guargar valla<span class="icon mif-floppy-disk fondoIcono bg-active-darkBlue fg-white text-light"></span></button>
                            </div>
                        </div>
                    </div>


                    <%--  </div>
                    </div>--%>
                </div>
            </div>
            <%--Fin panel valla--%>
        </div>

        <%-- Div Consulta--%>

        <%--fin div consulta--%>


        <div id="consulta" class="padding20 dialog" data-role="dialog" data-overlay="true" data-overlay-color="op-dark" data-close-button="true" data-windows-style="true">
            <div class="fondoPrincipal">
                <hr class="thin bg-grayLighter" />
                <h4 class="text-light fg-white">Listado de cierres</h4>
                <hr class="thin bg-grayLighter" />
            </div>
            <div id="dvConsultaCierre"></div>
        </div>
        <hr class="thin bg-grayLighter" />
        <h4 class="text-light">Detelle del cierre</h4>
        <hr class="thin bg-grayLighter" />
        <div id="dvConsultaVallas"></div>
    </div>


    <%--Video--%>


    <div data-role="dialog" id="dialog-video-content" data-background="bg-dark" data-close-button="true"
        data-content='<iframe src="https://www.youtube.com/v/eTwzoPVeuck/"></iframe>'
        data-width="600"
        data-content-type="video">
    </div>


</asp:Content>
