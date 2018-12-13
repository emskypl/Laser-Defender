using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public void LoadGameOver()
    {
        StartCoroutine(LoadWithDelay());

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game Over");
    }


}
