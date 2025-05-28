using Fadhli.Game.Module;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _target;
    [SerializeField]
    private float _minimumDistance;

    private void Awake()
    {
        if (!_target)
        {
            _target = FindAnyObjectByType<PlayerCharacter>();
        }
    }

    public GameObject GetTarget()
    {
        if (!_target)
        {
            return null;
        }
        return Vector3.Distance(transform.position, _target.transform.position) < _minimumDistance ? _target.gameObject : null;
    }
}
