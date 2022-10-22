using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineUIController : MonoBehaviour
{
    public Farm farm;
    [SerializeField]
    private GameObject machineUI;
    private Farm machineFarm;

    private void Awake()
    {
        machineFarm = machineUI.GetComponent<MachineUI>().farm;
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
}
