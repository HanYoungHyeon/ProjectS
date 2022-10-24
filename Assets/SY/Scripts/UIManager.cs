using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private MachineUI machineUI;
    public MachineUI MachineUI { get { return machineUI; } set { machineUI = value; } }
    [SerializeField]
    private MachineController machineController;
    public MachineController MachineController { get { return machineController; } set { machineController = value; } }

}
