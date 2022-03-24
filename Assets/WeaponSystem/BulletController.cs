using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace BattleRoyale
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BulletController : NetworkBehaviour
    {
        public float damage;
        public float force;

        private void Awake()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * force;
            GetComponent<Collider>().isTrigger = false;
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            //Provare Danno

            NetworkServer.Destroy(gameObject);
        }
    }
}

