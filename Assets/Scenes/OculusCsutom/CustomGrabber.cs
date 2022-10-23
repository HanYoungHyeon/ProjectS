using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabber : MonoBehaviour
{
    public enum GRABBER_MODE
    {
        DEFAULT, TOGGLE
    }

    [SerializeField]
    private GameObject currentGrabbableObj;                      //트리거 충돌 일어난 ICustomGrabbable이 있는 게임오브젝트
    public GRABBER_MODE mode = GRABBER_MODE.DEFAULT;            //모드 정하기 (토글이면 다시 누르기전까지 잡고있음)
    public KeyCode grabKey = KeyCode.Space;
    private void Update()
    {
        if (currentGrabbableObj == null)
            return;


        switch (mode)
        {

            case GRABBER_MODE.DEFAULT:
                if (Input.GetKeyDown(grabKey))
                {
                    if (currentGrabbableObj.GetComponent<ICustomGrabbable>().IsGrabed == false)      //잡는중이 아니면,
                        currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabBegin(gameObject); //잡기 시작
                    else
                        currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabEnd(gameObject);   //잡는중이면, 놓는다.
                }
                break;


            case GRABBER_MODE.TOGGLE:
                if (Input.GetKey(grabKey))
                    currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabBegin(gameObject);     //누르고있는 동안만 잡는다.
                else
                    currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabEnd(gameObject);
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        ICustomGrabbable tempGrabbable = other.GetComponent<ICustomGrabbable>();    //ICustomGrabbable이 있을때만 처리
        Debug.Log(other.gameObject);
        if (tempGrabbable == null) return;

        currentGrabbableObj = other.gameObject;            //ICustomGrabbable이 있으면 잡은오브젝트에 추가.

    }
    public void OnTriggerExit(Collider other)
    {
        ICustomGrabbable tempGrabbable = other.GetComponent<ICustomGrabbable>();
        if (tempGrabbable == null) return;

        currentGrabbableObj = null;                         //충돌에서 벗어나면 놓는다.
    }
}
