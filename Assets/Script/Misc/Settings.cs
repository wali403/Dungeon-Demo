using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ROOM SETTINGS

    public const int maxChildCorridors = 3;//单个房间可生成的最大子走廊数（虽然允许设置为3，
    //但是不推荐该最大值,可能会使地牢生成失败,因为这会使房间更难合理衔接）

    #endregion
}
