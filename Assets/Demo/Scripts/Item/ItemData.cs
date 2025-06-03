using Fadhli.Game.Module;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public abstract void Use(Character instigator);
}
