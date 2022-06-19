using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour, MoneyChangedEventListener
{
    public MoneyChangedEvent moneyChangedEvent;
    public InventoryController userInventory;
    TextMeshProUGUI text;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void OnMoneyChanged(int quantity)
    {
        text.text = quantity.ToString();
    }

    private void OnEnable()
    {
        moneyChangedEvent.moneyChanged += OnMoneyChanged;
        text.text = userInventory.GetMoney().ToString();
    }

    private void OnDisable()
    {
        moneyChangedEvent.moneyChanged -= OnMoneyChanged;
    }
}
