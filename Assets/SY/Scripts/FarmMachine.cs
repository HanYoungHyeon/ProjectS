using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FarmMachine : MonoBehaviour
{
    //업그레이드 : 스프링쿨러, 비료, 작물 제거, 비옥한 토양, 기적의 비료
    [SerializeField] LayerMask cropMask;
    public Vector3 machineHeight;
    public float detectRange;
    private bool isPlanted = false;
    [SerializeField]
    public List<Transform> plantArea = new List<Transform>();
    private float growTime;
    private float bornTime;
    private float rotTime;
    private bool isWater;
    public ScenePlant[] childPlant;
    [SerializeField]
    private GameObject sprinkler;
    public Crop tempCrop;
    

    private void Start()
    {
        ItemManager.Instance.CreateSceneItem(tempCrop, new Vector3(7f, 23f, -28f));
    }

    private void Update()
    {
        DetectCrop();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CropManager.Instance.timeChange();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Sprinkler();
        }
    }

    //작물 탐지
    public void DetectCrop()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position + machineHeight, detectRange, cropMask);
        for (int i = 0; i < targets.Length; i++) //레이어 마스크
        {
            if(isPlanted == false)
            {
                SceneCrop target = targets[0].transform.GetComponent<SceneCrop>();
                if (target != null)
                {
                    Plant(target);
                    growTime = target.Plant.growTime;
                    StartGrow();
                    target.ItemReturn();
                    isPlanted = true;
                }
            }
            else
            {
                return;
            }
            
        }
    }
    //처음 심기 시작했을 때 시간지나는 함수 액션에 추가
    public void StartGrow()
    {
        bornTime = 0;
        CropManager.Instance.timeChange += TimeChange;
    }

    //작물이 자라는 일정 시간이 지날 때 마다 실행시킬 함수
    public void TimeChange()
    {
        bornTime++;
        if (bornTime < growTime)
        {
            return;
        }
        else
        {
            EndGrow();
        }
    }
    public void EndGrow()
    {
        CropManager.Instance.timeChange -= TimeChange;
        isPlanted = false;
        bornTime = 0;
        growTime = 0;
    }

    //작물 심는 함수
    public void Plant(SceneCrop crop)
    {
        if (isPlanted == false)
        {
            for (int i = 0; i < plantArea.Count - crop.Plant.plantCount; i++)
            {
                int randomIndex = Random.Range(0, plantArea.Count);
                plantArea.RemoveAt(randomIndex);
            }

            for (int i = 0; i < crop.Plant.plantCount; i++)
            {
                ItemManager.Instance.CreateScenePlant(crop.Plant, plantArea[i].position).parent = gameObject.transform;
            }
            childPlant = GetComponentsInChildren<ScenePlant>();
            isPlanted = true;
        }

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + machineHeight, detectRange);
    }

    public void StartFunction(string func)
    {
        Invoke(func, 0.1f);
    }

    //작물 제거 10G
    public void DeleteCrop()
    {
        Debug.Log("작물 제거");
        for(int i = 0; i < childPlant.Length; i++)
        {
            childPlant[i].ItemReturn();
        }
        childPlant = null;
    }

    //스프링클러(자동으로 물주는 거) : 500G
    public void Sprinkler()
    {
        Debug.Log("자동 물주기");
        if(childPlant != null)
        {
            AutoWater();
            for (int i = 0; i < childPlant.Length; i++)
            {
                CropManager.Instance.timeChange -= childPlant[i].TimeChange;
            }
            CropManager.Instance.timeChange += AutoWater;
            for (int i = 0; i < childPlant.Length; i++)
            {
                CropManager.Instance.timeChange += childPlant[i].TimeChange;
            }
        }
        else
        {
            CropManager.Instance.timeChange += AutoWater;
        }
        
    }

    //기적의 비료(썩는 데 걸리는 시간 추가) : 500G
    public void Fertilizer()
    {
        for (int i = 0; i < childPlant.Length; i++)
        {
           // childPlant[i].isWater = true;
        }
    }

    //비옥한 토양(빨리 자라게) : 300G
    public void FertileSoil()
    {
        for (int i = 0; i < childPlant.Length; i++)
        {
            //childPlant[i].isWater = true;
        }
    }

    //나오는 열매 개수 증가 : 500G
    public void MoreFruits()
    {
        for (int i = 0; i < childPlant.Length; i++)
        {
            //childPlant[i].isWater = true;
        }
    }

    public void AutoWater()
    {
        if (childPlant != null)
        {
            for (int i = 0; i < childPlant.Length; i++)
            {
                childPlant[i].isWater = true;
            }
        }
        else
        {
            return;
        }
    }

    //public enum Event { Sprinkler, Fertilizer, Fertilizer, FertileSoil, MoreFruit, AutoWater }
}
