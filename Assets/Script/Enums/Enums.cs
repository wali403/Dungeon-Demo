
public enum Orientation
{
    north,
    east,
    south,
    west,
    none
}

public enum AimDirection
{
    right,
    left
}

public enum GameState
{
    gameStarted,//游戏开始
    playingLevel,//进行游戏中
    engagingEnemies,//进入房间，与敌人交战
    bossStage,//打败敌人，进入boss战
    engagingBoss,//与boss交战
    levelComplated,//通关关卡
    gameWon,//赢得游戏
    gameLost,//输掉游戏
    gamePaused,//暂停游戏
    dungeonOverviewMap,//浏览小地图
    restartGame//重开游戏
}