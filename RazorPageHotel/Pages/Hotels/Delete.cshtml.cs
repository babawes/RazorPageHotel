using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotel.Interfaces;
using RazorPageHotel.Models;

namespace RazorPageHotel.Pages.Hotels
{
    public class DeleteModel : PageModel
    {
        public int deletionId = -1;
        private IHotelService hotelService;

        public DeleteModel(IHotelService hService)
        {
            this.hotelService = hService;
        }

        public void OnGet(int id)
        {
            deletionId = id;
        }

        public async Task<IActionResult> OnPost(int deleteId)
        {
            await hotelService.DeleteHotelAsync(deleteId);
            return RedirectToPage("GetAllHotels");
        }
    }
}
