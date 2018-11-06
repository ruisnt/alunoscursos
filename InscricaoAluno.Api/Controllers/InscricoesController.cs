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
    public class InscricoesController : ControllerBase
    {
        private readonly InscricaoAlunoApiContext _context;

        public InscricoesController(InscricaoAlunoApiContext context)
        {
            _context = context;
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

            return Ok(inscricao);
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

            _context.Entry(inscricao).State = EntityState.Modified;

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

            _context.Inscricao.Add(inscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscricao", new { id = inscricao.id }, inscricao);
        }

        private bool InscricaoExists(int id)
        {
            return _context.Inscricao.Any(e => e.id == id);
        }
    }
}