using Microsoft.Reporting.WebForms;
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

    public partial class grilla : System.Web.UI.Page
    {

        static int disp;
        static string filtroCierre = string.Empty;
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
        private static DataTable datosVallas(int id)//busca los datos de cada valla para un cierre
        {
            string filtro = string.Empty;

            if (disp == 1)
            {
                filtro = "AND dbo.vallas.Disponibilidad = 1 ";
            }
            else if (disp == 2)
            {
                filtro = "AND dbo.vallas.Disponibilidad = 2 ";
            }
            else if (disp == 3)
            {
                filtro = "AND dbo.vallas.Disponibilidad = 3 ";
            }
            else
            {
                filtro = string.Empty;
            }


            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT   dbo.vallas.ID, dbo.vallas.tipo Tipo, dbo.vallas.descripcion Descripcion, dbo.vallas.calle Calle, dbo.vallas.Disponibilidad AS Estado, dbo.clientes.nombre AS Cliente, dbo.colocaciones.fechaFin AS [Dispobible en] FROM    dbo.clientes right JOIN  dbo.colocaciones ON dbo.clientes.ID = dbo.colocaciones.idCliente right JOIN  dbo.vallas ON dbo.colocaciones.idValla = dbo.vallas.ID WHERE dbo.vallas.idCierre = " + id + " " + filtro + filtroCierre + "  "))
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
            html.Append("<tr><td class='bg-grayLight' colspan='10'><span class='fg-white title text-light'>" + nombreCierre(id) + "</span></td></tr>");
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
                        if (row[column.ColumnName].ToString() == "1")
                        {
                            html.Append("<span class='mif-checkmark fg-green'></span>");
                        }
                        else if ((row[column.ColumnName].ToString() == "3"))
                        {
                            html.Append("<span class='mif-event-available fg-amber'></span>");
                        }
                        else
                        {
                            html.Append("<span class='mif-event-busy fg-red'></span>");
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

        [WebMethod]
        public static string grillaVallas(int disponibilidad, int segundoFiltro, string fechaInicio, string fechaFin, string calle)
        {
            disp = disponibilidad;
            string filtro = string.Empty;
            string otroFiltro = string.Empty;
            string filtroCierre2 = string.Empty;

            if (disp == 1)
            {
                filtro = " WHERE dbo.vallas.Disponibilidad = 1 ";
            }
            else if (disp == 2)
            {
                filtro = " WHERE dbo.vallas.Disponibilidad = 2 ";
            }
            else if (disp == 3)
            {
                filtro = " WHERE dbo.vallas.Disponibilidad = 3 ";
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
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.vallas.idCierre FROM dbo.vallas left JOIN dbo.colocaciones ON dbo.vallas.ID = dbo.colocaciones.idValla  " + filtro + filtroCierre2 +  "", con))
                {
                    
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            tabla.Append(cargaGrilla(Convert.ToInt16(dr["idCierre"].ToString())));
                        }
                        tabla.Append("</table>");
                    }
                }
            }

            return tabla.ToString();
        }

        [WebMethod]
        public static string agregarElemento(int idElemento, int idCircuito)//agrega vallas o mueble al circuito seleccionado
        {
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "INSERT INTO [dbo].[detalleCircuitos]([idElemento],[idCircuito],[idUsuario],[fechaRegistro])";
            SQL += " VALUES( " + idElemento + "," + idCircuito + "," + HttpContext.Current.Session["idusuario"] + ",GETDATE())";
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
        public static string guardaCircuito(int idCliente, string nombre)//Crea un circuito nuevo
        {
            object id;
            int idCircuito = 0;
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "INSERT INTO [dbo].[circuitos]([idCliente],[nombre],[idUsuario],[fechaRegistro])";
            SQL += " VALUES( " + idCliente + ",'" + nombre + "'," + HttpContext.Current.Session["idusuario"] + ",GETDATE());select @@IDENTITY as ID";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    id = oCmd.ExecuteScalar();
                    idCircuito = Convert.ToInt16(id);
                }
                mensaje = "$.Notify({ caption: 'Circuito', content: 'Creado', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'});selecionaCircuito(" + idCircuito + ");hideDialog('#dvNuevoCircuito');$('#idCircuito').val(" + idCircuito + ")";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Circuito', content: 'No Creado', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
            }

            return mensaje;

        }
        private static DataTable GetDataCircuito()//obtiene datos tabla circuito
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM View_Lista_Circuitos"))
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
        public static string consultaCircuitos()
        {
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = GetDataCircuito();
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
                html.Append("<td style='width:20px' onclick='selecionaCircuito(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-pin'></span></td>");
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
        public static string selecionaCircuito(int idCircuito)//carga nombre circuito
        {
            object nombre;
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            string SQL = "SELECT [nombre] FROM [dbo].[circuitos] WHERE ID = " + idCircuito;
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

        public string contadorVallas(int disponibilidad)//cuenta el numero de vallas: 1 disponibles, 2 reservadas, 3 ocupadas
        {
            object nombre;
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            string SQL = "SELECT COUNT(ID) FROM [dbo].[vallas] WHERE disponibilidad =  " + disponibilidad;
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
        //tabla circuitos->elementos
        private static DataTable GetDataElemento(int idCircuito)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.vallas.ID as [ID Elemento], dbo.vallas.tipo as Elemento, dbo.vallas.Descripcion, dbo.vallas.calle as Calle FROM dbo.vallas INNER JOIN dbo.detalleCircuitos ON dbo.vallas.ID = dbo.detalleCircuitos.idElemento INNER JOIN dbo.circuitos ON dbo.detalleCircuitos.idCircuito = dbo.circuitos.ID where dbo.circuitos.ID =" + idCircuito + ""))
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
        public static string consultaDetellaCircuito(int idCircuito)
        {
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = GetDataElemento(idCircuito);
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
                html.Append("<td style='width:20px' onclick='eliminaVallaCircuito(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-bin'></span></td>");
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

        public void reporteCircuito(int idCircuito, string extension)
        {

            ReportViewer1.LocalReport.DataSources.Clear();
            var adaptador = new DSreporteTableAdapters.DataTable1TableAdapter();
            var tabla = new DSreporte.DataTable1DataTable();
            adaptador.FillByIdCircuito(tabla, idCircuito);

            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DSreporte", (DataTable)tabla));
            ReportViewer1.DataBind();
            savePDF(extension);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void savePDF(string ext)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string HIJRA_TODAY = "01/10/1435";
            ReportParameter[] param = new ReportParameter[1];

            byte[] bytes = ReportViewer1.LocalReport.Render(
                ext,
                null,
                out mimeType,
                out encoding,
                out extension,
                out streamIds,
                out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader(
                "content-disposition",
                "attachment; filename= filename" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
        }
        [WebMethod]
        public static string eliminaVallaCircuito(int idValla)//Elimina la valla seleccionada
        {

            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "DELETE FROM [dbo].[detalleCircuitos] WHERE idElemento =" + idValla;

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
                mensaje = "$.Notify({ caption: 'Elemento', content: 'Removido del circuito', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'})";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Elemento', content: 'No removido', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
            }

            return mensaje;

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            reporteCircuito(Convert.ToInt16(helper.validaInt(Request.Form["idCircuito"])),"PDF");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            reporteCircuito(Convert.ToInt16(helper.validaInt(Request.Form["idCircuito"])), "EXCEL");
        }
    }
}