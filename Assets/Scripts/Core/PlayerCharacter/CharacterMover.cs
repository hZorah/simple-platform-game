using UnityEngine;

namespace Core.PlayerCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [Range(0, .3f)] [SerializeField] private float movementSmoothing;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundCheck;
        private Vector2 myVelocity;
        private Rigidbody2D myRigidbody;
        [SerializeField] private bool grounded;
        private bool jump;
        private float movement;

        #region Unity Events
        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            grounded = false;
            myVelocity = Vector2.zero;
        }
        private void Update() {
            movement = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                jump = true;
            }
        }
        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -Vector2.up, 0.1f);
            grounded = hit.collider != null;

            if (movement != 0){
                Vector3 scale = transform.localScale;
                if (movement > 0){
                    scale.x = 1f;
                }
                else {
                    scale.x = -1f;
                }
                transform.localScale = scale;
                MoveHorizontal(movement * Time.fixedDeltaTime * movementSpeed);
            }
            if (jump){
                Jump (jumpForce);
                jump = false;
            }
        }
        #endregion

        private void MoveHorizontal(float movement)
        {
            Vector2 targetVelocity = new Vector2(movement*100f, myRigidbody.velocity.y);
            targetVelocity = Vector2.SmoothDamp(myRigidbody.velocity, targetVelocity, ref myVelocity, movementSmoothing);
            myRigidbody.velocity = targetVelocity;
        }

        private void Jump(float jumpForce)
        {
            if (!grounded) return;
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
