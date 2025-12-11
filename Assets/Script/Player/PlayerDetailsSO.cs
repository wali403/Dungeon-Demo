using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "ScriptableObjects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    #region Header PLAYER BASE DETAILS
    [Space(10)]
    [Header("PLAYER BASE DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("玩家名字")]
    #endregion
    public string playerCharacterName;

    #region Tooltip
    [Tooltip("玩家预制体")]
    #endregion
    public GameObject playerPrefab;

    #region Tooltip
    [Tooltip("玩家运行时的动画控制器")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header HEALTH
    [Space(10)]
    [Header("HEALTH")]
    #endregion
    #region Tooltip
    [Tooltip("玩家的健康数值")]
    #endregion
    public int playerHealthAmount;

    #region Header OTHER
    [Space(10)]
    [Header("OTHER")]
    #endregion
    #region Tooltip
    [Tooltip("玩家在小地图上的图标")]
    #endregion
    public Sprite playerMinimapIcon;

    #region Tooltip
    [Tooltip("玩家的手部精灵")]
    #endregion
    public Sprite playerHandSprite;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerMinimapIcon), playerMinimapIcon);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerHandSprite), playerHandSprite);
    }
#endif
    #endregion
}
