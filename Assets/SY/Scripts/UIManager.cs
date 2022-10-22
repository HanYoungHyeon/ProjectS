using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject machineUI;
    public GameObject MachineUI { get { return machineUI; } set { machineUI = value; } }
    [SerializeField]
    private MachineUIController machineUIController;
    public MachineUIController MachineUIController { get { return machineUIController; } set { machineUIController = value; } }

}
