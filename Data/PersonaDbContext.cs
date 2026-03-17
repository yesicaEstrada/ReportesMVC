using ReportesMVC.Models;
using System.Data.SqlClient;
using System.Data;

namespace ReportesMVC.Data
{
    public class PersonaDbContext
    {
        public List<PersonaDTO> GetPersonas()
        {
            var lista = new List<PersonaDTO>();

            var cn = new DbConexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand comd = new SqlCommand("Sp_ConsultaPersona", conexion);
                comd.CommandType = CommandType.StoredProcedure;

                using (var dr = comd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PersonaDTO()
                        {
                            IdPersona= Convert.ToInt32(dr["IIDPERSONA"]),
                            Appaterno = dr["APPATERNO"].ToString(),
                            Apmaterno = dr["APMATERNO"].ToString(),
                            NombreSexo = dr["NOMBRE_SEXO"].ToString(),
                            Correo = dr["CORREO"].ToString(),
                            TelefonoCel1 = dr["TELEFONOOCELULAR1"].ToString(),
                            TipoDoc = dr["TIPODOC"].ToString(),
                            NumeroIdentificacion = dr["NUMEROIDENTIFICACION"].ToString()
                        });

                    }
                }
                return lista;
            }



        }


        public PersonaDTO ObtenerId(int IdPersona)
        {
            var obtPersona = new PersonaDTO();

            var cn = new DbConexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand comd = new SqlCommand("Sp_ConsultaXIdPersona", conexion);
                comd.Parameters.AddWithValue("IIDPERSONA", IdPersona);
                comd.CommandType = CommandType.StoredProcedure;

                using (var dr = comd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        obtPersona.IdPersona = Convert.ToInt32(dr["IIDPERSONA"]);
                        obtPersona.Appaterno = dr["APPATERNO"].ToString();
                        obtPersona.Apmaterno = dr["APMATERNO"].ToString();
                        obtPersona.IdSexo = Convert.ToInt32(dr["IIDSEXO"]);
                        obtPersona.Correo = dr["CORREO"].ToString();
                        obtPersona.TelefonoCel1 = dr["TELEFONOOCELULAR1"].ToString();
                        obtPersona.IidTipoDocumento = Convert.ToInt32(dr["IIDTIPODOCUMENTO"]);
                        obtPersona.NumeroIdentificacion = dr["NUMEROIDENTIFICACION"].ToString();

                    }
                }
                return obtPersona;

            }
        }

        public bool ActualizarPersona(PersonaDTO persona)
        {
            bool rpta;

            try
            {
                var cn = new DbConexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand comd = new SqlCommand("Sp_ActualizarPersona", conexion);
                    comd.Parameters.AddWithValue("IIDPERSONA", persona.IdPersona);
                    comd.Parameters.AddWithValue("APPATERNO", persona.Appaterno);
                    comd.Parameters.AddWithValue("APMATERNO", persona.Apmaterno);
                    comd.Parameters.AddWithValue("IIDSEXO", persona.IdSexo);
                    comd.Parameters.AddWithValue("CORREO", persona.Correo);
                    comd.Parameters.AddWithValue("TELEFONOOCELULAR1", persona.TelefonoCel1);
                    comd.Parameters.AddWithValue("IIDTIPODOCUMENTO", persona.IidTipoDocumento);
                    comd.Parameters.AddWithValue("NUMEROIDENTIFICACION", persona.NumeroIdentificacion);
                    comd.CommandType = CommandType.StoredProcedure;

                    comd.ExecuteNonQuery();

                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool inactivarXId(int id)
        {
            bool rpta = false;
            try
            {
                var con = new DbConexion();

                using (var cn = new SqlConnection(con.getCadenaSql()))
                {

                    cn.Open();
                    SqlCommand comd = new SqlCommand("Sp_INACTIVARXId", cn);
                    comd.Parameters.AddWithValue("@ID", id);
                    comd.CommandType = CommandType.StoredProcedure;

                    comd.ExecuteNonQuery();
                }
                return rpta = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                rpta = false;
            }
            return rpta;
        }
        
    }


}
