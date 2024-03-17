using BusinessObject.Models;
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
        void UpdateRoom(Room room);
        List<Room> GetAllRooms();

    }
}
