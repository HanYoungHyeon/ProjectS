using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BC
{
    public class VacuumPack : MonoBehaviour
    {
        [SerializeField]
        GameObject particle;
        [SerializeField]
        private Slot[] slots;
        private Slot curSlot;
        [SerializeField]
        private int slotIndex;
        [SerializeField]
        private float releasePower; 
        [SerializeField]
        private Collider[] vacuumPackColliders;
        [SerializeField]
        private Transform releasPosition;
        //public enum TYPE { slime, item }
        //public TYPE type = TYPE.slime;
        public bool isSlime;
        private bool isRelease;
        private Dictionary<Slot, UnityAction> slotRelease = new Dictionary<Slot, UnityAction>();

        private void Awake()
        {
            curSlot = slots[0];
        }

        private void Update()
        {
            ChangeMode();
            //if(OVRInput.GetDown(OVRInput.Button.Four))
            if (Input.GetKeyDown(KeyCode.K))
            {
                SlotChange();
            }
            //if(OVRInput.GetDown(OVRInput.Button.One))
            if (Input.GetKeyDown(KeyCode.L)|| Input.GetKeyUp(KeyCode.L))
            {
                if(!isRelease) ItemAbsorption();
            }
            if(Input.GetKeyDown(KeyCode.L)&&isRelease)
            {
                ItemRelease();
            }          
        }

        private void ChangeMode()
        {
            //if(OVRInput.GetDown(OVRInput.Button.Two))
            if (Input.GetKeyDown(KeyCode.T))
            {
                isRelease = !isRelease;
            }
        }

        private void SlotChange()
        {
            slotIndex++;
            if(slotIndex == slots.Length)
            {
                slotIndex = 0;
            }
            curSlot = slots[slotIndex];
        }

        private void ItemAbsorption()
        {
            particle.SetActive(!particle.activeSelf);
            foreach (var col in vacuumPackColliders)
            {
                col.enabled = !col.enabled;
            }
        }

        private void ItemRelease()
        {
            if (curSlot.ItemCount == 0) return;
            slotRelease[curSlot]?.Invoke();
            curSlot.ItemCount--;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IItemable target))
            {
                bool isInteracted = false;
                Item curItem = target.ItemRequest();
                foreach(var slot in slots)
                {
                    if(slot.curSlotItem == curItem)
                    {
                        slot.ItemCount++;
                        target.ItemReturn();
                        isInteracted = true;
                        break;
                    }
                }
                if(!isInteracted)
                {
                    foreach (var slot in slots)
                    {
                        if (slot.curSlotItem == null)
                        {                       
                            slot.AddItem(curItem);
                            slotRelease[slot] = ReleaseCheck(curItem);
                            slot.ItemCount++;
                            target.ItemReturn();
                            GameManager.Instance.TakeItem(curItem);
                            break;
                        }
                    }
                }
            }
        }


        private UnityAction ReleaseCheck(Item item)
        { 
            System.Type itemType = item.GetType();
            if (itemType == typeof(Gem))
            {
                return ReleaseGem;
            }
            if(itemType == typeof(Slime))
            {
                return ReleaseSlime;
            }
            if(itemType == typeof(Crop))
            {
                return ReleaseCrop;
            }
            return null;
        }

        private void ReleaseGem()
        {
            ReleaseForce(ItemManager.Instance.CreateSceneItem(curSlot.curSlotItem as Gem, releasPosition.position));
        }
        private void ReleaseCrop()
        {
            ReleaseForce(ItemManager.Instance.CreateSceneItem(curSlot.curSlotItem as Crop, releasPosition.position));
        }
        private void ReleaseSlime()
        {
            ReleaseForce(ItemManager.Instance.CreateSceneItem(curSlot.curSlotItem as Slime, releasPosition.position));
        }

        private void ReleaseForce(Transform itemTransform)
        {
            itemTransform.TryGetComponent(out IInteraction target);
            target.MoveStop();
            target.rigi.AddForce(releasPosition.forward * releasePower);
        }
    }
}

