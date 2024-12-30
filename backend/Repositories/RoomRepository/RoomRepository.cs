using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
namespace backend.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MyDbContext _context;

        public RoomRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
            .Include(room => room.Patients)
            .ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms
            .Include(room => room.Patients)
            .FirstAsync(room => room.Id == id);
        }

        public async Task<Room> AddAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}