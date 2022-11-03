using UnityEngine;

namespace Core.PlayerCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [Range(0, .3f)][SerializeField] private float movementSmoothing;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask whatIsGround;
        private Vector2 myVelocity;
        private Rigidbody2D myRigidbody;
        private bool grounded;
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
        private void Update()
        {
            movement = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
            }
        }
        private void FixedUpdate()
        {
            CheckGround();
            if (movement != 0)
            {
                FlipDirection();
                MoveHorizontally(movement * Time.fixedDeltaTime * movementSpeed);
            }
            if (jump)
            {
                Jump(jumpForce);
                jump = false;
            }
        }
        #endregion

        private void CheckGround(){
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -Vector2.up, 0.1f, whatIsGround);
            grounded = hit.collider != null;
        }

        private void FlipDirection()
        {
            Vector3 scale = transform.localScale;
            scale.x = movement > 0 ? 1f : -1f;
            transform.localScale = scale;
        }

        private void MoveHorizontally(float movement)
        {
            Vector2 targetVelocity = new(movement * 100f, myRigidbody.velocity.y);
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
