using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image _icon;

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
}
