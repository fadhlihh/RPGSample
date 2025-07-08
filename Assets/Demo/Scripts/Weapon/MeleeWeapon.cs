public class MeleeWeapon : Weapon
{
    public override EWeaponType Type => EWeaponType.Melee;

    public override void HeavyAttack()
    {
        _damageModifier = 10;
    }

    public override void StartTraceHit()
    {
        base.StartTraceHit();
        SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordSlash, 0.5f, 1);
    }
}
