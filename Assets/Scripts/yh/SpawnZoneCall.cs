using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneCall : MonoBehaviour
{
    [SerializeField]
    GameObject mountain;
    [SerializeField]
    GameObject lake;
    [SerializeField]
    GameObject forest;
    [SerializeField]
    GameObject yard;
    private WaitForSeconds wait = new WaitForSeconds(5f);
    void Start()
    {
        StartCoroutine(TurnOn());
    }


    
    IEnumerator TurnOn()
    {
        while(true)
        {
            mountain.SetActive(true);
            yield return wait;
            lake.SetActive(true);
            yield return wait;
            forest.SetActive(true);
            yield return wait;
            yard.SetActive(true);
            yield return wait;
        }
    }
}
