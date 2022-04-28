using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour {
    [SerializeField] GameObject[] levels;
    GameManager gM;

    private void Start() {
        gM = FindObjectOfType<GameManager>();
        for (int i = 0; i < levels.Length; i++) {
            Vector2 level = gM.RetrieveLevelScore(i+1);
            levels[i].transform.GetChild(0).GetComponent<TMP_Text>().text = level.x.ToString();

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
        
    }

    public void OnMenuPressed() {
        gM.LoadScene(5);
    }
}