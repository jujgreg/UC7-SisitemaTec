using AssistenciaTec.Data;
using AssistenciaTec.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistenciaTec.Repository
{
    internal class UserRepository
    {
        public User ConfirmarUsuario(string Email, string Password)
        {
            //Autenticar usuario no banco

            String sql = "select * from tbl_usuario WHERE email = @Email and senha = @Password";
            //conexao SQL
            using var conexao = Conexao.GetConexao();
            //criar o commando
            using var command = new SqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Password", Password);
            //executar o comando
            SqlDataReader reader = command.ExecuteReader();
            //cria um objeto do user com banco
            User userLogin = null;

            if (reader.Read())
            {
                userLogin = new User();

                userLogin.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                userLogin.Name = reader.GetString(reader.GetOrdinal("nome"));
                userLogin.Email = reader.GetString(reader.GetOrdinal("email"));
                userLogin.Password = reader.GetString(reader.GetOrdinal("senha"));
            }
            

                
            return userLogin;
        }
    }
}
