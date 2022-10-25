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
        GameObject uiPhone;
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
            lineRenderer.enabled = false;
        }

        private void Update()
        {
            //if(Input.GetMouseButton(1))
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                Time.timeScale = 0.1f;
                if(Physics.Raycast(leftHand.position, leftHand.forward, out movementHit, Mathf.Infinity, groundMask))
                {
                    lineRenderer.enabled = true;
                    linePositions[0] = transform.position + new Vector3(0,+0.75f,0);
                    movePointObject.SetActive(true);
                    Vector3 dir = movementHit.point -transform.position;
                    objectCharacterController.Move(dir);
                    linePositions[1] = movePointObject.transform.position;
                    lineRenderer.SetPositions(linePositions);
                }
            }
            //if(Input.GetMouseButtonUp(1))
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                Time.timeScale = 1f;
                transform.position = movePointObject.transform.position + new Vector3(0, 0.75f, 0);
                movePointObject.SetActive(false);
                lineRenderer.enabled = false;

            }
            if(OVRInput.GetDown(OVRInput.Button.One))
            {
                uiPhone.SetActive(uiPhone.activeSelf);
            }


        }

   

    }



}
