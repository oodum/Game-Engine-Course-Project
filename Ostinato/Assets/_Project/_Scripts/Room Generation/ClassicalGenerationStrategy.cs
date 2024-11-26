using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassicalGenerationStrategy : IEraGenerationStrategy
{
    private IRoomLogic[] roomLogics;
    private int roomCap = 10;
    private int[] specialClassicalRooms = { 0, 8, 9 };

    public ClassicalGenerationStrategy()
    {
        //Basically what rooms can show up in Classical Era
        roomLogics = new IRoomLogic[]
        {
            new EnemyRoomLogic(),
            new EliteRoomLogic(),
            new RiffRoomLogic(),
            new JudgeRoomLogic(),
        };
    }

    public List<RoomType> GenerateEra()
    {
        List<RoomType> rooms = new List<RoomType>();

        for(int i = 0; i < roomCap; i++)
        {
            int random = RandomGen.Get("RoomGenerator");
            int index = random % roomLogics.Length;

            rooms.Add(roomLogics[index].GetRoomType(random));
            if(!RoomChecker.CheckRoom(rooms[i], roomLogics, specialClassicalRooms, i))
            {
                rooms[i] = RoomType.Enemy;
            }
            Debug.Log($"iteration {i} is {rooms[i]}");
        }

        RoomChecker.ResetRoomCounts();
        AssignSpecialRooms(rooms);
        return rooms;
    }

    private void AssignSpecialRooms(List<RoomType> rooms)
    {
        rooms[0] = RoomType.Tutorial;
        rooms[8] = RoomType.Shop;
        rooms[9] = RoomType.Boss;
    }
}
