using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void Play()
    {
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Gameplay");
    }
}
