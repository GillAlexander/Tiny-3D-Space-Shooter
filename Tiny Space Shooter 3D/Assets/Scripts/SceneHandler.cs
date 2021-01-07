
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public int campainScene;
    public int level1;

    //private void OnEnable()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}

    public void Campain()
    {
        SceneManager.LoadScene(campainScene);
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1);
    }
}
