using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    private IRoomLogic roomLogic;
    private RoomTransition roomTransition;

    void Awake()
    {
        roomTransition = GetComponentInChildren<RoomTransition>();
    }

    public void Initialize(IRoomLogic type)
    {
        roomLogic = type;
    }

    public RoomTransition GetRoomTransition()
    {
        return roomTransition;
    }

    public void CheckDoorLogic()
    {
        if(roomLogic.Closed)
        {
            roomTransition.DisableDoor();
        }
    }
}
