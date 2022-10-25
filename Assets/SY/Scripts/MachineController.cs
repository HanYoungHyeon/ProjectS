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
    public List<bool> isUpgrades = new List<bool>();
    private bool isSelectedFarm = false;
    [SerializeField]
    private List<GameObject> farmListUI = new List<GameObject>();
    private GameObject selectFarm;
    public Upgrade selectUpgrade;

    private void Awake()
    {
        for (int i = 0; i < upgradeList.Count; i++)
        {
            isUpgrades.Add(false);
        }
        selectFarm = farmListUI[0];
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            OnUI(selectFarm);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OffUI(selectFarm);
            if(isSelectedFarm == true)
            {
                UIManager.Instance.MachineUI.CountUpgradeList();
            }
        }
    }

    //상호작용했을 때
    public void OnUI(GameObject selectFarm)
    {
        selectFarm.SetActive(true);
    }
    public void OffUI(GameObject selectFarm)
    {
        selectFarm.SetActive(false);
    }

    public void BuyUpgrade()
    {
        //캐릭터 소유 금액 -= 업그레이드 필요금액;
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
            isUpgrades[farm.Upgrades.IndexOf(selectUpgrade)] = true;
            farmMachine.StartFunction(selectUpgrade.UpgradeName);
            UIManager.Instance.MachineUI.CountUpgradeList();
            Debug.Log("업그레이드 성공");
        }  
    }

    public void SelectFarm()
    {
        farmListUI[0].SetActive(false);
        farmListUI[1].SetActive(true);
        UIManager.Instance.MachineUI.CountUpgradeList();
        selectFarm = farmListUI[1];
        UIManager.Instance.MachineUI.ShowTitle();
        isSelectedFarm = true;
    }
}
