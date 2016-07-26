<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="gestionSarmiento.inicio" %>

<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <link rel='shortcut icon' type='image/x-icon' href='../favicon.ico' />
    <title>|SmartGrid|</title>

    <!--<link href="../css/metro-responsive.css" rel="stylesheet">-->
    <link href="content/css/metro.css" rel="stylesheet" />
    <link href="content/css/metro-responsive.css" rel="stylesheet" />
    <link href="content/css/metro-icons.css" rel="stylesheet" />
    <script src="content/js/jquery-2.1.4.min.js"></script>
    <script src="content/js/metro.js"></script>


    <style>
        .tile-area-controls {
            position: fixed;
            right: 40px;
            top: 40px;
        }

        .tile-group {
            left: 100px;
        }

        .tile, .tile-small, .tile-sqaure, .tile-wide, .tile-large, .tile-big, .tile-super {
            opacity: 0;
            -webkit-transform: scale(.8);
            transform: scale(.8);
        }

        #charmSettings .button {
            margin: 5px;
        }

        .schemeButtons {
            /*width: 300px;*/
        }

        @media screen and (max-width: 640px) {
            .tile-area {
                overflow-y: scroll;
            }

            .tile-area-controls {
                display: none;
            }
        }

        @media screen and (max-width: 320px) {
            .tile-area {
                overflow-y: scroll;
            }

            .tile-area-controls {
                display: none;
            }
        }
    </style>

    <script>
        (function ($) {
            $.StartScreen = function () {
                var plugin = this;
                var width = (window.innerWidth > 0) ? window.innerWidth : screen.width;

                plugin.init = function () {
                    setTilesAreaSize();
                    if (width > 640) addMouseWheel();
                };

                var setTilesAreaSize = function () {
                    var groups = $(".tile-group");
                    var tileAreaWidth = 80;
                    $.each(groups, function (i, t) {
                        if (width <= 640) {
                            tileAreaWidth = width;
                        } else {
                            tileAreaWidth += $(t).outerWidth() + 80;
                        }
                    });
                    $(".tile-area").css({
                        width: tileAreaWidth
                    });
                };

                var addMouseWheel = function () {
                    $("body").mousewheel(function (event, delta, deltaX, deltaY) {
                        var page = $(document);
                        var scroll_value = delta * 50;
                        page.scrollLeft(page.scrollLeft() - scroll_value);
                        return false;
                    });
                };

                plugin.init();
            }
        })(jQuery);

        $(function () {
            $.StartScreen();

            var tiles = $(".tile, .tile-small, .tile-sqaure, .tile-wide, .tile-large, .tile-big, .tile-super");

            $.each(tiles, function () {
                var tile = $(this);
                setTimeout(function () {
                    tile.css({
                        opacity: 1,
                        "-webkit-transform": "scale(1)",
                        "transform": "scale(1)",
                        "-webkit-transition": ".3s",
                        "transition": ".3s"
                    });
                }, Math.floor(Math.random() * 500));
            });

            $(".tile-group").animate({
                left: 0
            });
        });

        function showCharms(id) {
            var charm = $(id).data("charm");
            if (charm.element.data("opened") === true) {
                charm.close();
            } else {
                charm.open();
            }
        }

        function setSearchPlace(el) {
            var a = $(el);
            var text = a.text();
            var toggle = a.parents('label').children('.dropdown-toggle');

            toggle.text(text);
        }
    </script>
</head>
<body style="overflow-y: hidden;">
    <div data-role="charm" id="charmSearch">
        <h1 class="text-light">Buscar</h1>
        <hr class="thin" />
        <br />
        <div class="input-control text full-size">
            <label>
                <span class="dropdown-toggle drop-marker-light">Programas</span>
                <ul class="d-menu" data-role="dropdown">
                    <li><a onclick="setSearchPlace(this)">Programas</a></li>
                    <li><a onclick="setSearchPlace(this)">Reportes</a></li>
                    <li><a onclick="setSearchPlace(this)">Opciones</a></li>
                </ul>
            </label>
            <input type="text">
            <button class="button"><span class="mif-search"></span></button>
        </div>
    </div>
    <div class="tile-area tile-area-scheme-dark fg-white" style="height: 100%; max-height: 100% !important;">
        <h1 class="tile-area-title"><small>Bienvenido a </small>Sarmiento SmartGrid</h1>
        <div class="tile-area-controls">
            <button class="image-button icon-right bg-cyan fg-white bg-hover-dark no-border"><span class="sub-header no-margin text-light"><%= HttpContext.Current.Session["Nombre"].ToString()%></span> <span class="icon mif-user bg-cyan fg-white"></span></button>
            <button class="square-button bg-cyan fg-white bg-hover-dark no-border" onclick="showCharms('#charmSearch')"><span class="mif-search"></span></button>
            <button class="square-button bg-cyan fg-white bg-hover-dark no-border"><span class="mif-cog"></span></button>
            <a href="Default.aspx" class="square-button bg-cyan fg-white bg-hover-dark no-border"><span class="mif-switch"></span></a>
        </div>

        <div class="tile-group double">
            <span class="tile-group-title">Crear</span>
            <div class="tile-container">
                <a href="pages/espacios.aspx" class="tile bg-darkBlue fg-white" data-role="tile">
                    <div class="tile-content iconic">
                        <span class="icon mif-stack2"></span>
                    </div>
                    <span class="tile-label">Cierres</span>
                </a>
                <div class="tile bg-darkBlue fg-white" data-role="tile">
                    <div class="tile-content iconic">
                        <span class="icon mif-versions"></span>
                    </div>
                    <span class="tile-label">Mobiliario</span>
                </div>
            </div>
        </div>

        <div class="tile-group double">
            <span class="tile-group-title">Procesos</span>
            <div class="tile-container">
                <a href="pages/grilla.aspx" class="tile-wide bg-teal fg-white" data-role="tile">
                    <div class="tile-content iconic">
                        <span class="icon mif-stack2"></span>
                    </div>
                    <span class="tile-label">Grilla comercial</span>
                </a>
            </div>
            <div class="tile-container">
                <a href="pages/grillaOperaciones.aspx" class="tile-wide bg-teal fg-white" data-role="tile">
                    <div class="tile-content iconic">
                        <span class="icon mif-stack2"></span>
                    </div>
                    <span class="tile-label">Grilla operaciones</span>
                </a>
            </div>
            <div class="tile-container">
                <a href="pages/colocacion.aspx" class="tile-wide bg-teal fg-white" data-role="tile">
                    <div class="tile-content iconic">
                        <span class="icon mif-map2"></span>
                    </div>
                    <span class="tile-label">Asignar colocación</span>
                </a>
            </div>

        </div>
    </div>


</body>
</html>
