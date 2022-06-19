using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoneyChangedEventListener
{
    public abstract void OnMoneyChanged(int quantity);
}
