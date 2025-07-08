using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Canvas _enemyUI;
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private Image _target;

    private void Update()
    {
        _enemyUI.transform.LookAt(Camera.main.transform);
    }

    public void SetHealthBarValue(float value, float maxHealthPoint)
    {
        _healthBar.fillAmount = value / maxHealthPoint;
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
