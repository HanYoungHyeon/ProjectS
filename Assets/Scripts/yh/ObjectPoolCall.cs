using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCall : MonoBehaviour
{
    [SerializeField]
    GameObject slime;
    [SerializeField]
    GameObject carrot;
    [SerializeField]
    GameObject corn;
    [SerializeField]
    GameObject eggplant;
    [SerializeField]
    GameObject pumpkin;
    [SerializeField]
    GameObject tomato;
    ObjectPool objectPool;
    ObjectPool carrotPool;
    ObjectPool cornPool;
    ObjectPool eggplantPool;
    ObjectPool pumpkinPool;
    ObjectPool tomatoPool;

    [SerializeField]
    List<GameObject> slimes = new List<GameObject>();
    [SerializeField]
    List<GameObject> plants = new List<GameObject>();
    public Vector3 spawnPosition;
    public Vector3 carrotPosition;
    public Vector3 cornPosition;
    public Vector3 eggplantPosition;
    public Vector3 pumpkinPosition;
    public Vector3 tomatoPosition;
    [SerializeField]
    int maxSlimeAmount;
    public int curSlimeAmount;
    [SerializeField]
    int maxPlantAmount;
    public int curPlantAmount;

    private void Awake()
    {
        curSlimeAmount = 0;
        maxSlimeAmount = 15;
        curPlantAmount = 0;
        maxPlantAmount = 3;
        objectPool = ObjectPoolManager.Instance.PoolRequest(slime, maxSlimeAmount, 0);
        carrotPool = ObjectPoolManager.Instance.PoolRequest(carrot, maxPlantAmount, 0);
        cornPool = ObjectPoolManager.Instance.PoolRequest(corn, maxPlantAmount, 0);
        eggplantPool = ObjectPoolManager.Instance.PoolRequest(eggplant, maxPlantAmount, 0);
        pumpkinPool = ObjectPoolManager.Instance.PoolRequest(pumpkin, maxPlantAmount, 0);
        tomatoPool = ObjectPoolManager.Instance.PoolRequest(tomato, maxPlantAmount, 0);
    }
    private void Start()
    {
        StartCoroutine(CheckSlime());
    }

    IEnumerator CheckSlime()
    {
          while (true)
         {
            if (curSlimeAmount < maxSlimeAmount)
            {
                yield return new WaitForSeconds(1f);
                objectPool.Call(spawnPosition, Quaternion.identity).TryGetComponent(out BC.SceneSlime callSlime);
                slimes.Add(callSlime.gameObject);
                callSlime.returnAcition -= WhenDieSlime;
                callSlime.returnAcition += WhenDieSlime;
                curSlimeAmount++;
            }
            else if (curSlimeAmount > maxSlimeAmount)
            {
                foreach (var go in slimes)
                {
                    go.transform.TryGetComponent(out BC.SceneSlime returnSlime);
                    returnSlime.ItemReturn();
                }
            }
            if (curPlantAmount < maxPlantAmount)
            {
                yield return new WaitForSeconds(1f);
                plants.Add(carrotPool.Call(carrotPosition, Quaternion.identity).gameObject);
                plants.Add(cornPool.Call(cornPosition, Quaternion.identity).gameObject);
                plants.Add(eggplantPool.Call(eggplantPosition, Quaternion.identity).gameObject);
                plants.Add(pumpkinPool.Call(pumpkinPosition, Quaternion.identity).gameObject);
                plants.Add(tomatoPool.Call(tomatoPosition, Quaternion.identity).gameObject);
                curPlantAmount += 5;
            }
        }
      }


    public void WhenDieSlime()
    {
        curSlimeAmount--;
    }
    /*private void Temp(BC.SceneSlime sceneSlime)
    {
        Debug.Log("실행 되나?");
        sceneSlime.returnAcition -= WhenDieSlime;
        sceneSlime.returnAcition += WhenDieSlime;

    }*/
}
