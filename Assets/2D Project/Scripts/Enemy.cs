
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

    public int scoreValue;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        
        // destroy the bullet
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(collision.gameObject); //destroys bullet, doesn't need to be here if bullet is destroyed by itself
            // todo - trigger death animation
            Destroy(gameObject);  //this should be after death animation plays
            
            //send out message that an enemy has died
            OnEnemyDied?.Invoke(scoreValue);  //? says if it's null then don't Invoke
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("That's a wall!");
            //OnEnemyHitWall?.Invoke();
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
}
