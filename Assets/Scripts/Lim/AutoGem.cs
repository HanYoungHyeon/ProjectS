using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GemsCount
{
    public Gem gem = null;
    public int count = 0;
}

public class AutoGem : MonoBehaviour
{
    [SerializeField]
    private LayerMask gemLayer;
    [SerializeField]
    private Transform centerTransform;
    [SerializeField]
    private Transform interactionTransform;
    [SerializeField]
    private Transform harvestTransform;
    [SerializeField]
    private List<GemsCount> gems;
    [SerializeField]
    private Vector3 boxSize;
    private ShowSlotInfo[] showSlotInfos;
    private float coolTime = 10f;
    private int slotCount = 2;
    private int totalCount = 0;
    private bool isCoolTime = false;
    private void Start()
    {
        showSlotInfos = GetComponentsInChildren<ShowSlotInfo>();
        gems = new List<GemsCount>(slotCount);
        StartCoroutine(CheckCoolTime(coolTime));
    }
    private void Update()
    {
        HarvestGem();
    }
    private void LateUpdate()
    {
        UpdateSlot();
    }

    private void HarvestGem()
    {
        if (isCoolTime)
            return;
        Collider[] colliders = Physics.OverlapBox(centerTransform.position, boxSize, Quaternion.identity, gemLayer);
        if(colliders.Length>0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].TryGetComponent(out SceneGem target);
                colliders[i].TryGetComponent(out Rigidbody targetRb);
                StartCoroutine(VaccumGem(target, targetRb));
                //Gem gem = target.ItemRequest() as Gem;
                //bool check = CheckGemSlot(gem);
                //if(check)
                //{
                //    target.home.Return(colliders[i].gameObject);
                //    totalCount++;
                //}
            }
        }
    }
    private bool CheckGemSlot(Gem gem)
    {
        if(gems.Count == 0)
        {
            gems.Add(new GemsCount());
            gems[0].gem = gem;
            gems[0].count++;
            return true;
        }
        else if (gems.Count < slotCount)
        {
            for (int i = 0; i < gems.Count; i++)
            {
                if (gems[i].gem != gem)
                    continue;
                if (gems[i].count >= 100)
                    continue;
                gems[i].count++;
                return true;
            }
            gems.Add(new GemsCount());
            gems[gems.Count - 1].gem = gem;
            gems[gems.Count - 1].count++;
            return true;
        }
        else if (gems.Count == slotCount)
        {
            for (int i = 0; i < gems.Count; i++)
            {
                if (gems[i].gem != gem)
                    continue;
                if (gems[i].count >= 100)
                    continue;
                gems[i].count++;
                return true;
            }
            return false;
        }
        return false;
    }
    private void UpdateSlot()
    {
        for (int i = 0; i < showSlotInfos.Length; i++)
        {
            if(i > gems.Count-1)
            {
                showSlotInfos[i].ChangeImage(null, 0);
            }
            else
            {
                showSlotInfos[i].ChangeImage(gems[i].gem, gems[i].count);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Vaccum"))
        {
            StartCoroutine(SummonGem());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Vaccum"))
        {
            StopCoroutine(SummonGem());
        }
    }

    IEnumerator CheckCoolTime(float value)
    {
        while (true)
        {
            yield return new WaitUntil(() => isCoolTime == false);
            isCoolTime = true;
            yield return new WaitForSeconds(value);
            isCoolTime = false;
        }
    }
    IEnumerator SummonGem()
    {
        while(totalCount>0)
        {
            int slotNum = UnityEngine.Random.Range(0, gems.Count);
            ItemManager.Instance.CreateSceneItem(gems[slotNum].gem, interactionTransform.position);
            gems[slotNum].count--;
            totalCount--;
            if (gems[slotNum].count <=0)
            {
                gems.RemoveAt(slotNum);
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
    IEnumerator VaccumGem(SceneGem target,Rigidbody targetRb)
    {
        while (true)
        {
            if (Vector3.Distance(target.transform.position, harvestTransform.position) < 1f)
                break;
            targetRb.velocity = (harvestTransform.position-targetRb.transform.position).normalized*3f;
            Debug.Log(Vector3.Distance(harvestTransform.position, targetRb.transform.position));
            yield return new WaitForSeconds(0.1f);
        }
        Gem gem = target.ItemRequest() as Gem;
        bool check = CheckGemSlot(gem);
        if (check)
        {
            target.home.Return(target.gameObject);
            totalCount++;
        }
        yield return null;
    }
}
