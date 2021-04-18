using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToArea : Quest
{
    Player player;
    public string AreaName;

    private void Start()
    {
        player = DataProvider.Instance.Player;
        print("Start Quest: " + QuestName);
    }

    public override IEnumerator QuestUpdate()
    {
        yield return new WaitForSeconds(1f);


        if (player.PlayerAreaName == AreaName)
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
