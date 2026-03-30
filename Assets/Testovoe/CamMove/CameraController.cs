using UnityEngine;
public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public SelectManagment SM;
    public GameObject Camera;


    public float distance = 1.0f;
    public Vector3 nextpos;
    public float maxheight =10f;

    public Vector3 CamDistance = new Vector3 (0f, 2f, -5f);
    public Vector3 CamStartPos;
    public Quaternion CamStartRotation;

    public float zoomSpeed = 5f;
    public float ZoomDis = 0f;
    private float ZoomMaxDis = 3f;

    void Start()
    {
        CamStartPos = Camera.transform.position;
        CamStartRotation = Camera.transform.rotation;
        nextpos = Camera.transform.position;
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
        nextpos = pos + CamDistance;
        Camera.transform.rotation = CamStartRotation;
        ZoomDis = 0;
    }

    public void Rotate()
    {
        if (!SM.EnableToSelect)
        {
         
            Camera.transform.RotateAround(SM.ObjPos, Vector3.up, 15);
            nextpos = Camera.transform.position;
        }
    }

    public void CamToStartPos()
    {
        ZoomDis = 0;
        nextpos = CamStartPos;
        Camera.transform.rotation = CamStartRotation;
        SM.isSelected = false;
        SM.EnableToSelect = true;
    }

    public void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll > 0 && ZoomDis<= ZoomMaxDis)
        {
            ZoomDis += 0.5f;
            nextpos += Camera.transform.forward * zoomSpeed;
        }
        if (scroll < 0 && ZoomDis >= -ZoomMaxDis)
        {
            nextpos -= Camera.transform.forward * zoomSpeed;
            ZoomDis -=0.5f;
        }

       
        

    }

    private void Update()
    {
        Zoom();
        Camera.transform.position = Vector3.Lerp(Camera.transform.position,nextpos, Time.deltaTime*10f);
    }

   
}

