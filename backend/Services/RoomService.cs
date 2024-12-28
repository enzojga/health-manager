public class RoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms;
    }

    public async Task<Room> GetRoomByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null)
        {
            return null;
        }

        return room;
    }

    public async Task<Room> CreateRoomAsync(RoomDto roomDto)
    {
        var room = new Room
        {
            Capacity = roomDto.Capacity,
        };

        return await _roomRepository.AddAsync(room);
    }

    public async Task<bool> UpdateRoomAsync(int id, RoomDto roomDto)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null)
        {
            return false;
        }

        room.Capacity = roomDto.Capacity;

        await _roomRepository.UpdateAsync(room);
        return true;
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null)
        {
            return false;
        }
        await _roomRepository.DeleteAsync(room);
        return true;
    }
}