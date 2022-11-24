using UnityEngine;

namespace Core.PlayerCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMover : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private CharacterMovementParameters _parameters;
        [SerializeField] private Transform _groundCheck;
        #endregion

        #region Members
        private Vector2 m_velocity;
        private Rigidbody2D m_rigidbody;
        private bool m_jump;
        private float m_movement;
        #endregion

        #region Unity Events
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            m_velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }
        #endregion

        private void MoveCharacter()
        {
            if (m_movement != 0)
            {
                FlipDirection();
            }
            MoveHorizontally(m_movement * Time.fixedDeltaTime * _parameters.MovementSpeed);

            if (!m_jump) return;
            PerformJump();
            m_jump = false;
        }

        private bool CheckGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, -Vector2.up, 0.1f, _parameters.WhatIsGround);
            return hit.collider != null;
        }

        private void FlipDirection()
        {
            Vector3 scale = transform.localScale;
            scale.x = m_movement > 0 ? 1f : -1f;
            transform.localScale = scale;
        }

        private void MoveHorizontally(float movement)
        {
            Vector2 velocity = m_rigidbody.velocity;
            Vector2 targetVelocity = new(movement * 100f, velocity.y);
            targetVelocity = Vector2.SmoothDamp(velocity, targetVelocity, ref m_velocity, _parameters.MovementSmoothing);
            m_rigidbody.velocity = targetVelocity;
        }

        private void PerformJump()
        {
            if (CheckGround())
                m_rigidbody.AddForce(new Vector2(0f, _parameters.JumpForce), ForceMode2D.Impulse);
        }

        /// <summary>
        /// Makes character jump
        /// </summary>
        public void JumpCommand()
        {
            m_jump = true;
        }

        /// <summary>
        /// Moves character sideways
        /// </summary>
        /// <param name="movement">The direction of the movement</param>
        public void MoveCommand(float movement)
        {
            this.m_movement = movement;
        }
    }
}
