using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleRoyale.Interfaces;
using Mirror;
using TMPro;

namespace BattleRoyale
{
    public class Damageable : NetworkBehaviour, Interfaces.IDamageable
    {
        [SerializeField]
        protected int _health = 10;
        public int Health => _health;
        public TextMeshPro label;

        public override void OnStartClient()
        {
            base.OnStartClient();
            UpdateUI();
        }

        private void UpdateUI()
        {
            label.text = _health.ToString();
        }

        public void Damage(int damageAmount)
        {
            if (!isServer) return;
            _health -= damageAmount;
            if(_health <= 0)
            {
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}

