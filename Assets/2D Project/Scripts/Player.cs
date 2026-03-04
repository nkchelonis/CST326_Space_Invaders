
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    
    public AudioClip shotPop;
    public float speed = 5f;
    
    private Animator _animator;
    private AudioSource _audioSource;

    void Start()
    {
        // get and cache animator
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            Debug.Log("Bang!");

            //destroy the bullet after 3 seconds
            Destroy(shot, 3f); //auto destroy after 3 seconds
            
            // trigger shoot sound
            _audioSource.PlayOneShot(shotPop);
            
            //_animator.SetTrigger("Shot Trigger");
        }

        //move player left and right
        float deltaX = speed*Time.deltaTime;
        
        if (Keyboard.current != null && Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Translate(-deltaX, 0, 0);
        }
        else if (Keyboard.current != null && Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Translate(deltaX, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet"))
        {
            //deactivate player and destroy the bullet
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
