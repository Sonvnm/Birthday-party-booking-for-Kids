using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoties.IRepository
{
    public interface IRoomRepository
    {
        void SaveRoom(Room room);
        Room GetRoomById(string roomId);
        void DeleteRoom(Room room);
        void UpdateRoom(RoomDto roomDto);
        List<Room> GetAllRooms();

    }
}
