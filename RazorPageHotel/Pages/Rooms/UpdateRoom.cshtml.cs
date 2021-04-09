using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotel.Interfaces;
using RazorPageHotel.Models;

namespace RazorPageHotel.Pages.Rooms
{
    public class UpdateRoomModel : PageModel
    {
        [BindProperty]
        public Room newRoom { get; set; }
        [BindProperty]
        public int hotelNumber { get; set; }
        [BindProperty]
        public int roomNumber { get; set; }

        private IRoomService roomService;

        public UpdateRoomModel(IRoomService rService)
        {
            roomService = rService;
        }

        public void OnGet(int hid, int rid)
        {
            hotelNumber = hid;
            roomNumber = rid;
        }

        public async Task<IActionResult> OnPost(int updateRId, int updateHId)
        {
            await roomService.UpdateRoom(newRoom, updateRId, updateHId);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}
