using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceInvadersTitle : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadGame()
    {
        StartCoroutine(_LoadGame());
        IEnumerator _LoadGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Schmup");
            while(!loadOperation!.isDone) yield return null;        
            
            //wait until scene is loaded and ready, then find the player
            GameObject playerObj = GameObject.Find("Player");
            Debug.Log(playerObj.name);
        }
        
    }
}
