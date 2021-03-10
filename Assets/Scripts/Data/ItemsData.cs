using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsData", order = 1)]
public class ItemsData : ScriptableObject
{
    public List<ItemData> ItemsDatas = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    public string ID;
    public Sprite Sprite;
    public GameObject Prefab;
}
