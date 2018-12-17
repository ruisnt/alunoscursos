using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InscricaoAluno.Api.Models;

namespace InscricaoAluno.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly InscricaoAlunoApiContext _context;

        public AlunosController(InscricaoAlunoApiContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public IEnumerable<Aluno> GetAluno()
        {
            return _context.Aluno;
        }

        // GET: api/Alunos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAluno([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpGet("{id}/Inscricoes")]
        public async Task<IActionResult> GetInscricoes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var busca = await (from aluno in _context.Aluno
                               join inscricao in _context.Inscricao
                                   on aluno.id equals inscricao.idCurso
                               join curso in _context.Curso
                                   on inscricao.idCurso equals curso.id
                               where aluno.id == id
                               select new { curso, inscricao, aluno })
                    .ToArrayAsync();

            var inscricoes = busca
                .Select(item => {
                    Inscricao inscricao = item.inscricao;
                    inscricao.Aluno = item.aluno;
                    inscricao.Curso = item.curso;
                    return inscricao;
                });

            if (!inscricoes.Any())
            {
                return NotFound();
            }

            return Ok(inscricoes);
        }

        // PUT: api/Alunos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno([FromRoute] int id, [FromBody] Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aluno.id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Alunos
        [HttpPost]
        public async Task<IActionResult> PostAluno([FromBody] Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Aluno.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.id }, aluno);
        }

        // DELETE: api/Alunos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            if (_context.Inscricao.Any(item => !item.Termino.HasValue && item.idAluno == id))
                return BadRequest("Não é possível remover aluno com inscrição ativa");

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return Ok(aluno);
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.id == id);
        }
    }
}