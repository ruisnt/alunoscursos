using InscricaoAluno.Api.Data;
using InscricaoAluno.Api.Mapeamento;
using InscricaoAluno.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscricaoAluno.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase, IMap
    {
        private readonly InscricaoAlunoApiContext _context;

        public CursosController(InscricaoAlunoApiContext context)
        {
            _context = context;
        }

        // GET: api/Cursos
        [HttpGet]
        public IEnumerable<Curso> GetCurso()
        {
            return _context
                .Curso
                .Where(item => !item.Excluido)
                .Select(this.Map);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurso([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var curso = await _context.Curso.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(this.Map(curso));
        }

        [HttpGet("{id}/Inscricoes")]
        public async Task<IActionResult> GetInscricoes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var busca = from curso in _context.Curso
                join inscricao in _context.Inscricao
                    on curso.id equals inscricao.idCurso
                join aluno in _context.Aluno
                    on inscricao.idAluno equals aluno.id
                where curso.id == id
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

        // PUT: api/Cursos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso([FromRoute] int id, [FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.id)
            {
                return BadRequest();
            }

            _context.Entry(this.Map(curso)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Cursos
        [HttpPost]
        public async Task<IActionResult> PostCurso([FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Curso.Add(this.Map(curso));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurso", new { id = curso.id }, curso);
        }

        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            if (_context.Inscricao.Any(item => item.Termino.HasValue && item.idCurso == id))
                return BadRequest("Não é possível remover um curso com inscrições ativas");

            curso.Excluido = true;
            await _context.SaveChangesAsync();

            return Ok(this.Map(curso));
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.id == id);
        }


    }
}