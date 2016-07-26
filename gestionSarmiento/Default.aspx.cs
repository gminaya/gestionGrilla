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


namespace gestionSarmiento
{
    public partial class Default : System.Web.UI.Page
    {
        [WebMethod]
        public static int iniciarSesion(string usuario, string password)
        {

            string pass = string.Empty;
            pass = helper.Encripta(password);

            return validaSesion(usuario, pass);

        }

        [WebMethod]
        public static int validaSesion(string usuario, string password)
        {
            string Sql = string.Empty;
            string Query = string.Empty;
            int login = 0;

            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            Sql = " SELECT ID, usuario, clave, nombre FROM usuarios WHERE usuario = '" + usuario + "' AND clave = '" + password + "'";

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(Sql, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    

                                        if (dr["clave"].ToString() == "54")
                                        {
                                            login = 2;
                                        }
                                        else
                                        {
                                            login = 1;
                                            HttpContext.Current.Session["idusuario"] = dr["ID"].ToString();
                                            HttpContext.Current.Session["Nombre"] = dr["nombre"].ToString();
                                            
                                        }

                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return login;
        }

        [WebMethod]
        public static string selecionaUsuario(string usuario)//carga nombre usuario
        {
            object nombre;
            string mensaje = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            string SQL = "SELECT [nombre] FROM [dbo].[usuarios] WHERE usuario = '" + usuario +"'";
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

        protected void Page_Load(object sender, EventArgs e)
        {

            Session.Abandon();
            Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
    }
}