using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Controller
{
    internal class FC_MovePlayer : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private CinemachineVirtualCamera _mainCinemachineCamera;

        [SerializeField] [Range(7f,70f)] private float _zoom = 10f;
        private float _zoomMultiplier = 2f;  
        private float _velocityCam = 0f;
        private float _minZoom = 7f;
        private float _maxZoom = 70f;
        private float _smoothTimeZoom = 0.17f;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;

        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _mainCinemachineCamera.m_Lens.OrthographicSize = 10f;
        }

        public void Update()
        {
            _rb.linearVelocity = _moveInput * _moveSpeed;
            _mainCinemachineCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(_mainCinemachineCamera.m_Lens.OrthographicSize, _zoom, ref _velocityCam, _smoothTimeZoom);
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
        }
    }
}