using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gestionSarmiento.pages
{
    public partial class grillaOperaciones : System.Web.UI.Page
    {
        static int disp;
        static string filtroCierre = string.Empty;
        [WebMethod]
        public static string grillaVallas(int disponibilidad, int segundoFiltro, string fechaInicio, string fechaFin, string calle)
        {
            disp = disponibilidad;
            string filtro = string.Empty;
            string otroFiltro = string.Empty;
            string filtroCierre2 = string.Empty;



            if (disp == 1)
            {
                filtro = " WHERE dbo.colocaciones.estado= 1 ";
            }
            else if (disp == 2)
            {
                filtro = " WHERE dbo.colocaciones.estado = 2 ";
            }
            else if (disp == 3)
            {
                filtro = " WHERE dbo.colocaciones.estado = 3 ";
            }
            else
            {
                filtro = string.Empty;
            }

            if (segundoFiltro == 1)
            {
                filtroCierre = " and dbo.colocaciones.fechaFin ='" + fechaInicio + "' ";
                filtroCierre2 = " WHERE dbo.colocaciones.fechaFin ='" + fechaInicio + "' ";
            }

            else if (segundoFiltro == 2)
            {
                filtroCierre = " and dbo.colocaciones.fechaFin between '" + fechaInicio + "' and '" + fechaFin + "' ";
                filtroCierre2 = " WHERE dbo.colocaciones.fechaFin between '" + fechaInicio + "' and '" + fechaFin + "' ";
            }
            else if (segundoFiltro == 3)
            {
                filtroCierre = " and dbo.vallas.calle like '%" + calle + "%'";
                filtroCierre2 = " WHERE dbo.vallas.calle like '%" + calle + "%'";
            }
            else if (segundoFiltro == 4)
            {
                filtroCierre = " and dbo.colocaciones.fechaFin ='" + fechaInicio + "' and dbo.vallas.calle like '%" + calle + "%' ";
                filtroCierre2 = " WHERE dbo.colocaciones.fechaFin ='" + fechaInicio + "' and dbo.vallas.calle like '%" + calle + "%' ";
            }

            else if (segundoFiltro == 5)
            {
                filtroCierre = " and   dbo.vallas.calle like '%" + calle + "%'and dbo.colocaciones.fechaFin between '" + fechaInicio + "' and '" + fechaFin + "'";
                filtroCierre2 = " WHERE   dbo.vallas.calle like '%" + calle + "%'and dbo.colocaciones.fechaFin between '" + fechaInicio + "' and '" + fechaFin + "'";
            }
            else
            {
                filtroCierre = string.Empty;
                filtroCierre2 = string.Empty;
            }

            StringBuilder tabla = new StringBuilder();
            tabla.Append("<table style='font-size: 14px' class='table border bordered hovered cell-hovered striped' data-auto-width='true'> ");
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {                                                               //SELECT DISTINCT [idCierre] FROM [Elementos].[dbo].[vallas]
                using (SqlCommand cmd2 = new SqlCommand("SELECT        dbo.vallas.idCierre FROM dbo.vallas left JOIN dbo.colocaciones ON dbo.vallas.ID = dbo.colocaciones.idValla " + filtro + filtroCierre2 + "", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd2))
                    {
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);
                        foreach (DataRow dr in dt2.Rows)
                        {
                            tabla.Append(cargaGrilla(Convert.ToInt16(dr["idCierre"].ToString())));
                        }
                        tabla.Append("</table>");
                    }
                }
            }

            return tabla.ToString();
        }
        private static DataTable datosVallas(int id)//busca los datos de cada valla para un cierre
        {
            string filtro = string.Empty;

            if (disp == 1)
            {
                filtro = " AND dbo.colocaciones.estado= 1 ";
            }
            else if (disp == 2)
            {
                filtro = " AND dbo.colocaciones.estado = 2 ";
            }
            else if (disp == 3)
            {
                filtro = " AND dbo.colocaciones.estado = 3 ";
            }
            else
            {
                filtro = string.Empty;
            }


            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT   dbo.vallas.ID, dbo.vallas.descripcion Descripcion, dbo.vallas.calle Calle, dbo.clientes.nombre AS Cliente,dbo.colocaciones.Arte, dbo.colocaciones.estado AS Estado,  dbo.colocaciones.fechaInicio as Inicio, dbo.colocaciones.fechaFin AS Fin FROM    dbo.clientes right JOIN  dbo.colocaciones ON dbo.clientes.ID = dbo.colocaciones.idCliente right JOIN  dbo.vallas ON dbo.colocaciones.idValla = dbo.vallas.ID WHERE dbo.vallas.idCierre = " + id + " " + filtro + filtroCierre + "  "))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        private static string cargaGrilla(int id)//Contruye las tablas de cada cierre
        {
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = datosVallas(id);
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            //Table start.
            //html.Append("<table class='table border bordered hovered cell-hovered striped ' data-auto-width='true'> ");
            //Building the Header row.
            html.Append("<thead>");
            html.Append("<tr><td class='bg-grayLight' colspan='11'><span class='fg-white title text-light'>" + nombreCierre(id) + "</span></td></tr>");
            html.Append("<tr>");
            html.Append("<th style='width:20px' ></th>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th style='width:20px'>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("<th style='width:20px' ></th>");
            html.Append("<th style='width:20px' ></th>");
            html.Append("</tr>");
            html.Append("</thead>");

            //Building the Data rows.
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {

                html.Append("<tr class='no-padding no-margin'>");
                html.Append("<td style='width:20px' ><span  onclick='agregarElemento(" + (dt.Rows[contador]).ItemArray[0] + ")' style='color:#D9853B' class='mif-plus btnAgreagar'></span></td>");

                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    if (column.ColumnName == "Estado")
                    {
                        if (row[column.ColumnName].ToString() == "2")
                        {
                            html.Append("<span class='mif-warning fg-amber'></span>");
                        }
                        else if ((row[column.ColumnName].ToString() == "1"))
                        {
                            html.Append("<span class='mif-checkmark fg-green'></span>");
                        }
                        else
                        {
                            html.Append("<span class='mif-notification fg-red'></span>");
                        }
                    }
                    else
                    {
                        html.Append(row[column.ColumnName]);
                    }
                    html.Append("</td>");

                }
                html.Append("<td style='width:20px' ><span  onclick='alert(" + (dt.Rows[contador]).ItemArray[0] + ")' style='color:#D9853B' class='mif-image btnVerImagen'></span></td>");
                html.Append("<td style='width:20px' ><span  onclick='alert(" + (dt.Rows[contador]).ItemArray[0] + ")' style='color:#D9853B' class='mif-youtube-play btnVerVideo'></span></td>");
                contador++;
                html.Append("</tr>");
            }
            html.Append("<tbody>");
            //Table end.
            // html.Append("</table>");
            return Convert.ToString(html);
        }
        private static string nombreCierre(int id)//carga el nombre de cada cierre para tabla de vallas
        {
            string cierre = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [Descripcion]  FROM [Elementos].[dbo].[cierres] where ID = " + id + " ", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cierre = reader["Descripcion"].ToString();
                        }
                    }
                }
                con.Close();
            }

            return cierre;
        }

        [WebMethod]
        public static string agregarElemento(int idElemento, int idBrigada)//agrega vallas o mueble al brigada seleccionado
        {
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "INSERT INTO [dbo].[detalleBrigadas]([idElemento],[idBrigada],[idUsuario],[fechaRegistro])";
            SQL += " VALUES( " + idElemento + "," + idBrigada + "," + HttpContext.Current.Session["idusuario"] + ",GETDATE())";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
                mensaje = "$.Notify({ caption: 'Elemento', content: 'Agregado', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'});";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Elemento', content: 'No agregado', icon: '<span class='mif-cross'></span>', type:'alert'});";
            }

            return mensaje;

        }

        [WebMethod]
        public static string guardaGrigada(string nombre, string fechaOperacion)//Crea una brigada nueva
        {
            object id;
            int idBrigaba = 0;
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "INSERT INTO [dbo].[brigadas]([nombre],[fechaOperacion],[idUsuario],[fechaRegistro])";
            SQL += " VALUES( '" + nombre + "','" + fechaOperacion + "'," + HttpContext.Current.Session["idusuario"] + ",GETDATE());select @@IDENTITY as ID";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    id = oCmd.ExecuteScalar();
                    idBrigaba = Convert.ToInt16(id);
                }
                mensaje = "$.Notify({ caption: 'Brigada "+ nombre + "', content: 'Creada', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'});selecionaBrigada(" + idBrigaba + ");hideDialog('#dvNuevoCircuito');$('#idBrigada').val(" + idBrigaba + ")";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Brigada', content: 'No Creada', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
            }

            return mensaje;

        }

        [WebMethod]
        public static string selecionaBrigada(int idBrigada)//carga nombre Brigada
        {
            object nombre;
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            string SQL = "SELECT [nombre] FROM [dbo].[brigadas] WHERE ID = " + idBrigada;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    nombre = oCmd.ExecuteScalar();
                    mensaje = Convert.ToString(nombre);
                }
            }
            catch (Exception)
            {
                mensaje = "ERROR";
            }
            return mensaje;
        }
        private static DataTable GetDataElemento(int idBrigada)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.detalleBrigadas.ID, dbo.vallas.descripcion AS Elemento, dbo.cierres.descripcion AS Cierre FROM dbo.detalleBrigadas INNER JOIN dbo.vallas ON dbo.detalleBrigadas.ID = dbo.vallas.ID INNER JOIN dbo.cierres ON dbo.vallas.idCierre = dbo.cierres.ID where dbo.detalleBrigadas.idBrigada  =" + idBrigada + ""))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        [WebMethod]
        public static string consultaDetellaBrigada(int idBrigada)
        {
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = GetDataElemento(idBrigada);
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            //Table start.
            html.Append("<table class='table border bordered hovered cell-hovered' data-auto-width='false'> ");
            //Building the Header row.
            html.Append("<thead>");
            html.Append("<tr><td class='fondoPrincipal' colspan='5'><span class='fg-white title text-light'></span></td></tr>");
            html.Append("<tr>");
            html.Append("<th style='width:20px' ></th>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }

            html.Append("</tr>");
            html.Append("</thead>");

            //Building the Data rows.
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                html.Append("<td style='width:20px' onclick='eliminaElementoBrigada(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-bin'></span></td>");
                contador++;

                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }

                html.Append("</tr>");
            }
            html.Append("<tbody>");
            //Table end.
            html.Append("</table>");
            return Convert.ToString(html);

        }

        private static DataTable GetDataBrigadas()//obtiene datos tabla circuito
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select ID, Nombre, fechaOperacion AS Fecga from Brigadas"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        [WebMethod]
        public static string consultaBrigada()
        {
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = GetDataBrigadas();
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            //Table start.
            html.Append("<table class='table border bordered hovered cell-hovered' data-auto-width='false'> ");
            //Building the Header row.
            html.Append("<thead>");
            html.Append("<tr>");
            html.Append("<th style='width:20px' ></th>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</thead>");

            //Building the Data rows.
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                html.Append("<td style='width:20px' onclick='selecionaBrigada(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-pin'></span></td>");
                contador++;

                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("<tbody>");
            //Table end.
            html.Append("</table>");
            return Convert.ToString(html);

        }
        [WebMethod]
        public static string eliminaElementoBrigada(int idElemento)
        {

            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "DELETE FROM [dbo].[detalleBrigadas] WHERE ID =" + idElemento;

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
                mensaje = "$.Notify({ caption: 'Elemento', content: 'Removido de la brigada', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'})";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Elemento', content: 'No removido', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
            }

            return mensaje;

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}