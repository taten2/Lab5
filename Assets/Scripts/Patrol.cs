using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachineBehaviour
{
    GameObject enemy;
    GameObject[] waypoints;
    int currentWaypoint;
    public NavMeshAgent agent;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        currentWaypoint = 0;
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waypoints.Length == 0) return;
        if(Vector3.Distance(waypoints[currentWaypoint].transform.position, enemy.transform.position) < 2.0f)
        {
            currentWaypoint++;
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        agent.SetDestination(waypoints[currentWaypoint].transform.position);
        /*var direction = waypoints[currentWaypoint].transform.position - enemy.transform.position;
        //enemy.transform.rotation = Quaternion.Slerp()
        enemy.trans*/

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
