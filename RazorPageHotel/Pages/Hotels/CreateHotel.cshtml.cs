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
    public class CreateHotelModel : PageModel
    {
        [BindProperty]
        public Hotel newHotel { get; set; }

        private IHotelService hotelService;

        public CreateHotelModel(IHotelService hService)
        {
            this.hotelService = hService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await hotelService.CreateHotelAsync(newHotel);
            return RedirectToPage("GetAllHotels");
        }
    }
}
