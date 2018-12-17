using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscricaoAluno.Api.DTO
{
    public class Aluno
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}
