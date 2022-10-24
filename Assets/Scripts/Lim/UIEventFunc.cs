using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventFunc : MonoBehaviour
{
    public UnityEvent OnClick;
    public UnityEvent OffClick;
    public Upgrade upgrade;
    private int upgradePrice;
    private bool isClick = false;

    public void ActiveFalse(GameObject target)
    {
        target.SetActive(false);
    }
    public void ActiveTrue(GameObject target)
    {
        target.SetActive(true);
    }
    public void ChangeValue(Scrollbar target)
    {
        if (isClick)
        {
            target.value += Input.GetAxis("Vertical") * 0.5f;
        }
    }
    public void ShowInfo()
    {
        UIManager.Instance.MachineUI.ShowInfo(upgrade);
    }

    public void BuyUpgrade()
    {
        UIManager.Instance.MachineController.BuyUpgrade();
    }

    public void ChangeBool(bool value)
    {
        isClick = value;
    }

}
