using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotel.Interfaces;
using RazorPageHotel.Models;

namespace RazorPageHotel.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Room newRoom { get; set; }
        [BindProperty] public int hotelNumber { get; set; } 

        private IRoomService roomService;

        public CreateModel(IRoomService rService)
        {
            roomService = rService;
        }

        public void OnGet(int id)
        {
            hotelNumber = id;
        }

        public async Task<IActionResult> OnPost()
        {
            await roomService.CreateRoom(newRoom.HotelNr, newRoom);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}
