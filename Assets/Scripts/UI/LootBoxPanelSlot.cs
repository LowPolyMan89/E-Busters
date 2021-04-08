﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxPanelSlot : MonoBehaviour
{
    public Item item;
    public Image image;

    private void Start()
    {
        image.sprite = DataProvider.Instance.BattleUI.emptySprite;
    }
}
