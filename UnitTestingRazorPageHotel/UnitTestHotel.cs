using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPageHotel.Models;
using RazorPageHotel.Services;

namespace UnitTestingRazorPageHotel
{
    [TestClass]
    public class UnitTestHotel
    {

        // denne klasse tester HotelService

        private string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelRazorpages;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [TestMethod]
        public void TestAddHotel()
        {
            //Arrange
            HotelService hotelService = new HotelService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            //Act
            int numbersOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(1000, "TestHotel", "Testvej");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;

            int numbersOfHotelsAfter = hotels.Count;
            Hotel h2 = new Hotel();
            foreach (Hotel hotel in hotelService.GetHotelsByNameAsync("TestHotel").Result)
            {
                h2 = hotel;
            }
            Hotel h = hotelService.DeleteHotelAsync(h2.HotelNr).Result;

            //Assert
            Assert.AreEqual(numbersOfHotelsBefore + 1, numbersOfHotelsAfter);
            Assert.IsTrue(ok);
            //Kan ikke virke fordi jeg har identity på hotelnummer så den auto incrementer
            //Assert.AreEqual(h.HotelNr, newHotel.HotelNr);


        }

        [TestMethod]
        public void TestAddRoom()
        {
            //Arrange
            RoomService roomService = new RoomService(connectionString);
            List<Room> Rooms = roomService.GetAllRoom().Result;

            //Act
            int numberOfRoomsBefore = Rooms.Count;
            Room newRoom = new Room(9000000, "D", 5000, 1);
            bool ok = roomService.CreateRoom(newRoom.HotelNr, newRoom).Result;
            Rooms = roomService.GetAllRoom().Result;

            int numberOfRoomsAfter = Rooms.Count;
            Room r = roomService.DeleteRoom(newRoom.RoomNr, newRoom.HotelNr).Result;

            //Assert
            Assert.AreEqual(numberOfRoomsBefore + 1, numberOfRoomsAfter);
            Assert.IsTrue(ok);
            //Kan ikke virke fordi jeg har identity på hotelnummer så den auto incrementer
            //Assert.AreEqual(h.HotelNr, newHotel.HotelNr);

        }
    }

}
