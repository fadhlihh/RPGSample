using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fadhli.Game.Module
{
    public class EnemyAnimation : CharacterAnimation
    {
        public void OnCharacterBeginHit()
        {
            _animator.Play("Hit_In_Place");
        }
    }
}
