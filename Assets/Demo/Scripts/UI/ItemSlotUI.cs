using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _itemSlotUI;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TMP_Text _quantityText;

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetQuantityText(int value)
    {
        _quantityText.text = value.ToString();
    }

    public void Show()
    {
        _itemSlotUI.SetActive(true);
    }

    public void Hide()
    {
        _itemSlotUI.SetActive(false);
    }
}
