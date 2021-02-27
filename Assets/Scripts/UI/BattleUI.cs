using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private DataProvider dataProvider;

    [SerializeField] private Image bulletCountSprite;
    [SerializeField] private Image noizeCountSprite;
    [SerializeField] private Text ammoCountText;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnUiUpdate += UpdateUI;
        Invoke("UpdateUI", 0.5f);
    }

    public void UpdateUI()
    {
        bulletCountSprite.fillAmount = (float)(dataProvider.CurrentWeapon.AmmoCount / dataProvider.CurrentWeapon.weaponData.AmmoCount);
        noizeCountSprite.fillAmount = dataProvider.CurrentMapData.Noize / 100f;
        ammoCountText.text = dataProvider.CurrentWeapon.AmmoStorage.ToString("");
    }

    private void OnDisable()
    {
        dataProvider.Events.OnUiUpdate -= UpdateUI;
    }
}
