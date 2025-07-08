using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _items = new List<Item>();
    [SerializeField]
    private Character _ownerCharacter;

    private int _currentIndex = 0;

    public List<Item> Items { get => _items; }

    private void Awake()
    {
        if (!_ownerCharacter)
        {
            _ownerCharacter = GetComponent<Character>();
        }
        if (Items.Count > 0)
        {
            _items[_currentIndex].OnEmptyItem += OnRemoveItem;
            UpdateItemSlotUI(_items[_currentIndex].ItemData.Icon, _items[_currentIndex].Quantity, true);
        }
    }

    public void NextItem()
    {
        _items[_currentIndex].OnEmptyItem -= OnRemoveItem;
        if (_currentIndex < Items.Count - 1)
        {
            _currentIndex = _currentIndex + 1;
        }
        else
        {
            _currentIndex = 0;
        }
        _items[_currentIndex].OnEmptyItem += OnRemoveItem;
        UpdateItemSlotUI(_items[_currentIndex].ItemData.Icon, _items[_currentIndex].Quantity, true);
    }

    public void UseItem()
    {
        _items[0].Use(_ownerCharacter);
        SFXManager.Instance.PlayAudio(ESFXType.Heal);
        if (_items.Count > 0)
        {
            UpdateItemSlotUI(_items[_currentIndex].ItemData.Icon, _items[_currentIndex].Quantity, true);
        }
    }

    public void OnRemoveItem(Item item)
    {
        _items[_currentIndex].OnEmptyItem -= OnRemoveItem;
        _items.Remove(item);
        if (Items.Count > 0)
        {
            _items[_currentIndex].OnEmptyItem += OnRemoveItem;
            UpdateItemSlotUI(_items[_currentIndex].ItemData.Icon, _items[_currentIndex].Quantity, true);
        }
        else
        {
            UpdateItemSlotUI(null, 0, false);
        }
    }

    public void UpdateItemSlotUI(Sprite icon, int quantity, bool active)
    {
        HUDManager.Instance.ItemSlotUI.SetIcon(icon);
        HUDManager.Instance.ItemSlotUI.SetQuantityText(quantity);
        if (active)
        {
            HUDManager.Instance.ItemSlotUI.Show();
        }
        else
        {
            HUDManager.Instance.ItemSlotUI.Hide();
        }
    }
}
