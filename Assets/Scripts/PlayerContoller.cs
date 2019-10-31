using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerContoller : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public NavMeshAgent agent;
    Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                anim.SetBool("Moving", true);
                agent.SetDestination(hit.point);
            }
        }

        if(agent.remainingDistance <= 0.5f)
        {
            anim.SetBool("Moving", false);
        } else
        {
            anim.SetBool("Moving", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = start;
    }
}
