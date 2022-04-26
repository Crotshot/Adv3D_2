using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(gameObject);

        ///Game Manager Setup
        //Empty!
        ///

        var w = FindObjectOfType<Wrangler>();
        if (w != null) {
            Destroy(w.gameObject); //If loaded from a scene and wrangled, go back to the scene with the new game manager
            LoadScene(PlayerPrefs.GetInt("SceneIndex"));
        }
        else {//Else just load the LogIn Scene
            LoadScene(1);
        }
    }

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void QuitApplication() {
        Application.Quit();
    }

    public void LevelComplete(float time, float score) {
        PlayerPrefs.SetFloat("Lvl" + SceneManager.GetActiveScene().buildIndex + "_time", time);
        PlayerPrefs.SetFloat("Lvl" + SceneManager.GetActiveScene().buildIndex + "_score", score);
        LoadScene(SceneManager.GetActiveScene().buildIndex +1);//Temporary
    }
}
