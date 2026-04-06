using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{

    [SerializeField] private SelectManagment SelectManagment;
    [SerializeField] private GameObject Camera;
    [SerializeField] private Toggle CamFlyModeToggle;
    [SerializeField] private Toggle CamRotateModeToggle;
    [SerializeField] private float MaxAngle;
    [SerializeField] private float MinAngle;
    [SerializeField] private bool isSelected = false;
    [SerializeField] private float zoomSpeed = 1f;

    public NewInputHandler inputHandler;
    public Vector3 nextpos;
    public bool isFly = false;

    private float distance = 1.0f;
    
    private float maxheight = 10f;

    private Vector3 CamDistance = new Vector3(0f, 1f, -5f);
    private Vector3 CamStartPos;
    private Quaternion CamStartRotation;

    private float ZoomDis = 0f;
    private float ZoomMaxDis = 1f;
    private Vector3 CurrentPosition;
    private bool isRotating = false;
    private Vector2 RightClickInput;
    private float Axis;


    void Start()
    {
        CamStartPos = Camera.transform.position;
        CamStartRotation = Camera.transform.rotation;
        nextpos = Camera.transform.position;
        inputHandler.OnRightClickStarted += StartRotate;
        inputHandler.OnRightClickCanceled += StopRotate;
        inputHandler.OnLook += SetLookInput;
        inputHandler.OnScroll += Zoom;
    }
    private void Update()
    {

        Camera.transform.position = Vector3.Lerp(Camera.transform.position, nextpos, Time.deltaTime * 10f);
        if (isRotating && isSelected)
        {
            RotateAround();
            RightClickInput = Vector2.zero;
        }
    }

    public void MoveToObject(Vector3 pos)
    {
        SelectManagment.EnableToSelect = false;
        CamFlyModeToggle.isOn = false;
        isSelected = false;
        CurrentPosition = pos;
        nextpos = pos + CamDistance;
        Camera.transform.rotation = CamStartRotation;
        ZoomDis = 0;
        StartCoroutine(DelayedCall());
    }

    IEnumerator DelayedCall()
    {
        yield return new WaitForSeconds(0.5f);
        isSelected = true;
    }

    public void RotateAround()
    {
        Camera.transform.RotateAround(CurrentPosition, Camera.transform.right, -RightClickInput.y / 2);
        Camera.transform.RotateAround(CurrentPosition, Vector3.up, RightClickInput.x / 2);
        nextpos = Camera.transform.position;
    }

    public void CamToStartPos()
    {
        isSelected = false;
        SelectManagment.EnableToSelect = true;
        ZoomDis = 0;
        nextpos = CamStartPos;
        Camera.transform.rotation = CamStartRotation;
    }

    public void Zoom(float Axis)
    {
        float scroll = Axis;

        if (scroll > 0 && ZoomDis <= ZoomMaxDis)
        {
            ZoomDis += 0.7f;
            nextpos += Camera.transform.forward * zoomSpeed;
        }
        if (scroll < 0 && ZoomDis >= -ZoomMaxDis)
        {
            nextpos -= Camera.transform.forward * zoomSpeed;
            ZoomDis -= 0.7f;
        }

    }

    public void CamFlyMode()
    {
        SelectManagment.EnableToSelect = !CamFlyModeToggle.isOn;
        isFly = CamFlyModeToggle.isOn;
        isSelected = false;
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
}

