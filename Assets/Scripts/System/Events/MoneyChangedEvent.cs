using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/MoneyChanged")]
public class MoneyChangedEvent : ScriptableObject
{
    public event Action<int> moneyChanged;

    public void Raise(int quantity)
    {
        if (moneyChanged!= null) moneyChanged(quantity);
    }
}
