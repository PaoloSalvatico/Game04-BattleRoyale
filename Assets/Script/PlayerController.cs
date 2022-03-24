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
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            var move = Input.GetAxis("Vertical") * stats.MoveSpeed * transform.forward;
            _controller.SimpleMove(move);

            var rotation = Input.GetAxis("Horizontal") * stats.RotationSpeed * Vector3.up;
            transform.Rotate(rotation);

            _animator.SetFloat("Speed", _controller.velocity.magnitude);
        }
    }
}

