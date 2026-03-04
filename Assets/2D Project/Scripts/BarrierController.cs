using System;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    
    private int _health = 5;
    private SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
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
            UpdateColor();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet"))
        {
            //deactivate player and destroy the bullet
            _health--;
            UpdateColor();
            Destroy(other.gameObject);
        }
    }

    private void UpdateColor()
    {
        if (_health == 4)
        {
            _spriteRenderer.color = Color.gray7;
        }
        else if (_health == 3)
        {
            _spriteRenderer.color = Color.gray5;
        }
        else if (_health == 2)
        {
            _spriteRenderer.color = Color.gray4;
        }
        else if (_health == 1)
        {
            _spriteRenderer.color = Color.gray3;
        }
    }
}
