using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFarm : MonoBehaviour
{
    private bool highWallUpgrade = false;
    private bool airNetUpgrade = false;
    private bool musicBoxUpgrade = false;
    private bool autoGemUpgrade = false;
    private bool autoFeedUpgrade = false;
    [SerializeField]
    private GameObject insideObject;
    public GameObject InsideObject { get { return insideObject; } set { insideObject = value; } }
    [SerializeField]
    private GameObject upgradeUI;
    public GameObject UpgradeUI { get { return upgradeUI; } set { upgradeUI = value; } }

    public LayerMask playerLayer;

    private void Start()
    {

        //슬라임 우리로 변경함수 추가
    }

    private void Update()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, 20f, playerLayer);
        
        if (target.Length>0)
        {
            upgradeUI.SetActive(true);
        }
        else
        {
            upgradeUI.SetActive(false);
        }
        //업그레이드 확인 함수 추가
    }
    //업그레이드 퀘스트 다이얼로그 관련 추가
}
