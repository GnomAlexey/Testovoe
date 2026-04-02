using UnityEngine;
using UnityEngine.UI;


public class SelectableObject : MonoBehaviour
{
    private const string BaseColor = "_BaseColor";
    private GameObject Shadow;
    private Material Mat;

    [SerializeField]
    private Material ShadowMat;

    [SerializeField]
    private Material SelectIndicatorMat;

    [SerializeField]
    private GameObject Model;

    public GameObject SelectIndicator;
    public Toggle Toggle;
    private void Start()
    {
        CreateShadow();
        CreateSelectIndicator();
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

    public void Select()
    {
        SelectIndicator.SetActive(true);
        Toggle.isOn = true;

    }

    public void UnSelect()
    {
        SelectIndicator.SetActive(false);
        Toggle.isOn = false;
    }

    public void ChangeTransparency(float alpha)
    {
        Mat = Model.GetComponent<Renderer>().material;
        Color color = Mat.GetColor(BaseColor);
        color.a = alpha;
        Mat.SetColor(BaseColor, color);
    }

    public void HideObj(bool ModelHide)
    {
        gameObject.SetActive(ModelHide);

    }



}
