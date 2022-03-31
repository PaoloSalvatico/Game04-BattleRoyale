using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleRoyale.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Battle Royale/Player Data")]
    public class PlayerStatsSO : ScriptableObject
    {
        [Range(1, 10)]
        [SerializeField] private int _level = 1;
        [SerializeField] private float _moveSpeed = 2;
        [SerializeField] private float _rotationSpeed = 2;

        [Range(1, 100)]
        [SerializeField] private int _hitPoints = 50;

        [Range(1, 10)]
        [SerializeField] private int _armour = 2;

        public int Level => _level;
        public int HitPoints => _hitPoints;
        public int Armour => _armour;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
    }

}
