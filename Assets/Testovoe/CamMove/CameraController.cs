using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{

    [SerializeField]
    private SelectManagment SelectManagment;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private Toggle CamFlyModeToggle;
    [SerializeField]
    private Toggle CamRotateModeToggle;

    private float distance = 1.0f;
    public Vector3 nextpos;
    private float maxheight = 10f;

    private Vector3 CamDistance = new Vector3(0f, 2f, -5f);
    private Vector3 CamStartPos;
    private Quaternion CamStartRotation;

    private float zoomSpeed = 1f;
    [SerializeField]
    private float ZoomDis = 0f;
    private float ZoomMaxDis = 1f;
    public bool isFly = false;
    [SerializeField]
    private bool isSelected = false;


    void Start()
    {
        CamStartPos = Camera.transform.position;
        CamStartRotation = Camera.transform.rotation;
        nextpos = Camera.transform.position;
    }
    private void Update()
    {
        Zoom();
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, nextpos, Time.deltaTime * 10f);
        if (Input.GetMouseButton(1) && isSelected)
        {
            Rotate();
        }
    }

    public void MoveUp()
    {
        if (Camera.transform.position.y <= maxheight)
        {

            nextpos = Camera.transform.position + new Vector3(0f, 1f * distance, 0f);
        }
    }

    public void MoveDown()
    {
        if (Camera.transform.position.y > 1)
        {
            nextpos = Camera.transform.position + new Vector3(0f, -1f * distance, 0f);
        }

    }

    public void MoveToObject(Vector3 pos)
    {
        SelectManagment.EnableToSelect = false;
        CamFlyModeToggle.isOn = false;
        isSelected = false;
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

    public void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * 9f;
        Camera.transform.RotateAround(SelectManagment.ListOfSelected[0].transform.position, Vector3.up, mouseX);
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

    public void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

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
}

