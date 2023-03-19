using Core.Enum;
using Core.Tools;
using Player.PlayerAnimation;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;

        [SerializeField] private DirectionalCameraPair _cameras;

        private Rigidbody2D _rigidbody;
        private Transform _transform;

        private bool _isJumping;

        private Vector2 _movement;
        private AnimationType _currentAnimationType;
    
        // Start is called before the first frame update
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Walk, _movement.magnitude > 0);
            _animator.PlayAnimation(AnimationType.Jump, _isJumping);
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
            _movement.x = direction;
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

        public void StartAttack()
        {
            if (!_animator.PlayAnimation(AnimationType.Attack, true))
            {
                return;
            }

            _animator.ActionRequested += Attack;
            _animator.AnimationEnded += EndAttack;
        }
        
        private void Attack()
        {
            Debug.Log("Attack");
        }
        
        
        public void EndAttack()
        {
            _animator.ActionRequested -= Attack;
            _animator.AnimationEnded -= EndAttack;
            _animator.PlayAnimation(AnimationType.Attack, false);
        }

        private void SetDirection(float direction)
        {
            if ((_direction == Direction.Right && direction < 0) || (_direction == Direction.Left && direction > 0))
            {
                Flip();    
            }
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = cameraPair.Key == _direction;
        }

        private void ResetJump()
        {
            _isJumping = false;
        }
    }
}
