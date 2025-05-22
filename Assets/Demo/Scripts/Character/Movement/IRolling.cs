using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IRolling
{
    public UnityEvent OnCharacterRoll { get; }
    public bool IsRolling { get; }

    public void Roll();
    public void EndRoll();
}
