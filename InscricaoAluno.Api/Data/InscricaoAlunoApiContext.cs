using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InscricaoAluno.Api.Models;

namespace InscricaoAluno.Api.Models
{
    public class InscricaoAlunoApiContext : DbContext
    {
        public InscricaoAlunoApiContext (DbContextOptions<InscricaoAlunoApiContext> options)
            : base(options)
        {
        }

        public DbSet<InscricaoAluno.Api.Models.Aluno> Aluno { get; set; }

        public DbSet<InscricaoAluno.Api.Models.Curso> Curso { get; set; }

        public DbSet<InscricaoAluno.Api.Models.Inscricao> Inscricao { get; set; }
    }
}
