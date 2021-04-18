using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLootBoxAndGetMedKitQuest : Quest
{
    Inventory inventory;
    public string ItemName;

    private void Start()
    {
        inventory = DataProvider.Instance.Player.Inventory;
        print("Start Quest: " + QuestName);
    }

    public override IEnumerator QuestUpdate()
    {
        yield return new WaitForSeconds(1f);


        if(inventory.GetItem(ItemName))
        {
            print("Quest complite: " + QuestName);
            QuestCompliteAction();
        }
        else
        {
            StartCoroutine(QuestUpdate());
        }
        
    }

    public override void StartQuest(GameObject panel)
    {
        QuestPanel = panel;
        StartCoroutine(QuestUpdate());
    }
}
