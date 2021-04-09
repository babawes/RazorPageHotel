using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using RazorPageHotel.Interfaces;
using RazorPageHotel.Models;

namespace RazorPageHotel.Services
{
    public class HotelService : Connection, IHotelService
    {
        private String queryString = "select * from Hotel";
        private String queryNameString = "select * from Hotel where (Name like '%' + @Navn + '%')";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private String insertSql = "insert into Hotel (Name, Address) Values (@Navn, @Adresse)";
        private String deleteSql = "delete from Hotel where Hotel_No = @ID";
        private String updateSql = "update Hotel " +
                                   "set Name=@Navn, Address=@Adresse " +
                                   "where Hotel_No = @ID";

        public HotelService(IConfiguration configuration) : base(configuration)
        {

        }
        public HotelService(string connnectionString) : base(connnectionString)
        {
        }


        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                await command.Connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int hotelNr = reader.GetInt32(0);
                    String hotelNavn = reader.GetString(1);
                    String hotelAdr = reader.GetString(2);
                    Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                    hoteller.Add(hotel);
                }
            }
            return hoteller;
        }


        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            Hotel hotel = null;

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int thotelNr = reader.GetInt32(0);
                    String hotelNavn = reader.GetString(1);
                    String hotelAdr = reader.GetString(2);
                    hotel = new Hotel(thotelNr, hotelNavn, hotelAdr);

                }

            }
            return hotel;
        }

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                await command.Connection.OpenAsync();

                int noOfRows = await command.ExecuteNonQueryAsync();//bruges ved update, delete, insert
                if (noOfRows == 1)
                {
                    return true;
                }
                return false;

            }
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                await command.Connection.OpenAsync();
                int affectedRows = await command.ExecuteNonQueryAsync();
                if (affectedRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);
                await command.Connection.OpenAsync();
                int affectedRows = await command.ExecuteNonQueryAsync();

            }

            return await GetHotelFromIdAsync(hotelNr);
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryNameString, connection);
                command.Parameters.AddWithValue("@Navn", name);
                await command.Connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int hotelNr = reader.GetInt32(0);
                    String hotelNavn = reader.GetString(1);
                    String hotelAdr = reader.GetString(2);
                    Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                    hoteller.Add(hotel);
                }
            }
            return hoteller;
        }


    }
}
