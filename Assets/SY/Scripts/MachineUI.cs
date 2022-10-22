using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineUI : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> upgradeList = new List<GameObject>();
    [SerializeField]
    private Image upgradeSprite;
    [SerializeField]
    private TextMeshProUGUI upgradeName;
    [SerializeField]
    private TextMeshProUGUI upgradeInfo;
    [SerializeField]
    private TextMeshProUGUI upgradePrice;
    public Farm farm;



    //UI창 활성화될 때마다 남은 UpgradeList만큼 활성화
    public void Awake()
    {
        farm = UIManager.Instance.MachineUIController.farm;
        Debug.Log(farm.Upgrades.Count);
        
    }

    private void OnEnable()
    {
        for (int i = 0; i < farm.Upgrades.Count; i++)
        {
            TextMeshProUGUI upgradeUGUI = upgradeList[i].GetComponentInChildren<TextMeshProUGUI>();
            UIEventFunc upgrade = upgradeList[i].GetComponentInChildren<UIEventFunc>();
            upgradeList[i].SetActive(true);
            upgradeUGUI.text = farm.Upgrades[i].UpgradeName;
            upgrade.upgrade = farm.Upgrades[i];
        }
    }

    public void OnDisable()
    {
        //list 비우기
        //upgradeList.RemoveRange(0, farm.Upgrades.Count);
    }

    public void ShowInfo(Upgrade upgrade)
    {
        upgradeName.text = upgrade.UpgradeName;
        upgradePrice.text = upgrade.RequireCoin + "";
        upgradeInfo.text = upgrade.UpgradeInfo;
    }

    public void BuyUpgrade()
    {
        //캐릭터 소유 금액 -= 업그레이드 필요금액
        CameraTest.money -= int.Parse(upgradePrice.text);
        Debug.Log(CameraTest.money);
    }
}
