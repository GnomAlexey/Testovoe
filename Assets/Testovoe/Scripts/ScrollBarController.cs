using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    [SerializeField]
    private GameObject Content;
    [SerializeField]
    private RectTransform RectTransform;
    private int StepsDown;

    void Start()
    {
        StepsDown = Content.GetComponentsInChildren<Image>().Length / 5 - 8;
    }

    public void ScrollUp()
    {
        if (RectTransform.localPosition.y + 160f >= 0)
        {
            RectTransform.localPosition += new Vector3(0, -40f, 0);
        }
    }
    public void ScrollDown()
    {
        if (RectTransform.localPosition.y <= 40f * StepsDown)
        {
            RectTransform.localPosition += new Vector3(0, +40f, 0);
        }
    }
}
