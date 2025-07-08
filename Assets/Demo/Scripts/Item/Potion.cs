using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Potion", order = 0)]
public class Potion : ItemData
{
    public int EffectValue;
    public GameObject HealingVFX;
    public override void Use(Character instigator)
    {
        IDamagable damagable = instigator.GetComponent<IDamagable>();
        damagable?.Heal(EffectValue);
        Instantiate(HealingVFX, instigator.transform.position, Quaternion.identity);
    }
}
