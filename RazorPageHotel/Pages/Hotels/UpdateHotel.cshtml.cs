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
    public class UpdateHotelModel : PageModel
    {
        [BindProperty]
        public Hotel newHotel { get; set; }
        [BindProperty]
        public int hotelNumber { get; set; }

        private IHotelService hotelService;

        public UpdateHotelModel(IHotelService hService)
        {
            this.hotelService = hService;
        }

        public void OnGet(int id)
        {
            hotelNumber = id;
        }

        public async Task<IActionResult> OnPost(int updateId)
        {
            await hotelService.UpdateHotelAsync(newHotel, updateId);
            return RedirectToPage("GetAllHotels");
        }
    }
}
