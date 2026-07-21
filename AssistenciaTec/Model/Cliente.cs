using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistenciaTec.Model
{
    public class Cliente
    {
        private string _Name;
        private string _Email;
        private string _Telefone;
        private string _Endereco;

        public int Id { get; set; }

        public string Name 
        { 
            get { return _Name; } 
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O campo nome tem ser Obrigatorio!", nameof(Name));
                }
                _Name = value;
            }
        }



        public string Email
        {
            get { return _Email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("campo Email é obrigatorio", nameof(Email));
                }
                _Email = value;
            }
        }




        public string Telefone { 
        get { return _Telefone; }
    set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("campo Telefone é obrigatorio", nameof(Telefone));
                }
                _Telefone = value;
            }
        }


        public string Endereco {
            get { return _Endereco; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("campo Endereco é obrigatorio", nameof(Endereco));
                }
                _Endereco = value;  
            }
        }



    }
}
