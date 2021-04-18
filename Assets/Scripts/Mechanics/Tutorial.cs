using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField]private Quest current;
    [SerializeField]private int questNumber = 0;

    private void Start()
    {
        Invoke("StartTutorial", 1f);
    }

    private void StartTutorial()
    {
        SetPlayerHP();
        current = DataProvider.Instance.QuestSystem.StartQuest("Hospital");
        questNumber++;
        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        yield return new WaitForSeconds(1f);

        if(questNumber == 1 && current == null)
        {
            print("NextTutorialQuest");
            current = DataProvider.Instance.QuestSystem.StartQuest("GetMedkit");
            questNumber++;
        }
        if (questNumber == 2 && current == null)
        {
            print("NextTutorialQuest");
            current = DataProvider.Instance.QuestSystem.StartQuest("HP100");
            questNumber++;
        }
        StartCoroutine(SlowUpdate());
    }

    public void SetPlayerHP()
    {
        DataProvider.Instance.Events.PlayerChangeHPEvent(-50, DataProvider.Instance.Player);

    }
}
