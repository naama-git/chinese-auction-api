using AutoMapper;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly ITicketService _ticketService;
        private readonly IWinnerService _winnerService;

        private readonly IMapper _mapper;
        public RaffleService(ITicketService ticketService, IMapper mapper, IWinnerService winnerService)
        {
            _ticketService = ticketService;
            _winnerService = winnerService;
            _mapper = mapper;
        }
        public async Task<CreateWinnerDTO> PerformRaffle(int prizeId)
        {
            var tickets = await _ticketService.GetTicketsByPrizeId(prizeId);

            if (tickets == null || !tickets.Any())
            {
                return null;
            }

            Random rnd = new Random();
            int winnerIndex = rnd.Next(tickets.Count());
            var winningTicket = tickets.ElementAt(winnerIndex);

            CreateWinnerDTO winner = new()
            {
                UserId = winningTicket.User.Id,
                PrizeId = prizeId,
            };

            await _winnerService.AddWinnerToPrize(winner);

            return winner;
        }
    }
}
