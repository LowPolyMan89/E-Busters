using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class DoorDataPanel : MonoBehaviour
{
    public Text DoorId;
    public Image StatusImage;
    public Door Door;
    private bool doorStatus;
    [SerializeField] private List<Button> buttons = new List<Button>();


    private void Update()
    {
        if(Door.IsEmergencyStatus)
        {
            foreach(var b in buttons)
            {

                b.interactable = false;
            }
        }
        else
        {
            foreach (var b in buttons)
            {

                b.interactable = true;
            }
        }
    }


    public bool DoorStatus { get => doorStatus; }

    public void SetStatus(bool value)
    {
        doorStatus = value;
        
        if(doorStatus)
        {
            StatusImage.color = Color.green;

            Door.Activate(true);
            
        }
        else
        {
            StatusImage.color = Color.red;

            Door.Activate(false);
        }
    }

    public void CreateObject()
    {

    }

    public void DestroyObject()
    {
        Destroy(this, 0.2f);
    }
}
