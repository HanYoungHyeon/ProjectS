using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineUI : MonoBehaviour
{
    [SerializeField]
    private Image upgradeSprite;
    [SerializeField]
    private TextMeshProUGUI upgradeName;
    [SerializeField]
    private TextMeshProUGUI upgradeInfo;
    [SerializeField]
    private TextMeshProUGUI upgradePrice;
    public GameObject upgradeFail;
    public GameObject upgradeSuccess;
    private MachineController machineUIController;
    public Farm farm;

    private void Awake()
    {
        machineUIController = UIManager.Instance.MachineController;
        farm = UIManager.Instance.MachineController.farm;
       
    }


    //UI창 활성화될 때마다 남은 UpgradeList만큼 활성화
    public void OnEnable()
    {
       
        CountUpgradeList();
    }


    public void ShowInfo(Upgrade upgrade)
    {
        upgradeName.text = upgrade.UpgradeName;
        upgradePrice.text = upgrade.RequireCoin + "";
        upgradeInfo.text = upgrade.UpgradeInfo;
        machineUIController.selectUpgrade = upgrade;
    }

    
    public void CountUpgradeList()
    {
        for (int i = 0; i < farm.Upgrades.Count; i++)
        {
            machineUIController.upgradeList[i].SetActive(false);
        }
        for (int i = 0; i < farm.Upgrades.Count; i++)
        {
            if (UIManager.Instance.MachineController.isUpgrades[i] == false)
            {
                TextMeshProUGUI upgradeUGUI = machineUIController.upgradeList[i].GetComponentInChildren<TextMeshProUGUI>();
                UIEventFunc upgrade = machineUIController.upgradeList[i].GetComponentInChildren<UIEventFunc>();
                machineUIController.upgradeList[i].SetActive(true);
                upgradeUGUI.text = farm.Upgrades[i].UpgradeName;
                upgrade.upgrade = farm.Upgrades[i];
            }
           
        }
    }

}
