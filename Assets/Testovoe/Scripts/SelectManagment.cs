using System.Collections.Generic;
using UnityEngine;

public class SelectManagment : MonoBehaviour
{

    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();
    public List<SelectableObject> AllObj = new List<SelectableObject>();
    public GameObject AllSelectObj;


    private bool ModelHide = false;

    [SerializeField]
    public bool EnableToSelect = true;
    public bool CheckBox = true;

    [SerializeField]
    private TextController TextController;


    private void Awake()
    {
        AllObj = new List<SelectableObject>(AllSelectObj.GetComponentsInChildren<SelectableObject>());
    }

    public void UnselectAll()
    {
        if (ListOfSelected != null)
        {
            int count = ListOfSelected.Count;
            for (int i = 0; i < count; i++)
            {
                ListOfSelected[0].UnSelect();
            }
        }
    }

    public void ChangeTransparencyAll(float alpha)
    {
        if (ListOfSelected != null)
        {
            foreach (SelectableObject obj in ListOfSelected)
            {
                obj.ChangeTransparency(alpha);
            }
        }
    }

    public void HideAll()
    {

        for (int i = 0; i < AllObj.Count; i++)
        {
            AllObj[i].HideObj(ModelHide);
            TextController.togglesHide[i].isOn = ModelHide;

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

    public void HideSelect()
    {

        if (TextController.AllSelectionToggle.isOn)
        {

            foreach (SelectableObject o in AllObj)
            {
                o.Select();
            }

        }
        else if (!TextController.AllSelectionToggle.isOn && ListOfSelected.Count !=0)
        {
            int count = ListOfSelected.Count;
            for (int i = 0; i < count; i++)
            {
                ListOfSelected[0].UnSelect();
            }
        }
    }
}
