using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]//不允许创建多个相同的组件,防止相关行为引发的错误
public class GameManager : SingletonMonobehaviour<GameManager>
{
    #region Header DUNGEON LEVELS

    [Space(10)]
    [Header("DUNGEON LEVELS")]

    #endregion Header DUNGEON LEVELS

    #region Tooltip

    [Tooltip("用地牢关卡SO来进行填充")]

    #endregion Tooltip

    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;

    #region Tooltip

    [Tooltip("填入起始的地牢关卡等级进行测试,第一个关卡 = 0")]

    #endregion Tooltip

    [SerializeField] private int currentDungeonLevelIndex = 0;
    private Room currentRoom;
    private Room previousRoom;
    private PlayerDetailsSO playerDetails;
    private Player player;

    [HideInInspector] public GameState gameState;//游戏状态

    protected override void Awake()
    {
        base.Awake();

        playerDetails = GameResources.Instance.currentPlayer.playerDetails;

        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        GameObject playerGameObject = Instantiate(playerDetails.playerPrefab);

        player = playerGameObject.GetComponent<Player>();

        player.Initialize(playerDetails);
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
    }

    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }

    private void Start()
    {
        gameState = GameState.gameStarted;
    }

    private void Update()
    {
        HandleGameState();

        //用于测试,按R键快速生成地牢
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameState = GameState.gameStarted;
        }
    }

    //处理游戏状态
    private void HandleGameState()
    {
        switch(gameState)
        {
            case GameState.gameStarted:

                //建立第一个关卡
                PlayDungeonLevel(currentDungeonLevelIndex);

                gameState = GameState.playingLevel;

                break;
        }
    }

    //设置当前房间
    public void SetCurrentRoom(Room room)
    {
        previousRoom = currentRoom;
        currentRoom = room;
    }

    //创建地牢关卡
    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        bool dungeonBuiltSuccessfully = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSuccessfully)
        {
            Debug.LogError("Couldn't build dungeon from specified rooms and node graphs");
        }

        //显示初始房间
        StaticEventHandler.CallRoomChangedEvent(currentRoom);

        player.gameObject.transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f,
            (currentRoom.lowerBounds.y + currentRoom.upperBounds.y) / 2f, 0f);

        //获取离玩家最近的出生点的世界坐标,让玩家出生在这个地方
        player.gameObject.transform.position = HelperUtilities.GetSpawnPositionNearestToPlayer(player.transform.position);

    }

    public Player GetPlayer()
    {
        return player;
    }

    //返回当前房间
    public Room GetCurrentRoom()
    {
        return currentRoom;
    }

    //检验,防止变量出现空值
    #region Validation

#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(dungeonLevelList), dungeonLevelList);
    }

#endif

    #endregion Validation

}
