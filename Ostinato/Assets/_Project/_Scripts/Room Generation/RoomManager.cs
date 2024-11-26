using UnityEngine;
using Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] private List<GameObject> roomPrefabs;
    private GameObject roomPrefab;
    private List<RoomType> rooms;
    private Room room;
    private RoomTransition roomTransition;
    private IEraGenerationStrategy eraGenerationStrategy;
    private int indexCounter = 0;

    private readonly static Dictionary<RoomType, IRoomLogic> roomLogicLookup = new Dictionary<RoomType, IRoomLogic>
    {
        {RoomType.Enemy, new EnemyRoomLogic()},
        {RoomType.Elite, new EliteRoomLogic()},
        {RoomType.Judge, new JudgeRoomLogic()},
        {RoomType.Riff, new RiffRoomLogic()},
        {RoomType.Tutorial, new TutorialRoomLogic()},
        {RoomType.Shop, new ShopRoomLogic()},
        {RoomType.Boss, new BossRoomLogic()},
    };

    //We dont need this other then base.Awake() in room manager, just for testing generating a era
    override protected void Awake()
    {
        RandomGen.Initialize(123);

        InitializeEra(new ClassicalGenerationStrategy());

        base.Awake();
    }

    public void InitializeEra(IEraGenerationStrategy strategy)
    {
        SetEraGenerationStrategy(strategy);
        GenerateEra();
        GenerateRoom(indexCounter);
    }

    public void SetEraGenerationStrategy(IEraGenerationStrategy strategy)
    {
        eraGenerationStrategy = strategy;
    }

    public void GenerateEra()
    {
        rooms = eraGenerationStrategy.GenerateEra();
    }

    public void GenerateRoom(int index)
    {
        roomPrefab = roomPrefabs.FirstOrDefault(prefab => prefab.name == rooms[index].ToString());
        if(roomPrefab != null)
        {
            roomPrefab = Instantiate(roomPrefab);
            room = roomPrefab.GetComponent<Room>();
            room.Initialize(roomLogicLookup[rooms[index]]);
            room.CheckDoorLogic();
            roomTransition = room.GetRoomTransition();
        }
        else
        {
            Debug.LogError($"Room prefab {rooms[index]} is not in the roomPrefabs list.");
        }
    }

    public void AdvanceRoom()
    {
        indexCounter++;
        Destroy(roomPrefab);
        GenerateRoom(indexCounter);
    }

    public void ResetEra()
    {
        indexCounter = 0;
        Destroy(roomPrefab);
    }

    public void LockDoor()
    {
        roomTransition.EnableDoor();
    }

    public void UnlockDoor()
    {
        roomTransition.DisableDoor();
    }
}
