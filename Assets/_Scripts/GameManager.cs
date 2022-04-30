using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetString("Current_User", "NA");

        var w = FindObjectOfType<Wrangler>();
        if (w != null) {
            Destroy(w.gameObject); //If loaded from a scene and wrangled, go back to the scene with the new game manager
            LoadScene(PlayerPrefs.GetInt("SceneIndex"));
        }
        else {//Else just load the LogIn/Menu Scene
            LoadScene(5);
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

    //Retrieves a Vector 2 of (Level i score, Level i time)
    public Vector2 RetrieveLevelScore(int i) {
        return new Vector2(PlayerPrefs.GetFloat("Lvl" + i + "_score"),
            PlayerPrefs.GetFloat("Lvl" + i + "_time")
            );
    }


    public void SaveLevelIndex() {
        StartCoroutine(UploadLevel());
    }

    IEnumerator UploadLevel() {
        string url = "https://adv3dassignment2db.000webhostapp.com/UpdateLevel.php";
        url += "?username=" + PlayerPrefs.GetString("Current_User") + "&level=" + SceneManager.GetActiveScene().buildIndex;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        print(result);
        Time.timeScale = 1;
        LoadScene(5);
    }

}
