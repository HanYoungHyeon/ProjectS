using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BC
{
    public class Player : OVRPlayerController
    {
        [SerializeField]
        GameObject movePointObject;
        [SerializeField]
        LayerMask groundMask;
        [SerializeField]
        LayerMask uiMask;
        [SerializeField]
        float uiInteractionDistance;
        [SerializeField]
        Transform leftHand;
        [SerializeField]
        CharacterController objectCharacterController;

        RaycastHit movementHit;
        Ray moveRay;
        RaycastHit uiHit;
        Ray interactionRay;

        LineRenderer lineRenderer;
        Vector3[] linePositions = new Vector3[2];

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            //interactionRay = cameraMain.ScreenPointToRay(transform.forward*5);
            //Physics.Raycast(interactionRay, out uiHit, uiInteractionDistance, uiMask);
            if (Input.GetMouseButton(1))
            {
                Time.timeScale = 0.1f;
                Debug.DrawRay(leftHand.position, leftHand.forward * 10);
                if(Physics.Raycast(leftHand.position, leftHand.forward, out movementHit, Mathf.Infinity, groundMask))
                //if (Physics.Raycast(moveRay, out movementHit, Mathf.Infinity, groundMask))
                {
                    linePositions[0] = transform.position + new Vector3(0,+0.75f,0);
                    movePointObject.SetActive(true);
                    Vector3 dir = movementHit.point - movePointObject.transform.position;
                    objectCharacterController.Move(dir);
                    linePositions[1] = movePointObject.transform.position;
                    lineRenderer.SetPositions(linePositions);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                Time.timeScale = 1f;
                transform.position = movePointObject.transform.position;
                movePointObject.SetActive(false);
            }
        }

   

    }



}
