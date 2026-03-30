using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor;

public class TextController : MonoBehaviour
{

    public SelectManagment SelectManagment;
    public List<SelectableObject> objects;

    public Transform content;
    public TMP_Text textPrefab;

    public List<Toggle> togglesHide = null;
    public List<Toggle> toggles = null;

    public Toggle toggPref;
    public Toggle toggPrefH;

    public Image imagePref;
    public GameObject images;



    void Start()
    {
        objects =SelectManagment.AllObj;


        CreateList();
        CreateTogglesH();
        CreateToggles();


    }
    
    void CreateList()
    {
        foreach (SelectableObject obj in objects)
        {
            Toggle T = Instantiate(toggPref, transform);
            TMP_Text newText = Instantiate(textPrefab, content);
            newText.text = obj.name;
            Toggle TH = Instantiate(toggPrefH, transform);
            togglesHide.Add(TH);
            toggles.Add(T);
            obj.toggle = T;
            Image image = Instantiate(imagePref, images.transform);

        }
    }

    void CreateToggles()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            int index = i;

            toggles[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    if (SelectManagment.CheckBox)
                    {
                        objects[index].Select();

                    }
                    SelectManagment.ListOfSelected.Add(objects[index]);
                }
                else { objects[index].UnSelect(); SelectManagment.ListOfSelected.Remove(objects[index]); }
            });
        }
    }
    void CreateTogglesH()
    {
        for (int i = 0; i < togglesHide.Count; i++)
        {
            int index = i;

            togglesHide[i].onValueChanged.AddListener((isOn) =>
            {
                objects[index].HideObj(isOn);
            });
        }
    }
    
}

   

