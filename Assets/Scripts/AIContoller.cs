using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIContoller : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    bool moving;
    bool waiting;
    float waitTime;
    GameObject[] waypoints;
    int curWaypoint;
    Ray ray;
    RaycastHit hit;
    float rayDist = 15.0f;
    bool chasing;
    float attention;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        waiting = true;
        waitTime = Random.Range(0.5f, 1.5f);
        chasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            attention -= Time.deltaTime;
            if(attention <= 0.0f)
            {
                chasing = false;
                anim.SetBool("Alert", false);
                anim.SetBool("Walking", true);
                ChangeWaypoint();
                agent.speed = 0.8f;
            }
        }
        RayUpdate();
        if(moving && agent.remainingDistance<= 0.5f)
        {
            moving = false;
            anim.SetBool("Walking", false);
            waiting = true;
            waitTime = Random.Range(0.5f, 5.0f);
        } else if (waiting)
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0.0f)
            {
                waiting = false;
                ChangeWaypoint();
            }
        }
    }
    void RayUpdate()
    {
        ray = new Ray(transform.position + new Vector3(0f, 1.0f, 0f), transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (hit.distance < rayDist)
                {
                    Chase();
                }
            }
        }
    }

    void Chase()
    {
        agent.SetDestination(hit.collider.gameObject.transform.position);
        anim.SetBool("Alert", true);
        chasing = true;
        attention = 5.0f;
        agent.speed = 5;
    }

    void ChangeWaypoint()
    {
        curWaypoint++;
        if (curWaypoint >= waypoints.Length)
        {
            curWaypoint = 0;
        }
        agent.SetDestination(waypoints[curWaypoint].transform.position);
        moving = true;
        anim.SetBool("Walking", true);
    }
}
