using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bobbins.Links.Data;
using Bobbins.Links.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bobbins.Links.Controllers
{
    [Route("votes")]
    public class VotesController : Controller
    {
        private readonly LinkContext _context;
        private readonly IMapper _mapper;

        public VotesController(LinkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Vote([FromBody] VoteDto voteDto, CancellationToken ct)
        {
            var vote = await _context.Votes
                .FirstOrDefaultAsync(v => v.LinkId == voteDto.LinkId && v.User == voteDto.User, ct)
                .ConfigureAwait(false);

            if (vote != null)
            {
                await UpdateExistingVote(voteDto, vote, ct);
            }
            else
            {
                await AddNewVote(voteDto, ct);
            }
            return Accepted();
        }

        private async Task AddNewVote(VoteDto voteDto, CancellationToken ct)
        {
            var vote = _mapper.Map<Vote>(voteDto);
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync(ct);
            if (vote.Value > 0)
            {
                await _context.AddUpVoteAsync(voteDto.LinkId, ct).ConfigureAwait(false);
            }
            else
            {
                await _context.AddDownVoteAsync(voteDto.LinkId, ct).ConfigureAwait(false);
            }
        }

        private async Task<IActionResult> UpdateExistingVote(VoteDto voteDto, Vote vote, CancellationToken ct)
        {
            if (vote.Value == voteDto.Value)
            {
                return Accepted();
            }
            vote.Value = voteDto.Value;
            await _context.SaveChangesAsync(ct);
            if (vote.Value > 0)
            {
                await _context.ChangeUpVoteAsync(voteDto.LinkId, ct).ConfigureAwait(false);
            }
            else
            {
                await _context.ChangeDownVoteAsync(voteDto.LinkId, ct).ConfigureAwait(false);
            }
            return Accepted();
        }
    }
}