using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTerminal : Terminal
{
    [SerializeField] private List<Door> doors = new List<Door>();
    private List<DoorDataPanel> doorDataPanels = new List<DoorDataPanel>();

    public List<Door> Doors { get => doors;}


    public override void UseTerminal()
    {

        CreateDoorPanels(this);
    }

    public DoorTerminal CreateDoorPanels(DoorTerminal terminal)
    {
        DataProvider.Instance.BattleUI.ClearDoorPanel();

        foreach (var x in doors)
        {
            DataProvider.Instance.BattleUI.CreateDoorDataPanelObject(x, x.DoorId, x.IsActive);
        }

        return this;
        
    }

}
