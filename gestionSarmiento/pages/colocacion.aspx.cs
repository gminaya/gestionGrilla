using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;

namespace gestionSarmiento.pages
{
    public partial class colocacion : System.Web.UI.Page
    {
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
        private static DataTable GetDataElemento(int idCircuito)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.detalleCircuitos.idElemento AS ID, dbo.vallas.descripcion AS Elemento, dbo.cierres.nombre AS Cierre, CAST(dbo.colocaciones.fechaInicio  AS DATE) AS Inicio, CAST(dbo.colocaciones.fechaFin  AS DATE)AS Fin, dbo.colocaciones.Arte FROM dbo.vallas INNER JOIN dbo.cierres ON dbo.vallas.idCierre = dbo.cierres.ID INNER JOIN dbo.detalleCircuitos ON dbo.vallas.ID = dbo.detalleCircuitos.idElemento LEFT OUTER JOIN dbo.colocaciones ON dbo.detalleCircuitos.idElemento = dbo.colocaciones.idValla WHERE dbo.detalleCircuitos.idCircuito = " + idCircuito + ""))
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
            html.Append("<tr><td class='fondoPrincipal' colspan='7'><span class='fg-white title text-light'></span></td></tr>");
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
                html.Append("<td style='width:20px' onclick='valoresElemento(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-pin'></span></td>");
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

        [WebMethod]
        public static string valoresCliente(int id)//carga los valores del cliente segun el circuito
        {
            string Valores = string.Empty;
            string Query = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            Query = " SELECT C.ID, C.nombre FROM clientes C inner join circuitos ci on c.ID = ci.idCliente WHere ci.ID = " + id + "";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(Query, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Valores = "$('#idCliente').val('" + dr["ID"].ToString() + "');";
                                    Valores += "$('.nombreCliente').html('" + dr["nombre"].ToString() + "')";
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception)
            {
                Valores = "-1";
            }

            return Valores;
        }

        [WebMethod]
        public static string valoresElemento(int id)//carga los valores del elemento para asignar colocacion
        {
            string Valores = string.Empty;
            string Query = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            Query = " SELECT ID, descripcion FROM vallas WHERE ID = " + id + "";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(Query, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Valores = "$('#idElemento').val('" + dr["ID"].ToString() + "');";
                                    Valores += "$('#Elento').val('" + dr["descripcion"].ToString() + "')";
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception)
            {
                Valores = "-1";
            }

            return Valores;
        }

        [WebMethod]
        public static string asignarColocacion(int idElemento,int idCliente, string arte, string fechaInicio, string fechaFin)
        {
            //el estado 2 = colocacion pendiente
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            if (helper.validaInt(idElemento.ToString()) > 0 && helper.validaInt(idCliente.ToString()) > 0 && arte != "" && fechaFin != "")
            {


                try
                {
                    using (SqlConnection conn = new SqlConnection(conexion))
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        SqlTransaction transaction;

                        // Start a local transaction.
                        transaction = conn.BeginTransaction("SampleTransaction");

                        // Must assign both transaction object and connection
                        // to Command object for a pending local transaction
                        command.Connection = conn;
                        command.Transaction = transaction;

                        try
                        {
                            command.CommandText = "INSERT INTO colocaciones (idValla, idCliente,	fechaInicio, fechaFin,arte, estado,	idUsuario,	fechaRegistro) VALUES (" + idElemento + "," + idCliente + ",'" + fechaInicio + "','" + fechaFin + "','" + arte + "',2,NULL,GETDATE())";
                            command.ExecuteNonQuery();
                            command.CommandText = "UPDATE vallas set disponibilidad = 2 WHERE ID= " + idElemento;
                            command.ExecuteNonQuery();

                            // Attempt to commit the transaction.
                            transaction.Commit();
                            mensaje = "$.Notify({ caption: 'Colocación', content: 'Registrada', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'});";
                        }
                        catch (Exception)
                        {

                            transaction.Rollback();
                            mensaje = "$.Notify({ caption: 'Error', content: 'Operación no completada', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
                        }
                    }
                }
                catch (Exception ex2)
                {

                    mensaje = "$.Notify({ caption: 'Error', content: 'Operación no completada', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
                }
            }
            else
            {
                mensaje = "$.Notify({ caption: 'Colocación no asignada', content: 'Datos no completados correctamente', icon: '<span class='mif-info'></span>', type: 'warning' });";
            }

            return mensaje;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}