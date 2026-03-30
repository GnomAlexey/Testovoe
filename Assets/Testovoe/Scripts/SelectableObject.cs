using UnityEngine;
using UnityEngine.UI;


public class SelectableObject : MonoBehaviour
{
    public GameObject SelectIndicator;
    public GameObject Model;
    public GameObject Shadow;
    public Toggle toggle;

    private Material mat;
    public Material ShadowMat;
    public Material SelectIndicatorMat;


    public void Select()
    {
        SelectIndicator.SetActive(true);
        toggle.isOn = true;

    }
    public void UnSelect()
    {
        SelectIndicator.SetActive(false);
        toggle.isOn = false;
    }

    public void ChangeTransparency()
    {
        mat = Model.GetComponent<Renderer>().material;
        Color color = mat.GetColor("_BaseColor");
        color.a = 0.5f;
        mat.SetColor("_BaseColor", color);
    }

    public void HideObj(bool ModelHide)
    {
        gameObject.SetActive(ModelHide);

    }

    private void CreateShadow()
    {
        Shadow = Instantiate(Model, transform);

        Shadow.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        Shadow.GetComponent<Renderer>().material = ShadowMat;
        Shadow.name = $"{name}Shadow";
        Shadow.GetComponent<Collider>().enabled = false;
    }

    private void CreateSelectIndicator()
    {
        SelectIndicator = Instantiate(Model, transform);
        SelectIndicator.GetComponent<Renderer>().material = SelectIndicatorMat;
        SelectIndicator.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        SelectIndicator.name = $"{name}Fade";
        SelectIndicator.GetComponent<Collider>().enabled = false;
        SelectIndicator.SetActive(false);
    }

    private void Start()
    {
        CreateShadow();
        CreateSelectIndicator();
    }
}
