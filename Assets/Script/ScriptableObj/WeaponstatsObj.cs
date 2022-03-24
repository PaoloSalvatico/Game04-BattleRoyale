using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace BattleRoyale
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Battle Royale/Weapon Data")]
    public class WeaponstatsObj : ScriptableObject
    {
        [SerializeField] NetworkIdentity _bulletPrefab;
        [SerializeField] string _animatorParameter;

        public GameObject Bullet => _bulletPrefab.gameObject;
        public string AnimatorParameter => _animatorParameter;
    }
}

