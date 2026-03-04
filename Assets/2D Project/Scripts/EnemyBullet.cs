using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * speed;
        Debug.Log("mwuahahaha");
    }
}
