using System;
using UnityEngine;

public class NewInputHandler : MonoBehaviour
{
    private CameraControl input;

    public event Action OnLeftClick;
    public event Action OnRightClick;
    public event Action OnRightClickStarted;
    public event Action OnRightClickCanceled;
    public event Action<Vector2> OnMoveCanceled;
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<float> OnScroll;


    void Awake()
    {
        input = new CameraControl();
    }
    void OnEnable()
    {
        input.Enable();

        input.Camera.LeftClick.performed += context => OnLeftClick?.Invoke();

        input.Camera.RightClick.performed += context => OnRightClick?.Invoke();
        input.Camera.RightClick.started += context => OnRightClickStarted?.Invoke();
        input.Camera.RightClick.canceled += context => OnRightClickCanceled?.Invoke();

        input.Camera.Move.performed += context => OnMove?.Invoke(context.ReadValue<Vector2>());
        input.Camera.Move.canceled += context => OnMoveCanceled?.Invoke(context.ReadValue<Vector2>());

        input.Camera.Look.performed += context => OnLook?.Invoke(context.ReadValue<Vector2>());

        input.Camera.Scroll.performed += context => OnScroll?.Invoke(context.ReadValue<Vector2>().y);

    }

    void OnDisable()
    {
        input.Disable();
    }
}

