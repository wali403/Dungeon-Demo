using UnityEngine;

[CreateAssetMenu(fileName = "AmmoDetails_", menuName = "ScriptableObjects/Weapons/Ammo Details")]
public class AmmoDetailsSO : ScriptableObject
{
    #region Header AMMO BASE DETAILS
    [Space(10)]
    [Header("AMMO BASE DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("子弹名字")]
    #endregion Tooltip
    public string ammoName;
    #region Tooltip
    [Tooltip("是否为玩家使用的子弹")]
    #endregion Tooltip
    public bool isPlayer;

    #region Header AMMO SPRITE, PREFAB & MATERIALS
    [Space(10)]
    [Header("AMMO SPRITE, PREFAB & MATERIALS")]
    #endregion
    #region Tooltip
    [Tooltip("子弹使用的精灵")]
    #endregion Tooltip
    public Sprite ammoSprite;
    #region Tooltip
    [Tooltip("子弹使用的预制体,通常情况下只有一个,如果填入了多个预制体," +
        "那么将从中随机选取一种.该预制体可以是一种弹药模式,但要求实现IFireable接口")]
    #endregion Tooltip
    public GameObject[] ammoPrefabArray;
    #region Tooltip
    [Tooltip("子弹使用的材质")]
    #endregion Tooltip
    public Sprite ammoMaterial;
    #region Tooltip
    [Tooltip("子弹蓄力时间,当子弹出现后,发射出来需要的秒数")]
    #endregion Tooltip
    public float ammoChargeTime = 0.1f;
    #region Tooltip
    [Tooltip("子弹蓄力材质,用于在子弹蓄力时,所使用的不同的材质")]
    #endregion Tooltip
    public Material ammoChargeMaterial;

    #region Header AMMO BASE PARAMETERS
    [Space(10)]
    [Header("AMMO BASE PARAMETERS")]
    #endregion
    #region Tooltip
    [Tooltip("子弹造成的伤害")]
    #endregion Tooltip
    public int ammoDemage = 1;
    #region Tooltip
    [Tooltip("子弹发射的最小速度(随机值最小值)")]
    #endregion Tooltip
    public float ammoSpeedMin = 20f;
    #region Tooltip
    [Tooltip("子弹发射的最大速度(随机值最大值)")]
    #endregion Tooltip
    public float ammoSpeedMax = 20f;
    #region Tooltip
    [Tooltip("子弹(或子弹模式)的范围为多少unity单位")]
    #endregion Tooltip
    public float ammoRange = 20f;
    #region Tooltip
    [Tooltip("子弹模式中旋转速度为多少度每秒")]
    #endregion Tooltip
    public float ammoRotationSpeed = 1f;

    #region Header AMMO SPREAD DETAILS
    [Space(10)]
    [Header("AMMO SPREAD DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("子弹散射的最小值(随机值最小值) - 更高的散射意味着弹道偏离越多")]
    #endregion Tooltip
    public float ammoSpreadMin = 0f;
    #region Tooltip
    [Tooltip("子弹散射的最大值(随机值最大值) - 更高的散射意味着弹道偏离越多")]
    #endregion Tooltip
    public float ammoSpreadMax = 0f;

    #region Header AMMO SPAWN DETAILS
    [Space(10)]
    [Header("AMMO SPAWN DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("子弹生成数量的最小值(随机值最小值)")]
    #endregion Tooltip
    public int ammoSpawnAmountMin = 1;
    #region Tooltip
    [Tooltip("子弹生成数量的最大值(随机值最大值)")]
    #endregion Tooltip
    public int ammoSpawnAmountMax = 1;
    #region Tooltip
    [Tooltip("子弹生成间隔的最小值(随机值最小值)")]
    #endregion Tooltip
    public float ammoSpawnIntervalMin = 0f;
    #region Tooltip
    [Tooltip("子弹生成间隔的最大值(随机值最大值)")]
    #endregion Tooltip
    public float ammoSpawnIntervalMax = 0f;

    #region Header AMMO TRAIL DETAILS
    [Space(10)]
    [Header("AMMO TRAIL DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("是否有武器痕迹")]
    #endregion Tooltip
    public bool isAmmoTrail = false;
    #region Tooltip
    [Tooltip("武器痕迹的持续时间")]
    #endregion Tooltip
    public float ammoTrailTime = 3f;
    #region Tooltip
    [Tooltip("武器痕迹材质")]
    #endregion Tooltip
    public Material ammoTrailMaterial;
    #region Tooltip
    [Tooltip("武器痕迹的初始宽度")]
    #endregion Tooltip
    [Range(0f, 1f)] public float ammoTrailStartWidth;
    #region Tooltip
    [Tooltip("武器痕迹的结束宽度")]
    #endregion Tooltip
    [Range(0f, 1f)] public float ammoTrailEndWidth;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(ammoName), ammoName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoSprite), ammoSprite);
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(ammoPrefabArray), ammoPrefabArray);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoMaterial), ammoMaterial);
        if (ammoChargeTime > 0f)
            HelperUtilities.ValidateCheckNullValue(this, nameof(ammoChargeMaterial), ammoMaterial);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoDemage), ammoDemage, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpeedMin), ammoSpeedMin, nameof(ammoSpeedMax), ammoSpeedMax, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoRange), ammoRange, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpreadMin), ammoSpreadMin, nameof(ammoSpreadMax), ammoSpreadMax, true);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpawnAmountMin), ammoSpawnAmountMin, nameof(ammoSpawnAmountMax),
            ammoSpawnAmountMax, false);
        //HelperUtilities.ValidateCheckPositiveRange(this, nameof())
    }

#endif
    #endregion
}
