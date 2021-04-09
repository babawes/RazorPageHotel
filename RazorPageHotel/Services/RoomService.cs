using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotel.Interfaces;
using RazorPageHotel.Models;

namespace RazorPageHotel.Services
{
    public class RoomService : Connection, IRoomService
    {
        // lad klassen arve fra interfacet IRoomService og arve fra Connection klassen
        // indsæt sql strings
        private string queryString2 = "select * from Room";
        private string queryString = "select * from Room where Hotel_No=@hotelno";
        private string RoomByHotelAndType = "select * from Room where Hotel_No=@hotelno And Types=@Types";
        private string roomFromId = "select * from Room where Room_No=@roomno And Hotel_No=@hotelno";
        private string insertRoom = "insert into Room Values (@RoomNo, @HotelNo, @Types, @Price)";
        private string updateSql = "update Room " +
                                   "set Room_No=@RoomNumber, Hotel_No=@HotelNumber, Types=@Types, Price=@Price " +
                                   "where Room_No=@RoomNumber and Hotel_No=@HotelNumber";
        private string deleteRoom = "delete from Room where Room_No=@RoomNo And Hotel_No=@HotelNo";
        //Implementer metoderne som der skal ud fra interfacet

        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }

        public RoomService(string connnectionString) : base(connnectionString)
        {
        }

        public async Task<List<Room>> GetAllRoom()
        {
            List<Room> rooms = new List<Room>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString2, connection);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int nr = reader.GetInt32(0);
                    int hotelnumber = reader.GetInt32(1);
                    string types = reader.GetString(2);
                    double pris = reader.GetDouble(3);
                    Room room = new Room(nr, types, pris, hotelnumber);
                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public async Task<List<Room>> GetAllRoomFromHotel(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@hotelno", hotelNr);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int nr = reader.GetInt32(0);
                    int hotelnumber = reader.GetInt32(1);
                    string types = reader.GetString(2);
                    double pris = reader.GetDouble(3);
                    Room room = new Room(nr, types, pris, hotelnumber);
                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public async Task<List<Room>> GetAllRoomFromHotelByType(int hotelNr, string type)
        {
            List<Room> rooms = new List<Room>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(RoomByHotelAndType, connection);
                command.Parameters.AddWithValue("@hotelno", hotelNr);
                command.Parameters.AddWithValue("@Types", type);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int nr = reader.GetInt32(0);
                    int hotelnumber = reader.GetInt32(1);
                    string types = reader.GetString(2);
                    double pris = reader.GetDouble(3);
                    Room room = new Room(nr, types, pris, hotelnumber);
                    rooms.Add(room);
                }
            }

            return rooms;
        }
        public async Task<Room> GetRoomFromId(int roomNr, int hotelNr)
        {
            Room room = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(roomFromId, connection);
                command.Parameters.AddWithValue("@hotelno", hotelNr);
                command.Parameters.AddWithValue("@roomno", roomNr);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int nr = reader.GetInt32(0);
                    int hotelnumber = reader.GetInt32(1);
                    string types = reader.GetString(2);
                    double pris = reader.GetDouble(3);
                    room = new Room(nr, types, pris, hotelnumber);
                }
            }

            return room;
        }

        public async Task<bool> CreateRoom(int hotelNr, Room room)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertRoom, connection);
                command.Parameters.AddWithValue("@RoomNo", room.RoomNr);
                command.Parameters.AddWithValue("@HotelNo", room.HotelNr);
                command.Parameters.AddWithValue("@Types", room.Types);
                command.Parameters.AddWithValue("@Price", room.Pris);
                await command.Connection.OpenAsync();

                int noOfRows = await command.ExecuteNonQueryAsync();//bruges ved update, delete, insert
                if (noOfRows == 1)
                {
                    return true;
                }
                return false;

            }
        }

        public async Task<bool> UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@Types", room.Types);
                command.Parameters.AddWithValue("@Price", room.Pris);
                command.Parameters.AddWithValue("@RoomNumber", roomNr);
                command.Parameters.AddWithValue("@HotelNumber", hotelNr);
                await command.Connection.OpenAsync();
                int affectedRows = await command.ExecuteNonQueryAsync();
                if (affectedRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<Room> DeleteRoom(int roomNr, int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteRoom, connection);
                command.Parameters.AddWithValue("@RoomNo", roomNr);
                command.Parameters.AddWithValue("@HotelNo", hotelNr);
                await command.Connection.OpenAsync();
                int affectedRows = await command.ExecuteNonQueryAsync();

            }

            return await GetRoomFromId(roomNr, hotelNr);
        }

        
    }
}
