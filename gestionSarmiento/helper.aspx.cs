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
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace gestionSarmiento
{
    public partial class helper : System.Web.UI.Page
    {
        public static string Encripta(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static double validaInt(string valor)
        {
            double retorno;

            if (double.TryParse(valor.ToString(), out retorno))
            {

                retorno = Convert.ToDouble(valor.ToString());
            }
            else
            {
                retorno = 0;
            }
            return retorno;
        }
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        [WebMethod]
        public static string selectPropietarios(string valor, string nombre, string tabla, string id)
        {
            string Valores = string.Empty;
            string Query = string.Empty;
            string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

            Query = " SELECT [" + valor + "], [" + nombre + "] FROM [Elementos].[dbo].["+tabla+"]";
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
                            while (dr.Read())
                            {
                                Valores += " $('#" + id + "').append('<option value=" + dr["ID"].ToString() + ">" + dr["nombre"].ToString() + "</option>');";

                            }

                        }
                    }
                }
            }

            catch (Exception)
            {

            }

            return Valores;
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}