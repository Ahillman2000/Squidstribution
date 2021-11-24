﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToScreen : MonoBehaviour
{
    [SerializeField] GameObject player;
    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] float explosionForce = 100;
    [SerializeField] float explosionRadius = 100;
    [SerializeField] float upwardsModifier = 1;

    void Start()
    {
        anim  = player.GetComponent<Animator>();
        agent = player.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                GameObject hitObject = hit.transform.gameObject;

                Vector3 newTargetPos = hit.point;
                agent.SetDestination(newTargetPos);
                /// if cannot reach target position then stay at current position

                if (hitObject.CompareTag("Damagable"))
                {
                    if (Vector3.Distance(player.transform.position, newTargetPos) <= 10f)
                    {
                        anim.SetInteger("AttackIndex", Random.Range(0, 3));
                        anim.SetTrigger("Attack");
                        Building buildingScript = hitObject.GetComponent<Building>();
                        buildingScript.TakeDamage(100);
                    }
                }

                if (hitObject.CompareTag("Destructable") && agent.remainingDistance <= 5f)
                {
                    Rigidbody hitObjectRB = hitObject.GetComponent<Rigidbody>();
                    hitObjectRB.AddExplosionForce(explosionForce, player.transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
                }

                if (hitObject.CompareTag("Attackable"))
                {
                    
                }

                if (hitObject.CompareTag("Nestable"))
                {

                }
            }
        }

        if(agent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}