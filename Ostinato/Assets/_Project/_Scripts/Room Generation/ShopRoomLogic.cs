using UnityEngine;

public class ShopRoomLogic : IRoomLogic
{
    public bool Closed { get; set; } = false;

    public RoomType GetRoomType(int random)
    {
        return RoomType.Shop;
    }

    public int GetMaxCount()
    {
        //Make some code for when we do jazz and edm era
        return 1;
    }
}
