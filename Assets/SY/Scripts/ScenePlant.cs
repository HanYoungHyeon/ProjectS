using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Filtering;
using Unity.Services.Analytics.Platform;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class ScenePlant : MonoBehaviour, IPoolingable
{
    [SerializeField]
    private Crop crop;
    public Crop Crop { get { return crop; } set { crop = value; } }
    [SerializeField]
    private Plant plant;
    public Plant Plant { get { return plant; }  set { plant = value; } }
    private float bornTime;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private bool isGrow;
    public bool isWater;
    public float growTime;
    public float rotTime;
    public ObjectPool home { get; set; }

    private void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(SettingDelay());
        isWater = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TimeChange();
        }
    }
    public void FirstSetting()
    {
        CropManager.Instance.timeChange += TimeChange;
        Crop = Plant.Fruit;
        isGrow = false;
        bornTime = 0;
        growTime = Plant.growTime;
    }

    public void TimeChange()
    {
        if(isWater == true)
        {
            rotTime = 0;
            isWater = false;
            if (isGrow == false) //식물 자라게
            {
                isGrow = true;
                meshFilter.mesh = plant.itemMesh;
                meshRenderer.material = plant.itemMaterilal;
            }
            else
            {
                bornTime++;
                if (bornTime < growTime)
                {
                    return;
                }
                else
                {
                    MakeFruits();
                }
            }
        }
        else
        {
            RotPlant();
        } 
    }
    
    public void MakeFruits()
    {
        ItemManager.Instance.CreateSceneItem(crop, transform.position + new Vector3 (0, 1f,0f));
        ItemReturn();
    }

    IEnumerator SettingDelay()
    {
        yield return new WaitForSeconds(0.1f);
        FirstSetting();
    }

    public void ItemReturn()
    {
        ItemReset();
        home.Return(this.gameObject);
    }

    //시간이 지나면 식물이 썩는 함수
    public void RotPlant()
    {
        rotTime++;
        if(rotTime > Plant.discardTime)
        {
            ItemReturn();
        }
    }

    protected void ItemReset()
    {
        CropManager.Instance.timeChange -= TimeChange;
        growTime = 0;
        bornTime = 0;
        rotTime = 0;
        isGrow = false;
        meshFilter.mesh = null;
        meshRenderer.material = null;
    }

}
