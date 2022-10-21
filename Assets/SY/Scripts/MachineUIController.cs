using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MachineUIController : MonoBehaviour
{
    [SerializeField]
    private Farm farm;
    [SerializeField]
    private List<GameObject> upgradeList;
    [SerializeField]
    private Sprite upgradeSprite;
    [SerializeField]
    private TextMeshProUGUI upgradeName;
    [SerializeField]
    private TextMeshProUGUI upgradeInfo;

    public void Awake()
    {

        for(int i = 0; i < farm.Upgrades.Count; i++)
        {
            TextMeshProUGUI tempUGUI = upgradeList[i].GetComponentInChildren<TextMeshProUGUI>();
            upgradeList[i].SetActive(true);
            tempUGUI.text = farm.Upgrades[i].UpgradeName;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            upgradeList[0].SetActive(false);
        }
    }
}
