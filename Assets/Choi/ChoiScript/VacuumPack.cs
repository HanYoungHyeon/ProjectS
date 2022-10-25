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
        Color[] colors;
        GameObject particle;
        private List<Slot> slots = new List<Slot>();
        private Slot curSlot;
        private int slotIndex;
        [SerializeField]
        private float releasePower;
        private Collider[] vacuumPackColliders = new Collider[2];
        private Transform releasPosition;
        private UnityEngine.UI.Image image;
        public bool isSlime;
        private bool isRelease;
        private bool isAbsorption;
        private float releasDelay;
        private Dictionary<Slot, UnityAction> slotRelease = new Dictionary<Slot, UnityAction>();

        private void Awake()
        {
            transform.FindComponentChild(out ParticleSystem particleGo);
            particle = particleGo.gameObject;
            vacuumPackColliders[0] = GetComponent<Collider>();      
            transform.FindComponentChild(out MeshCollider tempColliders);
            vacuumPackColliders[1] = tempColliders;
            Transform windowTransform = transform.FindComponentChild<Canvas>().GetChild(0);
            windowTransform.TryGetComponent(out image);
            for(int i=0; i<windowTransform.childCount; i++)
            {
                slots.Add(windowTransform.GetChild(i).GetComponent<Slot>());
            }
            curSlot = slots[0];
            releasPosition = transform.Find("ReleasPosition");
        }



        private void Update()
        {
            ChangeMode();
            if(OVRInput.GetDown(OVRInput.Button.Four))
            //if (Input.GetKeyDown(KeyCode.K))
            {
                SlotChange();
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            //if (Input.GetKeyDown(KeyCode.L)|| Input.GetKeyUp(KeyCode.L))
            {
                if(isRelease)
                {
                    ItemRelease();
                }
                else
                {
                    ItemAbsorption(true);
                } 
            }
            if(OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                if(!isRelease)
                {
                    ItemAbsorption(false);
                }
            }
            //if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)&&isRelease)
            ////if (Input.GetKeyDown(KeyCode.L) && isRelease)
            //{
               
            //}
        }

        private void ChangeMode()
        {
            if(OVRInput.GetDown(OVRInput.Button.Two))
            //if (Input.GetKeyDown(KeyCode.T))
            {
                if(isRelease)
                {
                    image.color = colors[0];
                    isRelease = false;
                }
                else
                {
                    image.color = colors[1];
                    isRelease = true;
                }
                if (isAbsorption) ItemAbsorption(false);

            }
        }

        private void SlotChange()
        {
            slotIndex++;
            if(slotIndex == slots.Count)
            {
                slotIndex = 0;
            }
            curSlot.ScaleUp(false);
            curSlot = slots[slotIndex];
            curSlot.ScaleUp(true);
        }

        private void ItemAbsorption(bool active)
        {
            isAbsorption = active;
            particle.SetActive(active);
            foreach (var col in vacuumPackColliders)
            {
                col.enabled = active;
            }
        }

        private void ItemRelease()
        {
            releasDelay += Time.fixedDeltaTime;
            if (curSlot.ItemCount == 0) return;
            if(releasDelay>0.3f)
            {
                slotRelease[curSlot]?.Invoke();
                curSlot.ItemCount--;
                releasDelay = 0;
            }
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

