using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoFeed : MonoBehaviour
{
    [SerializeField]
    private LayerMask cropsLayer;
    [SerializeField]
    private int feedCount = 3;
    [SerializeField]
    private Transform feedTransform;
    [SerializeField]
    private Transform cropsInputTransform;

    private ShowSlotInfo showSlotInfo;
    private SlimeFarmMachine slimeFarm;
    private Crop crop;
    private int count = 0;
    private float coolTime = 5f;
    private bool isCoolTime = false;

    private void Start()
    {
        showSlotInfo = GetComponentInChildren<ShowSlotInfo>();
        slimeFarm = GetComponentInParent<SlimeFarmMachine>();
        StartCoroutine(CheckCoolTime(coolTime));
    }

    private void Update()
    {
        AutoFeeding();
        GetCrops();
    }
    private void LateUpdate()
    {
        UpdateUI();
    }

    private void AutoFeeding()
    {
        if (isCoolTime)
            return;
        for (int i = 0; i < feedCount; i++)
        {
            if (count <= 0)
            {
                crop = null;
                break;
            }
            Transform cropTranform = ItemManager.Instance.CreateSceneItem(crop, feedTransform.position);
            Rigidbody cropRigidbody = cropTranform.GetComponent<Rigidbody>();
            cropTranform.parent = slimeFarm.InsideObject.transform;
            cropRigidbody.AddForce(cropTranform.forward);
            count--;
        }
    }

    private void GetCrops()
    {
        Collider[] colliders = Physics.OverlapSphere(cropsInputTransform.position, 1f, cropsLayer);
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Crop targetCrop = colliders[i].GetComponent<Crop>();
                if(crop == null)
                {
                    crop = targetCrop;
                }
                if (crop != targetCrop)
                    continue;
                count++;
                colliders[i].TryGetComponent(out IPoolingable target);
                target.home.Return(colliders[i].gameObject);
            }
        }
    }
    private void UpdateUI()
    {
        showSlotInfo.ChangeImage(crop, count);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vacuum")
        {
            StartCoroutine(SummonCrop());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Vacuum")
        {
            StopCoroutine(SummonCrop());
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
    IEnumerator SummonCrop()
    {
        while(count > 0)
        {
            ItemManager.Instance.CreateSceneItem(crop, transform.position);
            count--;
            if(count <=0)
            {
                crop = null;
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
}
