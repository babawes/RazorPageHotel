using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotel.Interfaces;

namespace RazorPageHotel.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        public int deletionHId = -1;
        public int deletionRId = -1;
        private IRoomService roomService;

        public DeleteModel(IRoomService rService)
        {
            roomService = rService;
        }

        public void OnGet(int hid, int rid)
        {
            deletionHId = hid;
            deletionRId = rid;
        }

        public async Task<IActionResult> OnPost(int deleteHId, int deleteRId)
        {
            await roomService.DeleteRoom(deleteRId, deleteHId);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}
