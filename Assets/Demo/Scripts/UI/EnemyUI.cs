using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private Image _target;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetHealthBarValue(float value)
    {
        _healthBar.fillAmount = value / 100;
    }

    public void ShowTarget()
    {
        _target.gameObject.SetActive(true);
    }

    public void HideTarget()
    {
        _target.gameObject.SetActive(false);
    }
}
