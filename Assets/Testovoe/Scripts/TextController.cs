using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public List<Toggle> SelectionToggles = null;

    [SerializeField]
    private SelectManagment SelectManagment;
    [SerializeField]
    private Transform Content;
    [SerializeField]
    private TMP_Text TextPrefab;
    [SerializeField]
    private Toggle ToggPref;
    [SerializeField]
    private Toggle ToggPrefH;
    [SerializeField]
    private Image imagePref;
    [SerializeField]
    private GameObject images;
    public List<Toggle> togglesHide = null;

    void Start()
    {
        CreateList();
        CreateTogglesHide();
        CreateSelectionToggles();
    }

    void CreateList()
    {
        foreach (SelectableObject obj in SelectManagment.AllObj)
        {
            Toggle ToggleSelect = Instantiate(ToggPref, transform);
            TMP_Text newText = Instantiate(TextPrefab, transform);
            Toggle ToggleHide = Instantiate(ToggPrefH, transform);
            Image image = Instantiate(imagePref, images.transform);
            newText.text = obj.name;
            togglesHide.Add(ToggleHide);
            SelectionToggles.Add(ToggleSelect);
            obj.Toggle = ToggleSelect;
        }
    }

    void CreateSelectionToggles()
    {
        for (int i = 0; i < SelectionToggles.Count; i++)
        {
            int index = i;

            SelectionToggles[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    if (SelectManagment.CheckBox)
                    {
                        SelectManagment.AllObj[index].Select();
                    }
                    SelectManagment.ListOfSelected.Add(SelectManagment.AllObj[index]);
                }
                else { SelectManagment.AllObj[index].UnSelect(); SelectManagment.ListOfSelected.Remove(SelectManagment.AllObj[index]); }
            });
        }
    }
    void CreateTogglesHide()
    {
        for (int i = 0; i < togglesHide.Count; i++)
        {
            int index = i;

            togglesHide[i].onValueChanged.AddListener((isOn) =>
            {
                SelectManagment.AllObj[index].HideObj(isOn);
            });
        }
    }

}



