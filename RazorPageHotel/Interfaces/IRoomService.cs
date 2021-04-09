using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPageHotel.Models;

namespace RazorPageHotel.Interfaces
{
    public interface IRoomService
    {
        /// <summary>
        /// henter alle værelser i databasen
        /// </summary>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoom();

        /// <summary>
        /// henter alle værelser til et hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomFromHotel(int hotelNr);


        /// <summary>
        /// henter alle værelser til et hotel fra databasen med en bestemt værelsestype
        /// </summary>
        /// <param name="hotelNr">Hotelnummeret </param>
        /// <param name="type">Værelsestypen der skal findes</param>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomFromHotelByType(int hotelNr, string type);



        /// <summary>
        /// Henter et specifik værelse fra database 
        /// </summary>
        /// <param name="roomNr">Udpeger det værelse der ønskes fra databasen</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Den fundne værelse eller null hvis værelset ikke findes</returns>
        Task<Room> GetRoomFromId(int roomNr, int hotelNr);

        /// <summary>
        /// Indsætter et ny værelse i databasen
        /// </summary>
        /// <param name="room">Værelset der skal indsættes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateRoom(int hotelNr, Room room);

        /// <summary>
        /// Opdaterer en værelset i databasen
        /// </summary>
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNr">Nummer på det værelse der skal opdateres</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateRoom(Room room, int roomNr, int hotelNr);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNr">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Task<Room> DeleteRoom(int roomNr, int hotelNr);

    }
}
