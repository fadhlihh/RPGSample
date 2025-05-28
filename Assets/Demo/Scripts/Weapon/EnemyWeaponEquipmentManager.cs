using UnityEngine;

public class EnemyWeaponEquipmentManager : WeaponEquipmentManager
{
    [SerializeField]
    private Transform _swordSocket;

    protected override void InitSocket()
    {
        if (_swordSocket != null)
        {
            _weaponSockets.Add(EWeaponType.Melee, _swordSocket);
        }
    }
}
