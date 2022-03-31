using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace BattleRoyale.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Battle Royale/Weapon Data")]
    public class WeaponsStatsSO : ScriptableObject
    {
        [SerializeField] private NetworkIdentity _bulletPrefab;
        [SerializeField] private string _animatorParameter;

        public GameObject Bullet => _bulletPrefab.gameObject;
        public string AnimatorParameter => _animatorParameter.ToString();
    }
}
