using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleRoyale.Data;
using Mirror;
using BattleRoyale.Interfaces;

namespace BattleRoyale
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour, IDamageable
    {
        private CharacterController _controller;
        [SerializeField] private PlayerStatsObj stats;
        [SerializeField] private WeaponstatsObj _weaponStats;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private NetworkAnimator _networkAnimator;

        private AnimationEventsDispatcher _eventDispatcher;
        private bool isShooting = false;
        private int _hitPoints;

        public int Health => _hitPoints;

        private void Awake()
        {
            _hitPoints = stats.HitPoints;
            _controller = GetComponent<CharacterController>();
            _eventDispatcher = GetComponentInChildren<AnimationEventsDispatcher>();
        }

        // Funzione eseguita solo sul server
        [ServerCallback]
        private void Start()
        {
            Debug.Log("Sono il server");
            RpcTestClient();
        }

        // Funzione eseguita solo dai client
        [ClientRpc]
        private void RpcTestClient()
        {
            Debug.Log("Sono il client");
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            var prefab = _weaponStats.Bullet;
            var ni = prefab.GetComponent<NetworkIdentity>();
            if(ni != null && !NetworkClient.prefabs.ContainsKey(ni.assetId))
            {
                NetworkClient.RegisterPrefab(prefab);
            }
        }

        private void OnEnable()
        {
            _eventDispatcher.shootEventStart += OnShootStarted;
            _eventDispatcher.shootEventEnd += OnShootEnded;
        }
        private void OnDisable()
        {

        }

        private void OnShootStarted()
        {
            if (!isLocalPlayer) return;
            CmdSpawnBullet();
        }

        private void OnShootEnded()
        {
            isShooting = false;
        }


        private void Update()
        {
            if (!isLocalPlayer) return;

            //Fase di MOvimento
            var m = stats.MoveSpeed;
            if (Input.GetKey(KeyCode.Space)) m *= 2;

            var move = Input.GetAxis("Vertical") * stats.MoveSpeed * transform.forward;
            _controller.SimpleMove(move);

            var rotation = Input.GetAxis("Horizontal") * stats.RotationSpeed * Vector3.up;
            transform.Rotate(rotation);

            _animator.SetFloat("Speed", _controller.velocity.magnitude);

            // Fase di Sparo
            if(Input.GetButtonDown("Fire1") && !isShooting)
            {
                isShooting = true;
                _networkAnimator.SetTrigger(_weaponStats.AnimatorParameter);
            }
        }

        [Command]
        private void CmdSpawnBullet()
        {
            //_animator.SetTrigger(_weaponStats.AnimatorParameter);
            var go = Instantiate(_weaponStats.Bullet, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(go);
        }

        public void Damage(int damageAmount)
        {
            damageAmount -= stats.Armor;
            damageAmount = Mathf.Clamp(damageAmount, 0, 10000);

        }
    }
}

