using UnityEngine;

public class BossRoomLogic : IRoomLogic
{
    public bool Closed { get; set; } = true;

    public RoomType GetRoomType(int random)
    {
        return RoomType.Boss;
    }

    public int GetMaxCount()
    {
        //Make some code for when we do jazz and edm era
        return 1;
    }
}
