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
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; private set; }

        private IHotelService hotelService;

        public GetAllHotelsModel(IHotelService hService)
        {
            this.hotelService = hService;
        }
        /// <summary>
        /// Metoden henter alle hotellerne, og hvis der er et FilterCriteria p? siden s?ger den efter hoteller med et navn der indeholder s?geordet
        /// </summary>
        /// <returns>Returnerer void</returns>
        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Hotels = await hotelService.GetHotelsByNameAsync(FilterCriteria);
            }
            else
                Hotels = await hotelService.GetAllHotelAsync();
        }
    }

}
