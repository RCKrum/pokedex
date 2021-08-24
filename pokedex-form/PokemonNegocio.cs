using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace pokedex_form
{
    class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            try
            {
                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector;

                conexion.ConnectionString = "data source=.\\SQLEXPRESS;initial catalog = POKEDEX_DB;  integrated security = true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select nombre,descripcion,Numero,UrlImagen from pokemons";
                comando.Connection = conexion;
                conexion.Open();
                lector=comando.ExecuteReader();
                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Nombre = lector.GetString(0);
                    //modo getstring que dice traeme un string de tal columna (0)

                    aux.Descripcion = (String)lector["descripcion"];
                    //de esta forma le digo del lector traeme la columna descripcion pero
                    //hay que transformarlo con un casteo explicito segun tipo de dato en este caso string
                    
                    aux.Numero = lector.GetInt32(2);
                    aux.UrlImagen = (string)lector["UrlImagen"];

                    lista.Add(aux);
                }
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void agregar(Pokemon pokemon)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS;database = POKEDEX_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;
                comando.CommandText = "insert into POKEMONS values (" + pokemon.Numero + ",'" + pokemon.Nombre + "','" + pokemon.Descripcion + "','" + pokemon.UrlImagen + "',1,1,null,1)";
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
   
}
