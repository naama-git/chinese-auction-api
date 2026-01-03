using AutoMapper;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;

namespace ChineseAuctionAPI.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly ITicketRepo _ticketRepo;
        private readonly IWinnerRepo _winnerRepo;

        private readonly IMapper _mapper;
        public RaffleService(ITicketRepo ticketRepo, IMapper mapper, IWinnerRepo winnerRepo)
        {
            _ticketRepo = ticketRepo;
            _winnerRepo = winnerRepo;
            _mapper = mapper;
        }
        public async Task<Winner> PerformRaffle(int prizeId)
        {
            var tickets = await _ticketRepo.GetTicketsByPrizeId(prizeId);

            if (tickets == null || !tickets.Any())
            {
                return null;
            }

            Random rnd = new Random();
            int winnerIndex = rnd.Next(tickets.Count());
            var winningTicket = tickets.ElementAt(winnerIndex);

            Winner winner = new()
            {
                UserId = winningTicket.UserId,
                PrizeId = prizeId,
            };

            await _winnerRepo.addWinnerToPrize(winner, prizeId);

            return winner;
        }
    }
}
