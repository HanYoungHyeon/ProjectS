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

    private void Start()
    {
        showSlotInfo = GetComponentInChildren<ShowSlotInfo>();
        slimeFarm = GetComponentInParent<SlimeFarmMachine>();
        StartCoroutine(AutoFeeding());
    }

    private void Update()
    {
        GetCrops();
    }
    private void LateUpdate()
    {
        UpdateUI();
    }

    private void GetCrops()
    {
        Collider[] colliders = Physics.OverlapSphere(cropsInputTransform.position, 1f, cropsLayer);
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Crop targetCrop = colliders[i].GetComponent<SceneCrop>().Crop;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Vaccum"))
        {
            StartCoroutine(SummonCrop());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Vaccum"))
        {
            StopCoroutine(SummonCrop());
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
    IEnumerator AutoFeeding()
    {
        while(true)
        {
            yield return new WaitUntil(() =>  count > 0 );
            yield return new WaitForSeconds(10f);
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
                Vector3 random = new Vector3(0, 0, Random.Range(-0.8f, 0.8f));
                cropRigidbody.AddForce(feedTransform.forward+random,ForceMode.Impulse);
                count--;
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
