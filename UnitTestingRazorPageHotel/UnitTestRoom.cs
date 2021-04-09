using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPageHotel.Models;
using RazorPageHotel.Services;

namespace UnitTestingRazorPageHotel
{
    class UnitTestRoom
    {
        private string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelRazorpages;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [TestMethod]
        public void TestAddRoom()
        {
            //Arrange
            RoomService roomService = new RoomService(connectionString);
            List<Room> Rooms = roomService.GetAllRoom().Result;

            //Act
            int numberOfRoomsBefore = Rooms.Count;
            Room newRoom = new Room(100000, "D", 5000, 1);
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
