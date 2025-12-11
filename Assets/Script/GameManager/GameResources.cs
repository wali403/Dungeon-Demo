using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                //加载Resources文件夹中一个叫GameResources的对象,获取GameResources组件
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    #region Header DUNGEON
    [Space(10)]
    [Header("DUNGEON")]
    #endregion
    #region Tooltip
    [Tooltip("填入房间类型列表")]
    #endregion
    public RoomNodeTypeListSO roomNodeTypeList;

    #region Header PLAYER
    [Space(10)]
    [Header("PLAYER")]
    #endregion
    #region Tooltip
    [Tooltip("当前玩家的SO，用于存储不同场景下的当前玩家数据")]
    #endregion
    public CurrentPlayerSO currentPlayer;

    #region Header MATERIALS
    [Space(10)]
    [Header("MATERIALS")]
    #endregion
    #region Tooltip
    [Tooltip("Dimmed Material")]
    #endregion
    public Material dimmedMaterial;//灰暗材质

    #region Tooltip
    [Tooltip("Sprite-Lit-Default Material")]
    #endregion
    public Material litMaterial;

    #region Tooltip
    [Tooltip("Populate whit the Variable Lit Shader")]
    #endregion
    public Shader variableLitShader;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(roomNodeTypeList), roomNodeTypeList);
        HelperUtilities.ValidateCheckNullValue(this, nameof(currentPlayer), currentPlayer);
        HelperUtilities.ValidateCheckNullValue(this, nameof(dimmedMaterial), dimmedMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(litMaterial), litMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(variableLitShader), variableLitShader);
    }

#endif
    #endregion
}
