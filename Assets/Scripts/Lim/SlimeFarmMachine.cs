using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimeFarmMachine : MonoBehaviour
{
    [SerializeField]
    private Farm farm;
    [SerializeField]
    private List<GameObject> upgradeObject;
    [SerializeField]
    private GameObject insideObject;
    public GameObject InsideObject { get { return insideObject; } set { insideObject = value; } }

    public LayerMask playerLayer;

    public void HighWallUpgrade()
    {
        upgradeObject[0].transform.position = upgradeObject[0].transform.position + Vector3.up*2;
    }
    public void AirNetUpgrade()
    {
        upgradeObject[1].SetActive(true);
    }
    public void AutoGemUpgrade()
    {
        upgradeObject[2].SetActive(true);
    }
    public void AutoFeedUpgrade()
    {
        upgradeObject[3].SetActive(true);
    }
    public void MusicBoxUpgrade()
    {
        upgradeObject[4].SetActive(true);
    }
    public void StartFunction(string name)
    {
        Invoke(name, 0.1f);
    }
}
