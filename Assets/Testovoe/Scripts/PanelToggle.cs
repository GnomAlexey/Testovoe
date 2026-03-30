using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject panel;

    public Transform transform;
    public Toggle toggle;
    public void SetPanel()
    {
        panel.SetActive(toggle.isOn);
        if (toggle.isOn)
        {

            gameObject.transform.position = transform.position + new Vector3(320f, 0, 0);
        }
        else
        {
            gameObject.transform.position = transform.position + new Vector3 (-320f,0,0);
        }
    }
}
