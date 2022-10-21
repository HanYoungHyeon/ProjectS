using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPllay : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;
    float moveX;
    float moveZ;
    string ver = "Vertical";
    string ho = "Horizontal";


    private void FixedUpdate()
    {
        moveX = Input.GetAxis(ho);
        moveZ = Input.GetAxis(ver);
        characterController.Move(new Vector3 (moveX,0,moveZ)*Time.fixedDeltaTime*1);


    }
}
