using UnityEngine;

public interface IRoomLogic
{
    bool Closed { get; set; }

    RoomType GetRoomType(int random);
    int GetMaxCount();
}
