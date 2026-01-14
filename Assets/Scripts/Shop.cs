using System;
using UnityEngine;

[System.Serializable]
public class Shop
{
    public int playerMoney = 0;

    // Hàm thêm ti?n
    public void AddMoney(int amount)
    {
        playerMoney += amount;
    }
}
