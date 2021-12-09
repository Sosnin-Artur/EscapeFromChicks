using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{       
    [SerializeField] private NavMeshAgent agent;
    
    private Transform _target;    

    private void Start()
    {
        _target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {        
        agent.destination =  _target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {            
            Destroy(gameObject);
        }
    }
}
