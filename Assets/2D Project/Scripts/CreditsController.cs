using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    private float _creditsTimer = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _creditsTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //decrease time for credits to show
        if ((int)_creditsTimer > 0)
        {
            _creditsTimer -= Time.deltaTime;
        }
        if ((int)_creditsTimer == 0)
        {
            _creditsTimer = -1f;
            LoadTitle();
        }
    }
    
    public void LoadTitle()
    {
        StartCoroutine(_LoadTitle());
        IEnumerator _LoadTitle()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("FaceInvaders");
            while(!loadOperation!.isDone) yield return null; 
        }
        
    }
}
