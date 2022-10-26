using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCall : MonoBehaviour
{

    [SerializeField]
    private int maxPlantAmount;
    private int curPlantAmount;
    WaitForSeconds oneSecond = new WaitForSeconds(1f);
    WaitUntil plantCount;
    [SerializeField]
    private Plant[] plantData;
    [SerializeField]
    LayerMask groundMask;
    Vector3 hitPosition;
    Vector3 randomPosition;
    float range_x;
    float range_z;


    private void Awake()
    {
        plantCount = new WaitUntil(() => curPlantAmount < maxPlantAmount);
    }
  
    public Vector3 SpawnPosition()
    {
        range_x = Random.Range(transform.position.x, transform.position.x + 10);
        range_z = Random.Range(transform.position.z, transform.position.z + 10);
        randomPosition = new Vector3(range_x, transform.position.y, range_z);
        Ray ray = new Ray(randomPosition, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, groundMask);
        hitPosition = hit.point;
        return hitPosition;
    }
    private IEnumerator Start()
    {
        while (true)
        {
            yield return plantCount;
            ItemManager.Instance.CreateScenePlant(plantData[Random.Range(0, plantData.Length)], SpawnPosition());
            curPlantAmount++;
            yield return oneSecond;
        }
    }

    /*private void Temp(BC.SceneSlime sceneSlime)
    {
        Debug.Log("실행 되나?");
        sceneSlime.returnAcition -= WhenDieSlime;
        sceneSlime.returnAcition += WhenDieSlime;

    }*/
}
