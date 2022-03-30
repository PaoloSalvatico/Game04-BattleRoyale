using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using BattleRoyale.Interfaces;

namespace BattleRoyale
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BulletController : NetworkBehaviour
    {
        public int damage;
        public float force;
        public GameObject explosionVfx;

        private void Awake()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * force;
            GetComponent<Collider>().isTrigger = false;
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(explosionVfx, transform.position, transform.rotation);
            if(isServer)
            {
                NetworkServer.Destroy(gameObject);
                var damageable = collision.gameObject.GetComponent<Interfaces.IDamageable>();
                if(damageable != null)
                {
                    damageable.Damage(damage);
                }
            }
        }
    }
}

