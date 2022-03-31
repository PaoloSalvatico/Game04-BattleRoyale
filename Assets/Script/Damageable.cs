using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleRoyale.Interfaces;
using Mirror;
using TMPro;

public class Damageable : NetworkBehaviour, IDamageable
{
    [SerializeField]
    [SyncVar(hook = nameof(HealthChanged))]
    private int _health = 10;

    public TextMeshProUGUI label;

    public override void OnStartClient()
    {
        base.OnStartClient();
        label.text = _health.ToString();
    }

    private void HealthChanged(int _, int newValue)
    {
        label.text = newValue.ToString();
    }

    public int Health => _health;

    public void Damage(int damageAmount)
    {
        Debug.Log("Damage: " + damageAmount);
        if (!isServer) return;
        _health -= damageAmount;
        if(_health <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
