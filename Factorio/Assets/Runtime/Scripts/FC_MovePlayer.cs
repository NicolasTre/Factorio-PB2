using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    internal class FC_MovePlayer : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        Camera _camera;


        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _camera = GetComponentInChildren<Camera>();
        }

        public void Update()
        {
            _rb.linearVelocity = _moveInput * _moveSpeed;
        }

        public void Move(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }
}

