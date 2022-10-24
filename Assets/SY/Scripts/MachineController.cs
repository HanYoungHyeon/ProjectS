using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineController : MonoBehaviour
{
    public Farm farm;
    [SerializeField]
    private FarmMachine farmMachine;
    [SerializeField]
    private GameObject machineUI;
    public List<GameObject> upgradeList = new List<GameObject>();
    public List<bool> isUpgrades;
    public Upgrade selectUpgrade;

    private void Awake()
    {
        for (int i = 0; i < upgradeList.Count; i++)
        {
            isUpgrades.Add(false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            OnUI();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OffUI();
        }
    }

    //상호작용했을 때
    public void OnUI()
    {
        machineUI.SetActive(true);
    }
    public void OffUI()
    {
        machineUI.SetActive(false);
    }

    public void BuyUpgrade()
    {
        //캐릭터 소유 금액 -= 업그레이드 필요금액;
        //얘 왤케 많이 실행됨??
        Debug.Log(selectUpgrade.RequireCoin);
        if(CameraTest.money - selectUpgrade.RequireCoin < 0)
        {
            UIManager.Instance.MachineUI.upgradeFail.SetActive(true);
            Debug.Log("업그레이드 실패");
        }
        else
        {
            UIManager.Instance.MachineUI.upgradeSuccess.SetActive(true);
            CameraTest.money -= selectUpgrade.RequireCoin;
            isUpgrades[farm.Upgrades.FindIndex(x => x.name == selectUpgrade.name)] = true;
            UIManager.Instance.MachineUI.CountUpgradeList(); 
            Debug.Log("업그레이드 성공");
        }

    }
}
