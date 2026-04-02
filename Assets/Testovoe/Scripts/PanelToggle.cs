using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    private Toggle Toggle;
    private void Start()
    {
        Toggle = GetComponent<Toggle>();
    }

    public void SetPanel()
    {
        Panel.SetActive(Toggle.isOn);
        if (Toggle.isOn)
        {

            gameObject.transform.position = transform.position + new Vector3(320f, 0, 0);
        }
        else
        {
            gameObject.transform.position = transform.position + new Vector3(-320f, 0, 0);
        }
    }
}
