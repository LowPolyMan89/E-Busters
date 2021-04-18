using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{

    public List<Quest> Quests = new List<Quest>();

    public void Start()
    {
        StartCoroutine(QuestQ());
    }

    public Quest GetQuest(string quest)
    {
        Quest _quest = null;

            foreach(Quest v in Quests)
            {
                if (quest == v.name)
                {
                _quest = v;
                }
            }

        return _quest;
        
    }

    private IEnumerator QuestQ()
    {
        yield return new WaitForSeconds(0.2f);

        List<Quest> temp = new List<Quest>();

        foreach(var v in Quests)
        {
            if (v.isComplited)
                temp.Add(v);
        }

        foreach (var v in temp)
        {
            Quests.Remove(v);
            Destroy(v);
        }

        StartCoroutine(QuestQ());
    }

    public Quest StartQuest(string questName)
    {
        Quest newQuest = null;

        foreach (var v in DataProvider.Instance.QuestData.Quests)
        {
            if(v.QuestName == questName)
            {
                newQuest = Instantiate(v);
                newQuest.StartQuest(DataProvider.Instance.BattleUI.CreateQuestPanel(v.QuestName, v.QuestDescription));
                Quests.Add(newQuest);          
            }

        }

        return newQuest;
    }

}

[System.Serializable]
public class Quest : MonoBehaviour
{
    public string QuestName = "emptyquest";
    public string QuestDescription = "empty_description";
    public QuestCondition QuestConditions = QuestCondition.Action;
    public bool isComplited = false;
    public GameObject QuestPanel;

    public virtual void StartQuest(GameObject panel)
    {
        QuestPanel = panel;
        StartCoroutine(QuestUpdate());
    }

    public enum QuestCondition
    {
        EnterTrigger,
        UseItem,
        PlayerHP,
        KillEnemy,
        Action
    }

    public virtual IEnumerator QuestUpdate()
    {
        yield return null;
    }

    public virtual void QuestCompliteAction()
    {
        StopCoroutine(QuestUpdate());
        QuestUI questUI = QuestPanel.GetComponent<QuestUI>();
        questUI.CompliteQuest();
        isComplited = true;
    }
}

