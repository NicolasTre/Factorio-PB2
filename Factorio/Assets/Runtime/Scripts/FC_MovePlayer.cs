using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    internal class FC_MovePlayer : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Camera _mainCamera;

        private float _zoom;
        private float _zoomMultiplier = 50f;  
        private float _maxZoom = 70f;
        private float _minZoom = 7f;
        private float _velocityCam = 0f;
        private float _smoothTimeZoom = 0.17f;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;

        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _mainCamera.orthographicSize = 10f;
        }

        public void Update()
        {
            _rb.linearVelocity = _moveInput * _moveSpeed;
        }

        public void MoveInGame(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void ZoomScroll(InputAction.CallbackContext context)
        {
           float _scroll = context.ReadValue<float>();
            _zoom -= _scroll * _zoomMultiplier;
            _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
            _mainCamera.orthographicSize = Mathf.SmoothDamp(_mainCamera.orthographicSize, _zoom, ref _velocityCam, _smoothTimeZoom);
        }
    }
}

