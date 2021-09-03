using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using modelo;

namespace negocio
{
    public class PokemonNegocio
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
                //comando.CommandText = "select nombre,descripcion,Numero,UrlImagen from pokemons";
                comando.CommandText = "select nombre,p.descripcion,Numero,d.Descripcion as Debilidad, t.Descripcion as Tipo,UrlImagen from pokemons p,elementos t,elementos d where p.IdTipo = t.Id and p.IdDebilidad = d.Id";

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
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (String)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion= (String)lector["Descripcion"];

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
                //primera muestra comando.CommandText = "insert into POKEMONS values (" + pokemon.Numero + ",'" + pokemon.Nombre + "','" + pokemon.Descripcion + "','" + pokemon.UrlImagen + "',1,1,null,1)";
                comando.CommandText = "insert into POKEMONS values (" + pokemon.Numero + ",'" + pokemon.Nombre + "','" + pokemon.Descripcion + "','" + pokemon.UrlImagen + "',@idTipo,@idDebilidad,null,1)";
                comando.Parameters.AddWithValue("@idTipo", pokemon.Tipo.Id);
                comando.Parameters.AddWithValue("@idDebilidad", pokemon.Debilidad.Id);
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
