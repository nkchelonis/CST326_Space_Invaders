

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    
    public AudioClip shotPop;
    public AudioClip backgroundMusic;
    public AudioClip deathPop;
    public float speed = 5f;
    
    private Animator _animator;
    private AudioSource _audioSource;

    void Start()
    {
        // get and cache animator
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        _audioSource.PlayOneShot(backgroundMusic);
    }
    
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            //Debug.Log("Bang!");

            //destroy the bullet after 3 seconds
            Destroy(shot, 3f); //auto destroy after 3 seconds
            
            // trigger shoot sound
            _audioSource.PlayOneShot(shotPop);
            _animator.SetTrigger("Shot Trigger");
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
            Destroy(other.gameObject);
            _animator.SetTrigger("Death Trigger");
            _audioSource.PlayOneShot(deathPop);
            StartCoroutine(PlayerDied());
        }
    }

    IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(1f);
        
        //gameObject.SetActive(false);
    }

    public void LoadCredits()
    {
        StartCoroutine(_LoadCredits());
        IEnumerator _LoadCredits()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Credits");
            while(!loadOperation!.isDone) yield return null;        
            
        }
    }
}
