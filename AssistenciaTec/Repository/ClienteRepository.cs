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
    internal class ClienteRepository
    {
        public int Salvar(Cliente cliente)
        {

            //criar o caminha SQL de insert
            string sql = "INSERT INTO tbl_clientes (nome, email, telefone, endereco) " +
                "OUTPUT INSERTED.cliente_id " +
                "VALUES (@Nome, @Email, @Telefone, @Endereco)";

            //abrir a conexão com banco de dados
            using var conexao = Conexao.GetConexao();

            //comando para o banco de dados

            using var command = new SqlCommand(sql, conexao);

            //campos SQL com valores

            command.Parameters.AddWithValue("@Nome", cliente.Name);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            command.Parameters.AddWithValue("@Endereco", cliente.Endereco);

            //Executar commando
            var novoId = (int)command.ExecuteScalar(); //casting


            return novoId;


            //versoes antigas para fechar caso não exista using
            //conexao.Close();

        }

        public List<Cliente> GetAll()
        {
            //criar instruçao SQL listar todos
            var sql = "SELECT * from tbl_clientes order by nome ";

            using var conexao = Conexao.GetConexao();

            //criar comando
            using var command = new SqlCommand(sql, conexao);

            //obj que guarda o resultado do comando select
            using var resultado = command.ExecuteReader();

            //criar a lista vazia
            List<Cliente> clientes = new List<Cliente>();

            while (resultado.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Id = resultado.GetInt32(resultado.GetOrdinal("cliente_id"));
                cliente.Name = resultado.GetString(resultado.GetOrdinal("nome"));
                cliente.Telefone = resultado.GetString(resultado.GetOrdinal("telefone"));
                cliente.Email = resultado.GetString(resultado.GetOrdinal("email"));
                cliente.Endereco = resultado.GetString(resultado.GetOrdinal("endereco"));
                clientes.Add(cliente);

            }

            return clientes;
        }

        public int excluir(int id)
        {
            var sql = "DELETE FROM tbl_clientes WHERE cliente_id = @id";

            using var conexao = Conexao.GetConexao();

            using var command = new SqlCommand(sql, conexao);

            command.Parameters.AddWithValue("id", id);

            //executar o comando
            var resultado = command.ExecuteNonQuery();

            return resultado;

        }

        public int atualizar(Cliente cliente)
        {
            //valores SQL
            var sql = "UPDATE tbl_clientes " +
                "SET nome = @Nome, " +
                "email = @Email, " +
                "telefone = @Telefone, " +
                "endereco = @Endereco " +
                "WHERE  cliente_id = @id";
            //conexao
            using var conexao = Conexao.GetConexao();

            using var command = new SqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@Nome", cliente.Name);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            command.Parameters.AddWithValue("id", cliente.Id);

            var resultado = command.ExecuteNonQuery();

            return resultado;
        }

        public List<Cliente> pesquisarNome(string nome)
        {
            var sql = "SELECT * FROM tbl_clientes " +
                "WHERE nome " +
                "LIKE @nome ORDER BY nome ASC";

            using var conexao = Conexao.GetConexao();

            using var command = new SqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@nome", "%" + nome + "%");


            using var resultado = command.ExecuteReader();

           
            List<Cliente> clientes = new List<Cliente>();

            while (resultado.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Id = resultado.GetInt32(resultado.GetOrdinal("cliente_id"));
                cliente.Name = resultado.GetString(resultado.GetOrdinal("nome"));
                cliente.Telefone = resultado.GetString(resultado.GetOrdinal("telefone"));
                cliente.Email = resultado.GetString(resultado.GetOrdinal("email"));
                cliente.Endereco = resultado.GetString(resultado.GetOrdinal("endereco"));
                clientes.Add(cliente);

            }

            return clientes;

            return null;
        }

    }
}
