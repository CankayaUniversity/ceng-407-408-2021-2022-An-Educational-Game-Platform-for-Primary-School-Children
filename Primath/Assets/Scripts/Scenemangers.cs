using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemangers : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetScene()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
    public void LoadNextSceneAgac()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex +7);
    }
    public void LoadNextSceneAgac2()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
    public void LoadMainPage()
    {
         SceneManager.LoadScene(0);
    }
    public void LoadNextSceneLabirent()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 10);
    }
    public void LoadNextSceneLabirent2()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
    public void LoadNextScenesaatpart2()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 10);
    }
    public void LoadNextScenesaatpart3()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
    public void LoadNextScenesaat2part3()
    {
       
        SceneManager.LoadScene(19);
    }
    public void LoadNextScenesaat2part2()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(18);
    }
    
    public void LoadNextScenesaatpart3tosaat2()
    {
   
        SceneManager.LoadScene(20);
    }
}
