using UnityEngine;

namespace Core.PlayerCharacter
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private Rigidbody2D _rigidbody;
        #endregion
        #region Members
        private Animator m_animator;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        #endregion

        #region Unity Events
        private void Awake() {
            m_animator = GetComponent<Animator>();
        }
        private void FixedUpdate() {

            m_animator.SetFloat(Horizontal, Mathf.Abs(_rigidbody.velocity.x));
            m_animator.SetFloat(Vertical, _rigidbody.velocity.y);
        }
        #endregion
    }
}
