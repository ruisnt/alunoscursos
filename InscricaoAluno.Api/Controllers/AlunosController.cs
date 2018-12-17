using InscricaoAluno.Api.Data;
using InscricaoAluno.Api.DTO;
using InscricaoAluno.Api.Mapeamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscricaoAluno.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase, IMap
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
            return _context
                .Aluno
                .Where(item => !item.Excluido)
                .Select(this.Map);
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

            return Ok(this.Map(aluno));
        }

        [HttpGet("{id}/Inscricoes")]
        public async Task<IActionResult> GetInscricoes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var busca = from inscricao in _context.Inscricao
                        join curso in _context.Curso
                            on inscricao.idCurso equals curso.id
                        join aluno in _context.Aluno
                            on inscricao.idAluno equals aluno.id
                        select new Models.Join
                        {
                            Curso = curso,
                            Inscricao = inscricao,
                            Aluno = aluno
                        };

            var inscricoes = busca
                .Select(this.Map);

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

            _context.Entry(this.Map(aluno)).State = EntityState.Modified;

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

            _context.Aluno.Add(this.Map(aluno));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { aluno.id }, aluno);
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

            aluno.Excluido = true;
            await _context.SaveChangesAsync();

            return Ok(this.Map(aluno));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.id == id);
        }
    }
}