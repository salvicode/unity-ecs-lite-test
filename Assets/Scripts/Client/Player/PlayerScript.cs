using UnityEngine;

namespace Client.Player
{
    using System;

    public class PlayerScript : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        private Animator _animator;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }
        private void Awake()
        {
            AssignAnimationIDs();
            _animator = GetComponent<Animator>();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public void StartMoveAnimation()
        {   
            _animator.SetFloat(_animIDSpeed, 3.0f);
            _animator.SetFloat(_animIDMotionSpeed, 2.0f);
        }

        public void StopMoveAnimation()
        {
            _animator.SetFloat(_animIDSpeed, 0.0f);
            _animator.SetFloat(_animIDMotionSpeed, 0.0f);
        }
    }
}