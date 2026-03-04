using System;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    private int _health = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            //decrease health of barrier and destroy the bullet
            _health--;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet"))
        {
            //deactivate player and destroy the bullet
            _health--;
            Destroy(other.gameObject);
        }
    }
}
