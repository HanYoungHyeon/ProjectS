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
    private SlimeFarmMachine slimeMachine;
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


    private void OnTriggerEnter(Collider other)
    {
        UISwitch(selectFarm, other);
    }

    private void OnTriggerExit(Collider other)
    {
        UISwitch(selectFarm, other);
    }

    public void UISwitch(GameObject selectFarm,Collider other)
    {
        if (other.TryGetComponent(out BC.Player player))
        {
            selectFarm.SetActive(!selectFarm.activeSelf);
            if (isSelectedFarm == true)
            {
                UIManager.Instance.MachineUI.CountUpgradeList();
            }
        }
    }

    public void BuyUpgrade()
    {
        //캐릭터 소유 금액 -= 업그레이드 필요금액;
        Debug.Log(selectUpgrade.RequireCoin);
        if(GameManager.Instance.playerGold - selectUpgrade.RequireCoin < 0)
        {
            UIManager.Instance.MachineUI.upgradeFail.SetActive(true);
            Debug.Log("업그레이드 실패");
        }
        else
        {
            UIManager.Instance.MachineUI.upgradeSuccess.SetActive(true);
            GameManager.Instance.playerGold -= selectUpgrade.RequireCoin;
            isUpgrades[farm.Upgrades.IndexOf(selectUpgrade)] = true;
            if(farm.name == "PlantFarm")
            {
                farmMachine.StartFunction(selectUpgrade.UpgradeName);
            }
            else if(farm.name == "SlimeFarm")
            {
                slimeMachine.StartFunction(selectUpgrade.UpgradeName);
            }
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
