using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    private int startingHealth;//初始血量
    private int currentHealth;//当前血量
    
    //设置初始血量
    public void SetStartingHealth(int sartingHealth)
    {
        this.startingHealth = sartingHealth;
        currentHealth = sartingHealth;
    }

    //获取初始血量
    public int GetStartringHealth()
    {
        return startingHealth;
    }
}
