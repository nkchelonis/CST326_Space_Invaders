using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRootController : MonoBehaviour
{
   // public delegate void EnemyMovedFunc();
   // public static event EnemyMovedFunc OnEnemyMoved;
    
    
    public Transform enemyRoot;

    public GameObject enemyLow;
    public GameObject enemyMedium;
    public GameObject enemyHigh;
    public float speed = 1.0f;

    private float _enemySpeed;
    private float _currentTime = 0;
    private float _direction = 1;
    private bool _wallShift = false;
    
    
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

        int enemyCount = 0;
        foreach (Transform child in enemyRoot)
            enemyCount++;

        if (enemyCount != 0)  //making sure not to divide by 0
        {
            float enemySpeedBoost = (1f / enemyCount)*10;  //fewer enemies -> larger fraction -> larger boost to speed
            speed = 1 + enemySpeedBoost;
        }

        if (enemyCount == 0)
        {
            LoadCredits();
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
        float deltaX = transform.position.x + _direction*speed;
        float deltaY = transform.position.y;

        if (_wallShift)
        {
            //Debug.Log("Changing direction!");
            _direction *= -1;  //swaps enemy direction
            deltaX = transform.position.x + _direction*speed + _direction;
            deltaY = transform.position.y - .5f;  //moves the enemies down one
            transform.position = new Vector3(deltaX, deltaY, 0);
            _wallShift = false;
        }

        //OnEnemyMoved?.Invoke();
        
        /*
        //hard coded swaps for direction :/
        if (transform.position.x > -2.5f || transform.position.x < -8.5f)
        {
            _direction *= -1;
            deltaX = transform.position.x + _direction*speed;
            deltaY -= .25f;
        }*/
        
        transform.position = new Vector3(deltaX, deltaY, 0);
    }

    void OnEnemyHitWall()
    {
        _wallShift = true;
        
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
