using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;
using TMPro;

namespace BC
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class SceneSlime : MonoBehaviour, IInteraction, IItemable,IPoolingable
    {
        public Slime curSlime;
        private Animator animator;
        private SkinnedMeshRenderer sliemSkin;
        private NavMeshAgent agent;
        public NavMeshAgent Agent { get; private set; }
        public Rigidbody rigi { get; set; }
        public ObjectPool home { get; set; }
        [SerializeField]
        Face slimeFace;
        [SerializeField]
        private LayerMask groundMask;
        private RaycastHit hit;
        [SerializeField]
        private Transform raycastOrigin;
        [SerializeField]
        private LayerMask feedMask;
        [SerializeField]
        private LayerMask vacuumMask;
        private Collider[] feedColliders = new Collider[1];
        private Collider[] vacuumColliders = new Collider[1];
        private float idleCount;
        private bool isFlying;
        private float hungry;
        private UnityAction vaccumCheck;
        public UnityAction returnAcition;


        private void Awake()
        {
            rigi = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            sliemSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void SlimeSet(Slime slime)
        {
            curSlime = slime;
            sliemSkin.sharedMesh = slime.itemMesh;
            sliemSkin.sharedMaterials[0] = slime.itemMaterilal;
            FaceSet(slimeFace.Idleface);
            hungry = 0;
            agent.speed = curSlime.speed;
            SlimeMovement();
        }
        public void FaceSet(Texture tex)
        {
            sliemSkin.materials[1].SetTexture(Parameter.mainTex, tex);
        }
        private void FixedUpdate()
        {
         
            vaccumCheck?.Invoke();
            if (isFlying) return;
            animator.SetFloat(Parameter.speed, agent.velocity.magnitude);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                idleCount += Time.fixedDeltaTime;
                if(idleCount>5f)
                {
                    SlimeMovement();
                    idleCount = 0;
                }
                else
                {
                    if(hungry>300)
                    {
                        FindFeed();
                    }
                    else
                    {
                        SlimeJump();
                    }
                }
            }
        }

        private void SlimeMovement()
        {
            if (isFlying) return;
            FaceSet(slimeFace.jumpFace);
            raycastOrigin.localRotation = Quaternion.Euler(new Vector3(10, Random.Range(0, 360f), 0));
            if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, Mathf.Infinity, groundMask))
            {
                agent.SetDestination(hit.point);
            }
        }

        private void FindFeed()
        {
            FaceSet(slimeFace.damageFace);
            feedColliders[0] = null;
            Physics.OverlapSphereNonAlloc(transform.position, 1f, feedColliders, feedMask);
            if(feedColliders[0] != null)
            {
                if (feedColliders[0].TryGetComponent(out SceneCrop feed))
                {
                    if(feed.Crop==curSlime.likeFeed)
                    {
                        feed.ItemReturn();
                        hungry += 150;
                        CreateGem();
                    }
                }
            }
        }
        private void CreateGem()
        {
            ItemManager.Instance.CreateSceneItem(curSlime.rewardGem, transform.position);
        }
        private void SlimeJump()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash != Parameter.jump)
            {
                if (Random.Range(0, 100) > 98)
                {
                    animator.SetTrigger(Parameter.jump);
                    FaceSet(slimeFace.jumpFace);
                }
            }
        }

        public Item ItemRequest()
        {
            return curSlime;
        }

        public void ItemReturn()
        {
            animator.applyRootMotion = true;
            rigi.isKinematic = true;
            isFlying = false;
            vaccumCheck = null;
            returnAcition?.Invoke();
            home.Return(this.gameObject);
        }

        public void MoveStop()
        {
            isFlying = true;
            rigi.isKinematic = false;
            animator.applyRootMotion = false;
            vaccumCheck = VacuumCheck;
        }
        private void VacuumCheck()
        {
            vacuumColliders[0] = null;
            Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, 0.27f, 0), 0.35f, vacuumColliders, vacuumMask, QueryTriggerInteraction.Collide);
            if (vacuumColliders[0] != null) return;
            animator.applyRootMotion = true;
            if (!agent.isOnNavMesh) return;
            rigi.isKinematic = true;
            isFlying = false;
            vaccumCheck = null;
        }

    }
}

