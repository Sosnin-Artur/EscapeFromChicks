using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{    
    [SerializeField] private float speed = 10.0f;  
    
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);             
    }
    
    private void OnBecameInvisible()
    {                
        Destroy(gameObject);
    }    
}
