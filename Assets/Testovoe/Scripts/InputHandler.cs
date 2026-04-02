using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private SelectManagment SelectManagment;

    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private CameraController CameraController;

    public SelectableObject Cursor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputLeftClick();
        }
        if (Input.GetMouseButtonDown(1) && SelectManagment.EnableToSelect)
        {
            InputRightClick();
        }
        if (CameraController.isFly)
        {
            HandleFly();
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
            if (SelectManagment.ListOfSelected.Contains(Cursor) && SelectManagment.CheckBox)
            {
                Cursor.UnSelect();
            }

            else if (SelectManagment.ListOfSelected.Contains(Cursor) == false)
            {
                if (SelectManagment.CheckBox)
                {
                    Cursor.Select();
                }

            }
        }
    }
    void HandleFly()
    {
        float CamSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 2 : speed;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Camera.transform.Translate(move * CamSpeed * Time.deltaTime);
        CameraController.nextpos = Camera.transform.position;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * 3f;
            float mouseY = Input.GetAxis("Mouse Y") * 3f;

            Camera.transform.Rotate(Vector3.up, mouseX, Space.World);
            Camera.transform.Rotate(Vector3.right, -mouseY, Space.Self);
        }
    }
}
