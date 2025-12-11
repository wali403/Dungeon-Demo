using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "ScriptableObjects/Weapons/Weapon Details")]
public class WeaponsDetailsSO : ScriptableObject
{
    #region Header WEAPON BASE DETAILS
    [Space(10)]
    [Header("WEAPON BASE DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("武器名字")]
    #endregion Tooltip
    public string weaponName;
    #region Tooltip
    [Tooltip("武器精灵,应该开启精灵中的 generate physics shape 选项")]
    #endregion Tooltip
    public Sprite weaponSprite;

    #region Header WEAPON CONFIGURATION
    [Space(10)]
    [Header("WEAPON CONFIGURATION")]
    #endregion
    #region Tooltip
    [Tooltip("武器发射位置 - 从精灵轴心点算起的枪口的位置")]
    #endregion Tooltip
    public Vector3 weaponShootPosition;
    //#region Tooltip
    //[Tooltip("武器当前的子弹")]
    //#endregion Tooltip
    //public AmmoDetailsSO weaponCurrentAmmo;

    #region Header WEAPON CONFIGURATION
    [Space(10)]
    [Header("WEAPON OPERATING VALUES")]
    #endregion
    #region Tooltip
    [Tooltip("是否拥有无限总弹药")]
    #endregion Tooltip
    public bool hasInfiniteAmmo = false;
    #region Tooltip
    [Tooltip("是否拥有无限弹夹弹药")]
    #endregion Tooltip
    public bool hasInfiniteClipCapacity = false;
    #region Tooltip
    [Tooltip("武器弹夹弹药")]
    #endregion Tooltip
    public int weaponClipAmmoCapacity = 6;
    #region Tooltip
    [Tooltip("武器总弹药")]
    #endregion Tooltip
    public int weaponAmmoCapacity = 100;
    #region Tooltip
    [Tooltip("发射子弹的间隔 - 多少秒发射一次")]
    #endregion Tooltip
    public float weaponFireRate = 0.2f;
    #region Tooltip
    [Tooltip("武器预充能时间 - 发射之前需要按住发射键多少秒")]
    #endregion Tooltip
    public float weaponPrechargeTime = 0f;
    #region Tooltip
    [Tooltip("武器换弹时间")]
    #endregion Tooltip
    public float weaponReloadTime = 0f;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(weaponName), weaponName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(weaponSprite), weaponSprite);
        //HelperUtilities.ValidateCheckNullValue(this, nameof(weaponCurrentAmmo), weaponCurrentAmmo);

        HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponFireRate), weaponFireRate, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponPrechargeTime), weaponPrechargeTime, true);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponReloadTime), weaponReloadTime, true);


        if (!hasInfiniteAmmo)
        {
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponAmmoCapacity), weaponAmmoCapacity, false);
        }

        if (!hasInfiniteClipCapacity)
        {
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponClipAmmoCapacity), weaponClipAmmoCapacity, false);
        }
    }

#endif
    #endregion
}
