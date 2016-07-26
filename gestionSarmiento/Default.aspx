<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="gestionSarmiento.Default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>SmartGrid</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="stylesheet" href="assets/css/main.css" />
</head>
<body class="landing">
    <div id="page-wrapper">
    <!-- Header -->
        <header id="header">
            <nav id="nav">
                <ul>
                    <li><a href="#importancia">Importancia</a></li>
                    <li><a href="#solicitar">Solicitar</a></li>
                    <li><a href="#iniciar" class="button special">Entrar</a></li>
                </ul>
            </nav>
        </header>
        <!-- Banner -->
        <section id="banner">
            <div class="content">
                <header>
                    <h2>El futuro ha llegado!</h2>
                    <p>
                        Sarmiento SmartGrid es la nueva forma de
                        <br />
                        administración de elementos de publicidad exterior.
                    </p>
                </header>
                <span class="image">
                    <img src="images/pic01.jpg" alt="" /></span>
            </div>
            <a href="#one" class="goto-next scrolly">Next</a>
        </section>
        <!-- One -->
        <section id="importancia" class="spotlight style1 bottom">
            <span class="image fit main">
                <img src="images/pic02.jpg" alt="" /></span>
            <div class="content">
                <div class="container">
                    <div class="row">
                        <div class="4u 12u$(medium)">
                            <header>
                                <h2>Automatización de la información</h2>
                                <p>La información es uno de los principales recursos que poseen las empresas actualmente.</p>
                            </header>
                        </div>
                        <div class="4u 12u$(medium)">
                            <p>
                                La implementación de Sarmiento SmartGrid, 
                                        brinda la posibilidad de obtener grandes ventajas, 
                                        incrementar la capacidad de organización de la empresa, 
                                        y tornar de esta manera los procesos a una verdadera competitividad.
                            </p>
                        </div>
                        <div class="4u$ 12u$(medium)">
                            <p>
                                Siendo un sistema eficaz que ofrece múltiples posibilidades, permitiendo acceder 
                                        a los datos relevantes de manera frecuente y oportuna. Ofreciendo así una notable satisfacción 
                                        en los usuarios que lo operan, que puede resultar en que los empleados logren alcanzar 
                                        los objetivos planteados por la compañía.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <a href="#two" class="goto-next scrolly">Next</a>
        </section>

        <!-- Two -->
        <section id="iniciar" class="spotlight style2 right">
            <span class="image fit main">
                <img src="images/pic03.jpg" alt="" /></span>
            <div class="content">
                <header>
                    <h2>Iniciar sesión</h2>
                    <p>Inicie con su cuenta de correo corporativo</p>
                </header>
                <input name="email" id="usuario" type="email" placeholder="Correo" value="">
                <input name="name" id="usuarioNombre" type="text" >
                <input name="clave" id="clave" type="password" placeholder="Contraseña" value="">
                <ul class="actions">
                    <li><a id="btnIniciar" class="button">Entrar</a></li>
                </ul>
            </div>
            <a href="#three" class="goto-next scrolly">Next</a>
        </section>
        <!-- Five -->
        <section id="solicitar" class="wrapper style2 special fade">
            <div class="container">
                <header>
                    <h2>Solicita acceso</h2>
                    <p>Introduzca su cuenta de sarmiento para solicitar acceso a la plataforma</p>
                </header>
                <form method="post" action="#" class="container 50%">
                    <div class="row uniform 50%">
                        <div class="8u 12u$(xsmall)">
                            <input type="email" name="email" id="email" placeholder="Su correo" />
                        </div>
                        <div class="4u$ 12u$(xsmall)">
                            <input type="submit" value="Solicitar" class="fit special" />
                        </div>
                    </div>
                </form>
            </div>
        </section>

        <!-- Footer -->
        <footer id="footer">
            <ul class="icons">
                <li><a href="#" class="icon alt fa-twitter"><span class="label">Twitter</span></a></li>
                <li><a href="#" class="icon alt fa-facebook"><span class="label">Facebook</span></a></li>
                <li><a href="#" class="icon alt fa-linkedin"><span class="label">LinkedIn</span></a></li>
                <li><a href="#" class="icon alt fa-instagram"><span class="label">Instagram</span></a></li>
                <li><a href="#" class="icon alt fa-envelope"><span class="label">Email</span></a></li>
            </ul>
            <ul class="copyright">
                <li>&copy; Sarmiento SmartGrid. Todos los derechos reservados.</li>
                <li>Sarmiento Dominicana</li>
            </ul>
        </footer>

    </div>

    <!-- Scripts -->
    <script src="content/js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="content/js/Default.js"></script>
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/jquery.scrolly.min.js"></script>
    <script src="assets/js/jquery.dropotron.min.js"></script>
    <script src="assets/js/jquery.scrollex.min.js"></script>
    <script src="assets/js/skel.min.js"></script>
    <script src="assets/js/util.js"></script>
    <!--[if lte IE 8]><script src="assets/js/ie/respond.min.js"></script><![endif]-->
    <script src="assets/js/main.js"></script>
    <script src="content/js/metro.js"></script>
</body>
</html>
