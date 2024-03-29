﻿using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoomDAO
    {
        public static List<Room> GetAllRooms()
        {
            var listRooms = new List<Room>();
            try
            {
                using(var context = new BirthdayPartyBookingForKids_DBContext())
                {
                    listRooms = context.Rooms.ToList();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRooms;
        }

        public static Room GetRoomByID(string id)
        {
            var room = new Room();
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                room = context.Rooms.FirstOrDefault(x => x.LocationId==id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return room;
        }
        public static void SaveRoomn(Room room)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                context.Rooms.Add(room);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateRoomn(RoomDto roomDto)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var room = new Room
                {
                    LocationId = roomDto.LocationId,
                    LocationName = roomDto.LocationName,
                    Description = roomDto.Description,
                    Price = roomDto.Price,
                };
                context.Entry<Room>(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteRoomn(Room room)
        {
            try
            {
                using var context = new BirthdayPartyBookingForKids_DBContext();
                var checkRoom = context.Rooms.FirstOrDefault(p => p.LocationId.Equals(room.LocationId));
                context.Rooms.Remove(checkRoom);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
