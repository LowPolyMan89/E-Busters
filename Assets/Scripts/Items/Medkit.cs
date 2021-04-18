using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : UsableItem
{
    [ContextMenu("Use medkit")]
    public override void Use()
    {
        DataProvider.Instance.Player.SetPlayerHP(100);
        base.UseItem();
    }
}
