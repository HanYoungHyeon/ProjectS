using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField]
    private Slime[] slimes;
    [SerializeField]
    private LayerMask groundMask;
    private List<GameObject> slimeList = new List<GameObject>();
    private Vector3 hitPosition;
    private Vector3 randomPosition;
    private float range_x;
    private float range_z;
    private int curSlimeAmount;
    private int maxSlimeAmount;
    private WaitForSeconds oneSecond = new WaitForSeconds(0.1f);
    private RaycastHit hit;
    private WaitUntil slimeCount;
    private WaitForSeconds wait = new WaitForSeconds(5f);
 
    private void Awake()
    {
        slimeCount = new WaitUntil(() => curSlimeAmount < maxSlimeAmount);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return slimeCount;
            ItemManager.Instance.CreateSceneItem(slimes[Random.Range(0, slimes.Length)], PointSet()).TryGetComponent(out BC.SceneSlime callSlime);
            slimeList.Add(callSlime.gameObject);
            callSlime.returnAcition -= WhenDieSlime;
            callSlime.returnAcition += WhenDieSlime;
            curSlimeAmount++;
            yield return oneSecond;
        }
    }
    private Vector3 PointSet()
    {
        range_x = Random.Range(transform.position.x, transform.position.x + 10);
        range_z = Random.Range(transform.position.z, transform.position.z + 10);
        randomPosition = new Vector3(range_x, transform.position.y, range_z);
        Physics.Raycast(randomPosition, Vector3.down, out hit, groundMask);
        hitPosition = hit.point;
        hitPosition.y += 0.25f;
        return hitPosition;
    }

    public void WhenDieSlime()
    {
        curSlimeAmount--;
    }

}
