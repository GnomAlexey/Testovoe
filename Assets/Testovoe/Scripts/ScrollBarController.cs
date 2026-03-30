using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    public GameObject Content;
    public RectTransform RectTransform;
    public int G;

    void Start()
    {
        G = Content.GetComponentsInChildren<Image>().Length / 5-1;
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
        if (RectTransform.localPosition.y <= 40f *G)
        {
            RectTransform.localPosition += new Vector3(0, +40f, 0);
        }
    }
}
