using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscricaoAluno.Api.DTO
{
    public class Curso
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Professor { get; set; }
    }
}
