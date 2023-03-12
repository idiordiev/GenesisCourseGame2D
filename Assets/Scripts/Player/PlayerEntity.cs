using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;

        private Rigidbody2D _rigidbody;
        private Transform _transform;

        private bool _isJumping;
    
        // Start is called before the first frame update
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground") && _isJumping)
            {
                ResetJump();
            }
        }

        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (_isJumping)
                return;

            _isJumping = true;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) || (!_faceRight && direction > 0))
            {
                Flip();    
            }
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }

        private void ResetJump()
        {
            _isJumping = false;
        }
    }
}
