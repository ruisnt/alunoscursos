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
    public class InscricoesController : ControllerBase, IMap
    {
        private readonly InscricaoAlunoApiContext _context;

        public InscricoesController(InscricaoAlunoApiContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public IEnumerable<Inscricao> Get()
        {
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

            var lista = busca
                .ToArray()
                .Select(this.Map);

            return lista;
        }

        // GET: api/Inscricoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInscricao([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscricao = await _context.Inscricao.FindAsync(id);

            if (inscricao == null)
            {
                return NotFound();
            }

            return Ok(this.Map(inscricao));
        }

        // PUT: api/Inscricoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscricao([FromRoute] int id, [FromBody] Inscricao inscricao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inscricao.id)
            {
                return BadRequest();
            }

            _context.Entry(this.Map(inscricao)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoExists(id))
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

        // POST: api/Inscricoes
        [HttpPost]
        public async Task<IActionResult> PostInscricao([FromBody] Inscricao inscricao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Inscricao.Add(this.Map(inscricao));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscricao", new { id = inscricao.id }, inscricao);
        }

        private bool InscricaoExists(int id)
        {
            return _context.Inscricao.Any(e => e.id == id);
        }
    }
}