using UnityEngine;

public class EnemyRootController : MonoBehaviour
{
    public Transform enemyRoot;

    public GameObject enemyLow;
    public GameObject enemyMedium;
    public GameObject enemyHigh;

    private float _enemySpeed;
    private float _currentTime = 0;
    private float _direction = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetEnemies();
        Enemy.OnEnemyHitWall += OnEnemyHitWall;

    }

    // Update is called once per frame
    void Update()
    {
        //hopefully moves enemies in steps
        _currentTime += Time.deltaTime;
        if ((int)_currentTime == 1)
        {
            _currentTime = 0;
            MoveEnemies();
        }

    }
    
    void OnDestroy()
    {
        Enemy.OnEnemyHitWall -= OnEnemyHitWall;
    }

    void SpawnEnemies()
    {
        float xCoordinate, yCoordinate;
        //spawn 11 top enemies
        for (int topX = 0; topX < 11; topX++)
        {
            xCoordinate = enemyRoot.position.x + topX;
            yCoordinate = enemyRoot.position.y;
            Transform tempEnemy = Instantiate(enemyHigh, enemyRoot).transform;
            Vector2 tempPosition = new Vector2(xCoordinate, yCoordinate);
            tempEnemy.SetPositionAndRotation(tempPosition, Quaternion.identity);
        }
        
        //spawn 22 middle enemies
        for (int midX = 0; midX < 11; midX++)
        {
            xCoordinate = enemyRoot.position.x + midX;
            yCoordinate = enemyRoot.position.y - 1;
            
            Transform tempEnemy = Instantiate(enemyMedium, enemyRoot).transform;
            Vector2 tempPosition = new Vector2(xCoordinate, yCoordinate);
            tempEnemy.SetPositionAndRotation(tempPosition, Quaternion.identity);
        }
        for (int midX = 0; midX < 11; midX++)
        {
            xCoordinate = enemyRoot.position.x + midX;
            yCoordinate = enemyRoot.position.y - 2;
            
            Transform tempEnemy = Instantiate(enemyMedium, enemyRoot).transform;
            Vector2 tempPosition = new Vector2(xCoordinate, yCoordinate);
            tempEnemy.SetPositionAndRotation(tempPosition, Quaternion.identity);
        }
        
        //spawn 22 low enemies
        for (int midX = 0; midX < 11; midX++)
        {
            xCoordinate = enemyRoot.position.x + midX;
            yCoordinate = enemyRoot.position.y - 3;
            
            Transform tempEnemy = Instantiate(enemyLow, enemyRoot).transform;
            Vector2 tempPosition = new Vector2(xCoordinate, yCoordinate);
            tempEnemy.SetPositionAndRotation(tempPosition, Quaternion.identity);
        }
        for (int midX = 0; midX < 11; midX++)
        {
            xCoordinate = enemyRoot.position.x + midX;
            yCoordinate = enemyRoot.position.y - 4;
            
            Transform tempEnemy = Instantiate(enemyLow, enemyRoot).transform;
            Vector2 tempPosition = new Vector2(xCoordinate, yCoordinate);
            tempEnemy.SetPositionAndRotation(tempPosition, Quaternion.identity);
        }
        
    }

    void ResetEnemies()
    {
        foreach (Transform child in enemyRoot)
            Destroy(child.gameObject);

        SpawnEnemies();
    }

    void MoveEnemies()
    {
        float deltaX = transform.position.x + _direction;
        float deltaY = transform.position.y;

        //hard coded swaps for direction :/
        if (transform.position.x == -2.5f || transform.position.x == -8.5f)
        {
            _direction *= -1;
            deltaY -= .25f;
        }
        
        transform.position = new Vector3(deltaX, deltaY, 0);
    }

    void OnEnemyHitWall()
    {
        Debug.Log("Changing direction!");
        _direction *= -1;  //swaps enemy direction
        float deltaX = transform.position.x;
        float deltaY = transform.position.y - 1;  //moves the enemies down one
        transform.position = new Vector3(deltaX, deltaY, 0);
    }
}
