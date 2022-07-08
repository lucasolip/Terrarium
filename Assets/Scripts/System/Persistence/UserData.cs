using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UserData
{
    public string username;
    public DateTime? bestBornTime;
    public DateTime? bestDiedTime;

    public UserData(string username, DateTime? bestBornTime, DateTime? bestDiedTime)
    {
        this.username = username;
        this.bestBornTime = bestBornTime;
        this.bestDiedTime = bestDiedTime;
    }
}
