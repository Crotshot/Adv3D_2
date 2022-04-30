using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour {
    [SerializeField] GameObject[] levels;
    [SerializeField] GameObject submitScore, returnToMenu;
    GameManager gM;
    int score = 0, time = 0;

    private void Start() {
        gM = FindObjectOfType<GameManager>();
        for (int i = 0; i < levels.Length; i++) {
            Vector2 level = gM.RetrieveLevelScore(i+1);
            levels[i].transform.GetChild(0).GetComponent<TMP_Text>().text = level.x.ToString();
            score += (int)level.x;
            time += (int)level.y;

            string m = "00", s = "00";
            if ((int)level.y / 60 != 0)
                    m = ((int)level.y / 60).ToString("f0");

            if ((int)level.y % 60 != 0 && (int)level.y % 60 < 10)
                s = "0" + ((int)level.y % 60).ToString("f0");
            else
                s = ((int)level.y % 60).ToString("f0");

            levels[i].transform.GetChild(1).GetComponent<TMP_Text>().text = m + ":" + s;
        }
    }

    public void OnUploadPressed () {
        submitScore.SetActive(false);
        returnToMenu.SetActive(false);
        StartCoroutine(UploadScore());
        StartCoroutine(UploadTime());
    }

    IEnumerator UploadScore() {
        string url = "https://adv3dassignment2db.000webhostapp.com/UpdateScore.php";
        url += "?username=" + PlayerPrefs.GetString("Current_User") + "&score=" + score;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        print(result);
    }

    IEnumerator UploadTime() {
        string url = "https://adv3dassignment2db.000webhostapp.com/UpdateTime.php";
        url += "?username=" + PlayerPrefs.GetString("Current_User") + "&time=" + time;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        print(result);
        returnToMenu.SetActive(true);
    }

    public void OnMenuPressed() {
        gM.LoadScene(5);
    }
}