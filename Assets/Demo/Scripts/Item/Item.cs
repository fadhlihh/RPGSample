using System;
using Fadhli.Game.Module;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField]
    private ItemData _itemData;
    [SerializeField]
    private int _quantity;

    public Action<Item> OnEmptyItem;

    public ItemData ItemData { get => _itemData; }
    public int Quantity { get => _quantity; }

    public Item(ItemData item, int quantity)
    {
        _itemData = item;
        _quantity = quantity;
    }

    public void AddQuantity(int value)
    {
        _quantity = _quantity + value;
    }

    public void RemoveQuantity(int value)
    {
        _quantity = _quantity - value;
    }

    public void Use(Character instigator)
    {
        ItemData.Use(instigator);
        RemoveQuantity(1);
        if (Quantity <= 0)
        {
            OnEmptyItem?.Invoke(this);
        }
    }
}
