using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace gestionSarmiento.pages
{
    public partial class espacios : System.Web.UI.Page
    {

        [WebMethod]
        public static string valoresValla(int id)
        {
            string Valores = string.Empty;
            string Query = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            Query = " SELECT [idCierre],[descripcion],[calle],[latitud],[longitud],[tipo],[disponibilidad],[rutaImagen],[rutaVideo] FROM [Elementos].[dbo].[vallas] where ID = " + id + "";
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
                                    
                                    Valores += "$('#descripcion').val('" + dr["descripcion"].ToString() + "');";
                                    
                                    
                                    Valores += "$('#calle').val('" + dr["calle"].ToString() + "');";
                                    Valores += "$('#latitud').val('" + dr["latitud"].ToString() + "');";
                                    Valores += "$('#longitud').val('" + dr["longitud"].ToString() + "');";
                                    Valores += "$('#tipo').val('" + dr["tipo"].ToString() + "').trigger('change');";
                                    Valores += "$('#disponibilidad').val('" + dr["disponibilidad"].ToString() + "').trigger('change');";
                                    Valores += "$('#rutaImagen').val('" + dr["rutaImagen"].ToString() + "');";
                                    Valores += "$('#rutaVideo').val('" + dr["rutaVideo"].ToString() + "');";
                                    Valores += "$('#id').prop('disabled', true)";

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
        public static string valoresCierre(int id)
        {
            string Valores = string.Empty;
            string Query = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            Query = " SELECT [idPropietario],[licencia],[descripcion],[ciudad],[zona],[calle],[latitud],[longitud] FROM [Elementos].[dbo].[cierres] where ID = " + id + "";

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
                                while (dr.Read())//trigger('change');
                                {
                                    Valores = "$('#idPropietario').val('" + dr["idPropietario"].ToString() + "');";
                                    Valores += "$('#licencia').val('" + dr["licencia"].ToString() + "');";
                                    Valores += "$('#descripcionC').val('" + dr["descripcion"].ToString() + "');";
                                    Valores += "$('#ciudadC').val('" + dr["ciudad"].ToString() + "');";
                                    Valores += "$('#zonaC').val('" + dr["zona"].ToString() + "');";
                                    Valores += "$('#calleC').val('" + dr["calle"].ToString() + "');";
                                    Valores += "$('#latitudC').val('" + dr["latitud"].ToString() + "');";
                                    Valores += "$('#longitudC').val('" + dr["longitud"].ToString() + "');";
                                    Valores += "$('#idCierreC').prop('disabled', true)";
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
        public static string guardaValla(int idCierre,  string descripcion, string calle, string latitud, string longitud, string tipo,
             string disponibilidad, string rutaImagen, string URLvideo)
        {
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            string SQL = "INSERT INTO [dbo].[vallas]([idCierre],[descripcion],[calle],[latitud],[longitud],[tipo],[disponibilidad],[rutaImagen],[rutaVideo],[idUsuario],[fechaRegistro])";
            SQL += "VALUES( " + idCierre + ",'" + descripcion + "','" + calle + "','" + latitud + "','" + longitud + "','" + tipo + "','" + disponibilidad + "','" + rutaImagen + "','" + URLvideo + "',"+ HttpContext.Current.Session["idusuario"] +",GETDATE())";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "$.Notify({ caption: 'Valla', content: 'Guardada', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'});";
        }
        [WebMethod]
        public static string actualizaValla(int id, string nombre, string descripcion,  string ciudad, string zona, string calle, string latitud, string longitud, string dimension,
             string disponibilidad, string rutaImagen, string URLvideo)
        {
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            string SQL = "update vallas set nombre ='" + nombre + "', descripcion='" + descripcion + "', ciudad='" + ciudad + "', zona='" + zona + "', calle='" + calle + "', latitud='" + latitud + "', longitud='" + longitud + "', rutaImagen='" + rutaImagen + "', rutaVideo='" + URLvideo + "' where ID= " + id + "";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "";
        }

        [WebMethod]
        public static string guardaCierre(int idPropietario, string licencia, string descripcion, string ciudad, string zona, string calle, string latitud, string longitud)
        {
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "INSERT INTO [dbo].[cierres]([idPropietario],[licencia],[descripcion],[ciudad],[zona],[calle],[latitud],[longitud],[idUsuario],[fechaRegistro])";
            SQL += " VALUES( " + idPropietario + ",'" + licencia + "','" + descripcion + "','" + ciudad + "','" + zona + "','" + calle + "','" + latitud + "','" + longitud + "',"+ HttpContext.Current.Session["idusuario"] + ",GETDATE())";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
                mensaje = "$.Notify({ caption: 'Cierre', content: 'Guardado', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'});";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Cierre', content: 'Guardado', icon: '<span class='mif-cross'></span>', type:'alert'});";
            }

            return mensaje;

        }


        private static DataTable GetDataCierre()//optiene datos tabla cierre
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [ID],[licencia] as Licencia,[Descripcion] as Descripción  FROM [Elementos].[dbo].[cierres]"))
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

        //optiene datos tabla valla
        private static DataTable GetData(int idCierre)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [ID],[Tipo],[descripcion],[Disponibilidad] FROM [Elementos].[dbo].[vallas] WHERE idCierre=" + idCierre + ""))
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
        public static string consultaVallas(int id)
        {
            
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = GetData(id);
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            //Table start.
            html.Append("<table class='table border bordered hovered cell-hovered' data-auto-width='false'> ");
            //Building the Header row.
            html.Append("<thead>");
            html.Append("<tr><td class='fondoPrincipal' colspan='6'><span class='fg-white title text-light'></span></td></tr>");
            html.Append("<tr>");
            html.Append("<th style='width:20px' ></th>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("<th style='width:20px' ></th>");
            html.Append("</tr>");
            html.Append("</thead>");

            //Building the Data rows.
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                html.Append("<td style='width:20px' onclick='eliminaValla(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-pin'></span></td>");
                contador++;

                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    if (column.ColumnName == "Disponibilidad")
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
                            html.Append("<span class='mif-stop fg-red'></span>");
                        }
                    }
                    else
                    {
                        html.Append(row[column.ColumnName]);
                    }
                    html.Append("</td>");
                }
                html.Append("<td style='width:20px'><span span style='color:#D9853B' class='mif-bin'></span></td>");
                html.Append("</tr>");
            }
            html.Append("<tbody>");
            //Table end.
            html.Append("</table>");
            return Convert.ToString(html);
            ////Append the HTML string to Placeholder.
            //PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            //PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
        }

        [WebMethod]
        public static string consultaCierres()
        {
            int contador = 0;
            //if (!this.IsPostBack)
            //{
            //Populating a DataTable from database.
            DataTable dt = GetDataCierre();
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
                html.Append("<td style='width:20px' onclick='buscaIdCierre(" + (dt.Rows[contador]).ItemArray[0] + ")'><span style='color:#D9853B' class='mif-pin'></span></td>");
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
            ////Append the HTML string to Placeholder.
            //PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            //PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
        }

        [WebMethod]
        public static string eliminaValla(int idValla)//Elimina la valla seleccionada
        {

            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            string SQL = "DELETE FROM [dbo].[vallas] WHERE ID =" + idValla;

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand oCmd = new SqlCommand(SQL, conn);
                    oCmd.CommandType = CommandType.Text;
                    oCmd.ExecuteNonQuery();
                }
                mensaje = "$.Notify({ caption: 'Elemento', content: 'Removido', icon:" + (char)34 + "<span class='mif-checkmark'></span>" + (char)34 + ", type:'success'})";
            }
            catch (Exception)
            {
                mensaje = "$.Notify({ caption: 'Elemento', content: 'No Removido', icon:" + (char)34 + "<span class='mif-cross'></span>" + (char)34 + ", type:'alert'});";
            }

            return mensaje;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}