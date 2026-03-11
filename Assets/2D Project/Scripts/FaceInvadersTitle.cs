using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceInvadersTitle : MonoBehaviour
{
    private float _timer = 10f;

    void Start()
    {
        _timer = 10f;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //decrease time for score table to show
        if ((int)_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        if ((int)_timer == 0)
        {
            _timer = -1f;
            LoadGame();
        }
    }

    public void LoadGame()
    {
        _timer = -1f;
        StartCoroutine(_LoadGame());
        IEnumerator _LoadGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Schmup");
            while(!loadOperation!.isDone) yield return null;        
        }
        
    }
}
