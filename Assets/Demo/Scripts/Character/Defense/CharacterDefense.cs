using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDefense : MonoBehaviour
{
    [SerializeField]
    private int _damageModifier;

    public UnityEvent OnStartBlock;
    public UnityEvent OnStopBlock;
    public UnityEvent OnParry;

    public bool IsBlocking { get; private set; }
    public bool IsParrying { get; set; }
    public int DamageModifier { get => _damageModifier; }

    public void StartBlock()
    {
        IsBlocking = true;
        OnStartBlock?.Invoke();
    }

    public void StopBlock()
    {
        IsBlocking = false;
        OnStopBlock?.Invoke();
    }

    public void Parry()
    {
        IsParrying = true;
        OnParry?.Invoke();
    }

    public void StopParry()
    {
        IsParrying = false;
    }
}
