using UnityEngine;

public class TutorialRoomLogic : IRoomLogic
{
    public bool Closed { get; set; } = true;

    public RoomType GetRoomType(int random)
    {
        return RoomType.Tutorial;
    }

    public int GetMaxCount()
    {
        //Make some code for when we do jazz and edm era
        return 1;
    }
}
