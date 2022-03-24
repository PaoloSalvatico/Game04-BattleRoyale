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

        public int Level => _level;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;

    }
}

