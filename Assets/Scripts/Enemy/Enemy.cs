using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int cost = 10;        
    [SerializeField] private int damage = 10;        
    [SerializeField] private int health = 10;    
    [SerializeField] private float knockbackForce = 10.0f;    

    [SerializeField] private Rigidbody rigidbody; 
    [SerializeField] private Slider healthBar;     
    
    private int _health;
    
    public void TakeDamage(int value)
    {
        healthBar.gameObject.SetActive(true);
        health -= value;
        if (health <= 0)
        {     
            Manager.Player.ChangeScore(cost);    
            Destroy(gameObject);
        }
        
        healthBar.value = ((float)health / _health);
    }
    
    private void Awake()
    {        
        healthBar.gameObject.SetActive(false);
        _health = health;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Manager.Player.ChangeHealth(-damage);
            Vector3 direction = transform.position - collision.gameObject.transform.position + new Vector3(0, 1, 0);

            rigidbody.AddForce(direction * knockbackForce,ForceMode.Impulse);
        }
    }    
}
