using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private SelectManagment SelectManagment;

    [SerializeField] private Camera Camera;

    [SerializeField] private CameraController CameraController;

    public SelectableObject Cursor;
    public NewInputHandler inputHandler;

    private Vector2 moveInput;
    private bool isRotating = false;
    private Vector2 RightClickInput;
    private void Start()
    {
        CreateBinds();
    }

    private void CreateBinds()
    {
        inputHandler.OnLeftClick += InputLeftClick;
        inputHandler.OnRightClick += InputRightClick;
        inputHandler.OnRightClickStarted += StartRotate;
        inputHandler.OnRightClickCanceled += StopRotate;
        inputHandler.OnLook += SetLookInput;
        inputHandler.OnMove += SetMove;
        inputHandler.OnMoveCanceled += StopMove;
    }

    private void Update()
    {
        if (CameraController.isFly)
        {
            if (isRotating)
            {
                FlyRotatement(RightClickInput);
                RightClickInput = Vector2.zero;
            }
            FlyMovement();
        }
    }

    private bool isRaycastHit()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent<SelectableCollider>(out var cursor))
            {
                Cursor = cursor.SelectableObject;
                return true;
            }
        }
        return false;
    }

    private void InputLeftClick()
    {
        if (isRaycastHit())
        {
            CameraController.MoveToObject(Cursor.transform.position);

            SelectManagment.EnableToSelect = false;
            SelectManagment.UnselectAll();


            if (Cursor)
            {
                Cursor.Select();
            }

        }

    }

    private void InputRightClick()
    {
        if (isRaycastHit())
        {
            if (SelectManagment.ListOfSelected.Contains(Cursor))
            {
                Cursor.UnSelect();
            }

            else if (SelectManagment.ListOfSelected.Contains(Cursor) == false)
            {
                Cursor.Select();

            }
            if (SelectManagment.ListOfSelected.Count == SelectManagment.AllObj.Count)
            {
                SelectManagment.HideSelect();
            }
        }
    }
    private void FlyMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Camera.transform.Translate(move * speed * Time.deltaTime);
        CameraController.nextpos = Camera.transform.position;
    }
    private void FlyRotatement(Vector2 input)
    {
        Camera.transform.Rotate(Vector3.up, input.x / 50 * rotationSpeed, Space.World);
        Camera.transform.Rotate(Vector3.right, -input.y / 50 * rotationSpeed, Space.Self);
    }

    private void StartRotate()
    {
        isRotating = true;
    }

    private void StopRotate()
    {
        isRotating = false;
    }

    private void SetLookInput(Vector2 input)
    {
        RightClickInput = input;
    }
    private void SetMove(Vector2 input)
    {
        moveInput = input;

    }
    private void StopMove(Vector2 input)
    {
        moveInput = Vector2.zero;
    }


}
