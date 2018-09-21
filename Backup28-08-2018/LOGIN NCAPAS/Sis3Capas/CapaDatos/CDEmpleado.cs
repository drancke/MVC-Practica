﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CDEmpleado
    {
        private CDConexion Conexion = new CDConexion();
        private SqlDataReader leer;

        public SqlDataReader iniciarSesion(string user, string pass) {

            SqlCommand comando = new SqlCommand("SPIniciarSesion",Conexion.AbriConexion());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Usuario",user);
            comando.Parameters.AddWithValue("@Password",pass);

            leer = comando.ExecuteReader();
            return leer;
        }
    }
}
