using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FC_InputPlayer : MonoBehaviour
{

    public bool canRotate { get; private set; } = false;

    public static FC_InputPlayer instance;
   

    [HideInInspector] public UnityEvent OnRotateMachine;
    [HideInInspector] public UnityEvent OnDestroyMachine;

    private void Awake()
    {
        instance = this;
    }

    public void RotateMachine(InputAction.CallbackContext context)
    {
        if (context.performed && canRotate)
        {
            OnRotateMachine?.Invoke();
        }
    }

    public void DestroyMachine(InputAction.CallbackContext context)
    {
        if (context.performed && canRotate)
        {
            OnRotateMachine?.Invoke();
        }
    }

    public void SetCanRotate(bool value)
    {
        canRotate = value ;
    }

    public void Destroy()
    {
       
    }
}
