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
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        private IRoomService roomService;
        [BindProperty(SupportsGet = true)]
        public string currentHotelName { get; set; }
        [BindProperty(SupportsGet = true)]
        public int currentHotelNumber { get; set; }
        public List<Room> Rooms { get; private set; }

        public GetAllRoomsModel(IRoomService rService)
        {
            roomService = rService;
        }
        /// <summary>
        /// Metoden henter v?relserne i et bestemt hotel, og hvis der er et FilterCriteria valgt p? siden vil den ogs? filtrere ud fra v?relsestypen
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotel"></param>
        /// <returns>returnerer void</returns>
        public async Task OnGet(int id, string hotel)
        {

            currentHotelName = hotel;
            currentHotelNumber = id;
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Rooms = await roomService.GetAllRoomFromHotelByType(currentHotelNumber, FilterCriteria);
            }
            else
                Rooms = await roomService.GetAllRoomFromHotel(currentHotelNumber);

        }
    }
}
