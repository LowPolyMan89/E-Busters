using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text Name;

    internal void CompliteQuest()
    {
        Name.color = Color.green;
        Destroy(this.gameObject, 2f);
    }
}
