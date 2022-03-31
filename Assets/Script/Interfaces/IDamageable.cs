using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleRoyale.Interfaces
{
    public interface IDamageable
    {
        int Health { get; }
        void Damage(int damageAmount);
    }

}
