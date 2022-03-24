using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleRoyale.Data;
using Mirror;

namespace BattleRoyale
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        private CharacterController _controller;
        [SerializeField] private PlayerStatsObj stats;
        [SerializeField] private WeaponstatsObj _weaponStats;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private NetworkAnimator _networkAnimator;

        private AnimationEventsDispatcher _eventDispatcher;
        private bool isShooting = false;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _eventDispatcher = GetComponentInChildren<AnimationEventsDispatcher>();
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
            var go = Instantiate(_weaponStats.Bullet, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(go);
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
                CmdSpawnBullet();
            }
        }

        [Command]
        private void CmdSpawnBullet()
        {
            _networkAnimator.SetTrigger(_weaponStats.AnimatorParameter);
            //_animator.SetTrigger(_weaponStats.AnimatorParameter);
            
        }
    }
}

