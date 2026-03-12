
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDiedFunc(int points);
    public static event EnemyDiedFunc OnEnemyDied;

    public delegate void EnemyHitWallFunc();
    public static event EnemyHitWallFunc OnEnemyHitWall;

    public AudioClip ticClip;
    public AudioClip tacClip;
    public AudioClip shootClip;
    public AudioClip corkPopClip;

    public int scoreValue;
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    
    private AudioSource _audioSource;
    private Animator _animator;
    

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        //EnemyRootController.OnEnemyMoved += OnEnemyMoved;
    }

    void Update()
    {
        //randomly shoot bullets
        if (Random.Range(1, 7500) == 5)
        {
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            _audioSource.PlayOneShot(shootClip);
            _animator.SetTrigger("Shot Trigger");
            
            //destroy the bullet after 3 seconds
            Destroy(shot, 3f); //auto destroy after 3 seconds
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        
        // destroy the bullet
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(collision.gameObject); //destroys bullet, doesn't need to be here if bullet is destroyed by itself
            // trigger death animation
            GetComponent<AudioSource>().PlayOneShot(corkPopClip);
            _animator.SetTrigger("Death Trigger");
            
            //send out message that an enemy has died
            OnEnemyDied?.Invoke(scoreValue);  //? says if it's null then don't Invoke
        }
        
    }

    void EnemyDied()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("That's a wall!");
            OnEnemyHitWall?.Invoke();
        }
    }

    public void PlayTicSound()
    {
        //Debug.Log("tic");
        GetComponent<AudioSource>().PlayOneShot(ticClip);
    }

    public void PlayTacSound()
    {
        //Debug.Log("tac");
        GetComponent<AudioSource>().PlayOneShot(tacClip);
    }

    
    /*
    void OnEnemyMoved()
    {
        if (_animator.GetBool("Arms Up"))
        {
            _animator.SetBool("Arms Up", false);
        }
        else
        {
            _animator.SetBool("Arms Up", true);
        }
    }*/
}
