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

        [SerializeField] private PlayerStatsSO stats;
        [SerializeField] private WeaponsStatsSO weaponStats;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private NetworkAnimator _networkAnimator;

        private AnimationEventDispatcher _eventDispatcher;

        private bool _isShooting;

        private int _hitPoints;

        public int Health => _hitPoints;

        void Awake()
        {
            _hitPoints = stats.HitPoints;

            _controller = GetComponent<CharacterController>();
            _eventDispatcher = GetComponentInChildren<AnimationEventDispatcher>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            var prefab = weaponStats.Bullet;
            var ni = prefab.GetComponent<NetworkIdentity>();
            if(ni != null && !NetworkClient.prefabs.ContainsKey(ni.assetId))
            {
                NetworkClient.RegisterPrefab(weaponStats.Bullet);
            }

            if (isLocalPlayer)
            {
                CameraManager.Instance.SetTarget(transform);
            }
        }

        private void OnEnable()
        {
            _eventDispatcher.ShootStarted += OnShootStarted;    
            _eventDispatcher.ShootEnded += OnShootEnded;
        }

        private void OnDisable()
        {
            _eventDispatcher.ShootStarted -= OnShootStarted;
            _eventDispatcher.ShootEnded -= OnShootEnded;
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            // Fase di movimento

            var m = stats.MoveSpeed;
            if (Input.GetKey(KeyCode.Space)) m *= 2;

            var move = Input.GetAxis("Vertical") * m * transform.forward;
            _controller.SimpleMove(move);

            var rot = Input.GetAxis("Horizontal") * stats.RotationSpeed * transform.up;
            transform.Rotate(rot);

            _animator.SetFloat("Speed", _controller.velocity.magnitude);

            // Fase di Fuoco
            if(Input.GetButtonDown("Fire1") && !_isShooting)
            {
                _isShooting = true;
                _networkAnimator.SetTrigger(weaponStats.AnimatorParameter);
            }
        }

        private void OnShootEnded()
        {
            _isShooting = false;
        }

        private void OnShootStarted()
        {
            if (!isLocalPlayer) return;
            CmdSpawnBullet();
        }

        [Command]
        void CmdSpawnBullet()
        {
            //            _animator.SetTrigger(weaponStats.AnimatorParameter);
            var go = Instantiate(weaponStats.Bullet, _spawnPoint.position, _spawnPoint.rotation);
            NetworkServer.Spawn(go);
        }

        public void Damage(int damageAmount)
        {
            damageAmount -= stats.Armour;
            damageAmount = Mathf.Clamp(damageAmount, 0, 100000);
            _hitPoints -= damageAmount;
            if(_hitPoints <= 0)
            {
                // sei morto
            }
        }

        private void OnDestroy()
        {
            CameraManager.Instance.SetTarget(null);
        }
    }

}
