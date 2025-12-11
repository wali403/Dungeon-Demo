using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayer_", menuName = "ScriptableObjects/Player/Current Player")]
public class CurrentPlayerSO : ScriptableObject
{
    public PlayerDetailsSO playerDetails;
    public string playerName;//玩家输入的名字
}
