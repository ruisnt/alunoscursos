using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscricaoAluno.Api.DTO
{
    public class Inscricao
    {
        public int id { get; set; }
        public int idAluno { get; set; }
        public int idCurso { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public int? Avaliacao { get; set; }
        public Curso Curso { get; set; }
        public Aluno Aluno { get; set; }
    }
}
