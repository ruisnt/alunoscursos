using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscricaoAluno.Api.Models
{
    public class Join
    {
        public Inscricao Inscricao { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
    }
}
