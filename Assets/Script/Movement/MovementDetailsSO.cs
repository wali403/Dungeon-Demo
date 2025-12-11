using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "ScriptableObjects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{
    #region Header MOVEMENT DETAILS
    [Space(10)]
    [Header("MOVEMENT DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("最小移动速度,用于GetMoveSpeed方法中在最大移速和最小移速中生成一个数值")]
    #endregion Tooltip
    public float minMoveSpeed = 8f;
    #region Tooltip
    [Tooltip("最大移动速度,用于GetMoveSpeed方法中在最大移速和最小移速中生成一个数值")]
    #endregion Tooltip
    public float maxMoveSpeed = 8f;

    //在最大移速和最小移速中生成一个数值
    public float GetMoveSpeed()
    {
        if (minMoveSpeed == maxMoveSpeed)
        {
            return minMoveSpeed;
        }
        else
        {
            return Random.Range(minMoveSpeed, maxMoveSpeed);
        }
    }

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(minMoveSpeed), minMoveSpeed, nameof(maxMoveSpeed), maxMoveSpeed, false);
    }

#endif
    #endregion
}
