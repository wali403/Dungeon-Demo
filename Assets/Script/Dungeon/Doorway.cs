using UnityEngine;
[System.Serializable]

public class Doorway
{
    public Vector2Int position;//洞口位置
    public Orientation orientation;//洞口方向
    public GameObject doorPrefab;//上锁洞口的预制体

    #region Header
    [Header("门洞开始复制的位置")]
    #endregion
    public Vector2Int doorwayStartCopyPosition;
    #region Header
    [Header("门洞应该复制的宽度")]
    #endregion
    public int doorwayCopyTileWidth;
    #region Header
    [Header("门洞应该复制的高度")]
    #endregion
    public int doorwayCopyTileHeight;
    [HideInInspector]
    public bool isConnected = false;
    [HideInInspector]
    public bool isUnavailable = false;
}
