using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Input;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Movement;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Combat;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(NinjaInputReader))]
    [RequireComponent(typeof(NinjaHealth))]
    [RequireComponent(typeof(NinjaAnimator))]
    public sealed class NinjaController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 8f;

        [Header("Action Timing")]
        [SerializeField] private float attackDuration = 0.35f;
        [SerializeField] private float hurtDuration = 0.3f;

        [Header("References")]
        [SerializeField] private GroundChecker groundChecker;

        private Rigidbody2D _rigidbody;
        private NinjaInputReader _input;
        private NinjaHealth _health;
        private NinjaAnimator _animator;

        private NinjaContext _context;
        private NinjaStateMachine _stateMachine;

        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public float AttackDuration => attackDuration;
        public float HurtDuration => hurtDuration;

        public bool IsDead => _health != null && _health.IsDead;
        public float MoveInput => _input != null ? _input.MoveAxis : 0f;
        public bool HasMoveInput => Mathf.Abs(MoveInput) > 0.01f;
        public bool IsGrounded => groundChecker != null && groundChecker.IsGrounded();

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = GetComponent<NinjaInputReader>();
            _health = GetComponent<NinjaHealth>();
            _animator = GetComponent<NinjaAnimator>();

            _context = new NinjaContext(
                this,
                _animator,
                _input,
                groundChecker,
                _health,
                _rigidbody,
                transform);

            _stateMachine = new NinjaStateMachine();

            RegisterStates();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.Damaged += HandleDamaged;
                _health.Died += HandleDied;
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.Damaged -= HandleDamaged;
                _health.Died -= HandleDied;
            }
        }

        private void Start()
        {
            ChangeState(NinjaStateId.Idle);
        }

        private void Update()
        {
            _stateMachine.HandleInput();
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void RegisterStates()
        {
            _stateMachine.Register(new NinjaIdleState(_context));
            _stateMachine.Register(new NinjaRunState(_context));
            _stateMachine.Register(new NinjaJumpState(_context));
            _stateMachine.Register(new NinjaAttackState(_context));
            _stateMachine.Register(new NinjaHurtState(_context));
            _stateMachine.Register(new NinjaDieState(_context));
        }

        private void HandleDamaged(int currentHealth)
        {
            if (currentHealth > 0)
            {
                ChangeState(NinjaStateId.Hurt);
            }
        }

        private void HandleDied()
        {
            ChangeState(NinjaStateId.Die);
        }

        public void ChangeState(NinjaStateId stateId)
        {
            _stateMachine.ChangeState(stateId);
        }

        public void MoveHorizontal(float direction)
        {
            _rigidbody.velocity = new Vector2(direction * moveSpeed, _rigidbody.velocity.y);

            if (Mathf.Abs(direction) > 0.01f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
                transform.localScale = scale;
            }
        }

        public void StopHorizontal()
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        }

        public void Jump()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        public void ConsumeJumpInput()
        {
            _input.ConsumeJump();
        }

        public void ConsumeAttackInput()
        {
            _input.ConsumeAttack();
        }

        public bool IsJumpPressed()
        {
            return _input != null && _input.JumpPressed;
        }

        public bool IsAttackPressed()
        {
            return _input != null && _input.AttackPressed;
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }
    }
}