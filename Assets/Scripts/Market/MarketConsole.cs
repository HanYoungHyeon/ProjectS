using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarketConsole : MonoBehaviour
{
    [SerializeField]
    private MarketWindow marketWindow;
    [SerializeField]
    private MarketSlot slot;
    private bool isActive;

    //MarketSlotÀÇ slotAction(MarketSlotÀÇ TriggerEnter½Ã ¹ß»ýÇÒ °ñµæ È¹µæ ÀÌº¥Æ®)
    public void Awake()
    {
        transform.FindComponentChild(out slot);
        transform.FindComponentChild(out marketWindow);
        slot.slotAction = (Gem gem)=> GameManager.Instance.playerGold += marketWindow.detectionGems[gem].curPrice;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController target))
        {
            marketWindow.gameObject.SetActive(true);
            marketWindow.MarketPopUp();
            slot.SlotSwitch();
            isActive = true;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if(isActive)
        {
            slot.SlotSwitch();
            marketWindow.gameObject.SetActive(false);
            isActive = false;
        }
    }
}
