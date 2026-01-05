using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Controllers
{
    //לבדיקה ולא לשימוש!!!
    // [ApiController]
    // [Route("api/[controller]")]
    // public class TicketController : ControllerBase

    // {
    //     private readonly ITicketService _ticketService;
    //     public TicketController(ITicketService ticketService)
    //     {
    //         _ticketService = ticketService;
    //     }



    //     [HttpPost("AddTicket")]
    //     public async Task<ActionResult<TicketDTO.TicketCreateDTO>> AddTicket([FromBody] TicketDTO.TicketCreateDTO ticketDTO)
    //     {
    //         try
    //         {
    //             await _ticketService.AddTicket(ticketDTO);
    //             return Ok(new { message = "Ticket added successfully!" });
    //         }
    //         catch (Exception ex)
    //         {
    //             return BadRequest(new { message = ex.Message });
    //         }
    //     }


    //     [HttpPost("AddTicketsRange")]
    //     public async Task<ActionResult<TicketCreateDTO>> AddTicketRange([FromBody] List<TicketCreateDTO> ticketsDTO)
    //     {
    //         try
    //         {
    //             await _ticketService.AddTicketsRange(ticketsDTO);
    //             return Ok(new { message = "Ticket added successfully!" });
    //         }
    //         catch (Exception ex)
    //         {
    //             return BadRequest(new { message = ex.Message });
    //         }
    //     }



    //     [HttpGet("GetTicketsByPrizeId/{prizeId}")]
    //     public async Task<IActionResult> GetTicketsByPrizeId(int prizeId)
    //     {
    //         try
    //         {
    //             var tickets = await _ticketService.GetTicketsByPrizeId(prizeId);
    //             return Ok(tickets);
    //         }
    //         catch (Exception ex)
    //         {
    //             return BadRequest(new { message = ex.Message });
    //         }
    //     }



    //     [HttpGet("GetTicketsByUserIdAndPrizeId/{userId}/{prizeId}")]
    //     public async Task<IActionResult> GetTicketsByUserIdAndPrizeId(int userId, int prizeId)
    //     {
    //         try
    //         {
    //             var tickets = await _ticketService.GetTicketsByUserIdAndprizeId(userId, prizeId);
    //             return Ok(tickets);
    //         }
    //         catch (Exception ex)
    //         {
    //             return BadRequest(new { message = ex.Message });
    //         }
    //     }
    // }
}
