using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleRoyale.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Battle Royale/Player Data")]
    public class PlayerStatsObj : ScriptableObject
    {
        [SerializeField] int _level = 1;
        [SerializeField] float _moveSpeed = 2;
        [SerializeField] float _rotationSpeed = 4;

        [Range(0, 100)]
        [SerializeField] int _hitPoints = 50;
        [SerializeField] int _maxHitPoints = 100;
        [Range(1, 10)]
        [SerializeField] int _armor = 50;

        public int Level => _level;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public int HitPoints => _hitPoints;
        public int MaxHitPoints => _maxHitPoints;
        public int Armor => _armor;
    }
}

