using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEditor;

public class SelectManagment : MonoBehaviour
{


    public Camera Camera;
    public SelectableObject Cursor;


    public Vector3 ObjPos = new Vector3(0,0,0);
    public CameraController Controller;


    public bool isSelected = false;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();
    public List<SelectableObject> AllObj = new List<SelectableObject>();
    public GameObject AllSelectObj;


    private bool ModelHide= false;
    public bool EnableToSelect= true;
    public bool CheckBox = true;


    public TextController textController;


    public GameObject Toggles;
    public List<Toggle> AllToggles;

    void Awake()
    {
        AllToggles = textController.toggles;
        AllObj = new List<SelectableObject>(AllSelectObj.GetComponentsInChildren<SelectableObject>());
        Debug.Log(AllObj);
    }

  

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !isSelected)
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
                Cursor = hit.collider.GetComponent<SelectableCollider>().SelectableObject;
             

                if (Input.GetMouseButtonDown(0))
                {
                    ObjPos = hit.collider.transform.position;
                    Controller.MoveToObject(ObjPos);

                    EnableToSelect = false;
                    UnselectAll();

                  

                    if (Cursor)
                    {
                        if (ListOfSelected.Contains(Cursor) == false)
                        {
                            Cursor.Select();
                            Cursor.SelectIndicator.SetActive(false);
                        }

                    }
                    
                    
                }
                if (Input.GetMouseButtonDown(1) && EnableToSelect)
                {
                    if (ListOfSelected.Contains(Cursor))
                    {
                        Cursor.UnSelect();
                        textController.objects.Contains(Cursor);

                    }
                    
                    else if (ListOfSelected.Contains(Cursor) == false)
                    {
                        if (CheckBox) { Cursor.Select(); }
              

                    }
                  

                }
            }
        }
    }

    public void UnselectAll()
    {
        int C = ListOfSelected.Count;
        for (int i = 0; i < C; i++)
        {
            ListOfSelected[0].UnSelect();
        }
        
        
    }

    public void ChangeTransparency(float transparency)
    {
        if (ListOfSelected != null)
        {
            for (int i = 0; i < ListOfSelected.Count; i++)
            {

                Material mat = ListOfSelected[i].Model.GetComponent<Renderer>().material;
                Color color = mat.GetColor("_BaseColor");
                color.a = transparency;
                mat.SetColor("_BaseColor", color);

            }


        }
        
        
    }

    public void Hide()
    {
        if (ListOfSelected != null)
        {
            for(int i = 0;i < AllObj.Count; i++)
            {

                AllObj[i].HideObj(ModelHide);
                textController.togglesHide[i].isOn = ModelHide;

            }
            switch (ModelHide)
            {
                case false:
                    ModelHide = true;
                    break;
                case true:
                    ModelHide = false;
                    break;

            }
        }
    }

    public void HideSelect()
    {

        if (!CheckBox)
        {
            foreach(SelectableObject o in ListOfSelected)
            {
                o.SelectIndicator.SetActive(true);
            }
            CheckBox = true;
        }
        else if (CheckBox) 
        {
            foreach (SelectableObject o in ListOfSelected)
            {
                o.SelectIndicator.SetActive(false);
            }
            CheckBox = false;
        }
    }


}
