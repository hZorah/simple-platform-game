using UnityEngine;

namespace Core.PlayerCharacter
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D myRigidbody;
        private Animator myAnimator;
        #region Unity Events
        private void Awake() {
            myAnimator = GetComponent<Animator>();
        }
        private void FixedUpdate() {

            myAnimator.SetFloat("Horizontal", Mathf.Abs(myRigidbody.velocity.x));
            myAnimator.SetFloat("Vertical", myRigidbody.velocity.y);
        }
        #endregion
    }
}
