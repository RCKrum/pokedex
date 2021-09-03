﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;

        private SqlDataReader lector;// lecror con l minuscula
        public SqlDataReader Lector //lector con L mayuscula
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("data source=.\\SQLEXPRESS;initial catalog = POKEDEX_DB;  integrated security = true;");
        }
        public void  setearConsulta(string consulta)
        {
            comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;

        }
        public void ejecutarConsulta()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }
        public void cerrarConexion()
        {
            conexion.Close();
            if (lector != null)
                lector.Close();
        }
    }
}
