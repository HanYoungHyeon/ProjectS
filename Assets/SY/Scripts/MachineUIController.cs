using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineUIController : MonoBehaviour
{
    [SerializeField]
    private Farm farm;
    [SerializeField]
    private List<GameObject> upgradeList;
    [SerializeField]
    private Image upgradeSprite;
    [SerializeField]
    private TextMeshProUGUI upgradeName;
    [SerializeField]
    private TextMeshProUGUI upgradeInfo;
    [SerializeField]
    private TextMeshProUGUI upgradePrice;

    

    //UI창 활성화될 때마다 남은 UpgradeList만큼 활성화
    public void Awake()
    { 
        for(int i = 0; i < farm.Upgrades.Count; i++)
        {
            TextMeshProUGUI tempUGUI = upgradeList[i].GetComponentInChildren<TextMeshProUGUI>();
            UIEventFunc tempUpgrade = upgradeList[i].GetComponentInChildren<UIEventFunc>();
            upgradeList[i].SetActive(true);
            tempUGUI.text = farm.Upgrades[i].UpgradeName;
            tempUpgrade.upgrade = farm.Upgrades[i];
        }
    }

    public void OnDisable()
    {
        //list 비우기
        Debug.Log("g");
        upgradeList.RemoveRange(0, farm.Upgrades.Count);
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.O))
        {
            upgradeList[0].SetActive(false);
            upgradeList.RemoveAt(0);
        }
    }

    public void ShowInfo(Upgrade upgrade)
    {
        upgradeName.text = upgrade.UpgradeName;
        upgradePrice.text = upgrade.RequireCoin.ToString();
        upgradeInfo.text = upgrade.UpgradeInfo;
    }
}
