using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bobbins.Links.Data;
using Bobbins.Links.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bobbins.Links.Controllers
{
    [Route("links")]
    public class LinksController : Controller
    {
        private readonly LinkContext _context;
        private readonly IMapper _mapper;

        public LinksController(LinkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLinks([FromQuery] int page, CancellationToken ct)
        {
            var links = await _context.Links
                .OrderByDescending(l => l.CreatedAt)
                .Skip(page * 20)
                .ToListAsync(ct)
                .ConfigureAwait(false);
            return Ok(_mapper.Map<List<LinkDto>>(links));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLink(int id, CancellationToken ct)
        {
            var link = await _context.Links
                .FirstOrDefaultAsync(l => l.Id == id, ct)
                .ConfigureAwait(false);

            if (link is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LinkDto>(link));
        }

        [HttpPut("{id}/comment-added")]
        public async Task<IActionResult> CommentAdded(int id, CancellationToken ct)
        {
            var rows = await _context.IncrementCommentCountAsync(id, ct);
            if (rows == 0)
            {
                return NotFound();
            }
            return Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LinkDto linkDto, CancellationToken ct)
        {
            var link = _mapper.Map<Link>(linkDto);
            _context.Links.Add(link);
            await _context.SaveChangesAsync(ct);
            return CreatedAtAction("GetLink", new {id = link.Id}, _mapper.Map<LinkDto>(link));
        }
    }
}
