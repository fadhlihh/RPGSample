using Fadhli.Framework;
using UnityEngine;

public class HUDManager : SingletonBehaviour<HUDManager>
{
    [SerializeField]
    private CharacterUI _characterUI;
    [SerializeField]
    private WeaponSlotUI _weaponSlotUI;
    [SerializeField]
    private ItemSlotUI _itemSlotUI;

    public CharacterUI CharacterUI { get => _characterUI; }
    public WeaponSlotUI WeaponSlotUI { get => _weaponSlotUI; }
    public ItemSlotUI ItemSlotUI { get => _itemSlotUI; }

    private void Awake()
    {
        if (!_characterUI)
        {
            _characterUI = FindAnyObjectByType<CharacterUI>();
        }
        if (!_weaponSlotUI)
        {
            _weaponSlotUI = FindAnyObjectByType<WeaponSlotUI>();
        }
        if (!_itemSlotUI)
        {
            _itemSlotUI = FindAnyObjectByType<ItemSlotUI>();
        }
    }
}
