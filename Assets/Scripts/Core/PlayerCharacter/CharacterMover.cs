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
        private bool jump;
        [SerializeField] private float movement;

        #region Unity Events
        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            myVelocity = Vector2.zero;
        }
        // private void Update()
        // {
        //     movement = Input.GetAxis("Horizontal");
        //     if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.Space))
        //     {
        //         jump = true;
        //     }
        // }
        private void FixedUpdate()
        {
            if (movement != 0)
            {
                FlipDirection();
            }
            MoveHorizontally(movement * Time.fixedDeltaTime * movementSpeed);
            
            if (jump)
            {
                PerformJump();
                jump = false;
            }
        }
        #endregion

        private bool CheckGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -Vector2.up, 0.1f, whatIsGround);
            return  hit.collider != null;
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

        private void PerformJump()
        {
            if (CheckGround())
                myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        public void JumpCommand()
        {
            jump = true;
        }

        public void MoveCommand(float movement)
        {
            this.movement = movement;
        }
    }
}
