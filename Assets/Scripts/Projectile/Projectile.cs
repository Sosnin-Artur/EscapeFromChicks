using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 5;                  
    [SerializeField] private int ricochetCount = 1;   
    
    [SerializeField] private Collider projectileCollider;               

    private int _currentRicochetCount = 0;

    private void Awake()
    {                        
        projectileCollider.isTrigger = true;
        StartCoroutine(EnableCollision());
    }
    
    private IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.1f);
        projectileCollider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);            
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {            
            if (_currentRicochetCount < ricochetCount)
            {                
                damage /= 2;

                Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                transform.rotation = Quaternion.LookRotation(newDirection);
                _currentRicochetCount++;
            }   
            else
            {
                Destroy(gameObject);
            }         
        }
    }
}
