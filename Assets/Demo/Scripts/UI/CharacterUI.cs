using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private Image _staminaBar;
    [SerializeField]
    private GameObject _aimCrosshair;

    public void SetHealthBarValue(float value)
    {
        _healthBar.fillAmount = value / 100;
    }

    public void SetStaminaBarValue(float value)
    {
        _staminaBar.fillAmount = value / 100;
    }

    public void ShowAimCrosshair()
    {
        _aimCrosshair.SetActive(true);
    }

    public void HideAimCrosshair()
    {
        _aimCrosshair.SetActive(false);
    }
}
