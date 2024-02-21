using BusinessObject.Models;
using DataAccess;
using Repositoties.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.Repository
{
    public class RoomRepository : IRoomRepository
    {
        public void DeleteRoom(Room room)
        => RoomDAO.DeleteRoomn(room);

        public List<Room> GetAllRooms()
        =>RoomDAO.GetAllRooms();

        public Room GetRoomById(int roomId)
        =>RoomDAO.GetRoomByID(roomId);

        public void SaveRoom(Room room)
        =>RoomDAO.SaveRoomn(room);

        public void UpdateRoom(Room room)
        =>RoomDAO.UpdateRoomn(room);
    }
}
