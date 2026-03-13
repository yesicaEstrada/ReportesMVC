using ReportesMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace ReportesMVC.Data
{
    public class UsuarioDbContext
    {
        public (bool registrado, string mensaje) RegistrarUsuario(UsuarioDTO usuario)
        {
            var conexion = new DbConexion();
            bool registrado = false;
            string mensaje;

            try
            {
                using(var cn = new SqlConnection(conexion.getCadenaSql()))
                {

                    SqlCommand comd = new SqlCommand("[BDReportes].[dbo].[Sp_RegistrarUsuario]", cn);
                    comd.Parameters.AddWithValue("correo", usuario.Correo);
                    comd.Parameters.AddWithValue("clave", usuario.Clave);
                    comd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    comd.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    comd.CommandType = CommandType.StoredProcedure; //indicando que es un sp


                    cn.Open();//abrimos la conexcion a bd
                    comd.ExecuteNonQuery(); //ejecutamos el comando

                    registrado = Convert.ToBoolean(comd.Parameters["registrado"].Value);
                    mensaje = comd.Parameters["mensaje"].Value.ToString();
                   
                }
                return (registrado, mensaje);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int ValidarUsuario(string correo, string clave)
        {
            int idusuario = 0;
            var conexion = new DbConexion();
            try
            {
                using (var cn = new SqlConnection(conexion.getCadenaSql()))
                {
                    SqlCommand comd = new SqlCommand("sp_ValidarUsuario", cn);
                    comd.Parameters.AddWithValue("correo", correo);
                    comd.Parameters.AddWithValue("clave", clave);
                    comd.CommandType = CommandType.StoredProcedure; //indicando que es un sp


                    cn.Open();//abrimos la conexcion a bd
                    idusuario = Convert.ToInt32(comd.ExecuteScalar().ToString()); //ejecutamos el comando

                }
                return idusuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
