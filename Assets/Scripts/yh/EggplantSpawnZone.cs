using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggplantSpawnZone : MonoBehaviour
{
    [SerializeField]
    LayerMask groundMask;
    public ObjectPoolCall objectPoolCall;
    Vector3 hitPosition;
    Vector3 randomPosition;
    float range_x;
    float range_z;
    void Update()
    {
        range_x = Random.Range(transform.position.x, transform.position.x + 10);
        range_z = Random.Range(transform.position.z, transform.position.z + 10);
        randomPosition = new Vector3(range_x, transform.position.y, range_z);
        Ray ray = new Ray(randomPosition, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, groundMask);
        hitPosition = hit.point;
        ////hitPosition.y += 0.25f;
        //objectPoolCall.eggplantPosition = hitPosition;
    }
}
