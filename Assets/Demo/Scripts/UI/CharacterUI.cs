using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;

    public void SetHealthBarValue(float value)
    {
        _healthBar.fillAmount = value / 100;
    }
}
