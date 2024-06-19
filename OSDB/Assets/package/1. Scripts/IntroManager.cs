using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public void ClickStart()
    {
        SceneManager.LoadScene("Main");
    }
    public void ClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        
#else 
Application.Quit();
#endif 
    }
}
