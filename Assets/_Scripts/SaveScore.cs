using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveScore : MonoBehaviour {
    string playerNname;
    int score;

    void Start() {
        gameObject.GetComponent<InputField>().onEndEdit.AddListener(Save);
        score = 1000;
    }

    void Save(string textInField) {
        playerNname = textInField;
        print("Starting to save score for user " + textInField);
        StartCoroutine("ConnectToPHP");
    }

    IEnumerator ConnectToPHP() {
        string url = "http://localhost:8888/UpdateScore_b.php";
        url += "?name=" + playerNname + "&score=" + score;
        WWW www = new WWW(url);
        yield return www;
        print("DB updated");
    }
}