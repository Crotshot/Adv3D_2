using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour {

    [SerializeField] float parTime = 20f, scorePerCollectable = 100f, parBonus = 250f;
    [SerializeField] TMP_Text timeText, scoreText, parText, parBonusText, remainingText;

    float time = 0f, score = 0f;
    List<Collectable> collectables;
    string m = "00", s = "00";

    private void Start() {
        collectables = new List<Collectable>();

        List<Transform> collectiblePositions = new List<Transform>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("CollectablePosition")) {
            collectiblePositions.Add(g.transform);
        }

        foreach (Collectable c in FindObjectsOfType<Collectable>()) {
            collectables.Add(c);
            Transform t = collectiblePositions[Random.Range(0, collectiblePositions.Count)];
            c.transform.position = t.position;
            c.transform.rotation= t.rotation;
            if (t.parent != null){
                c.transform.parent = t.parent;
            }
            collectiblePositions.Remove(t);
            Destroy(t.gameObject);
        }

        for (int i = collectiblePositions.Count -1; i > -1; i--) {
            Destroy(collectiblePositions[i].gameObject);
            collectiblePositions.Remove(collectiblePositions[i]);
        }

        
        parBonusText.text = parBonus.ToString("f0");


        m = "00";
        if ((int)parTime / 60 != 0)
            m = ((int)parTime / 60).ToString("f0");

        s = "00";
        if ((int)parTime % 60 != 0 && (int)parTime % 60 < 10)
            s = "0" + ((int)parTime % 60).ToString("f0");
        else
            s = ((int)parTime % 60).ToString("f0");
        parText.text = m + ":" + s;

        remainingText.text = collectables.Count.ToString();
    }

    
    private void Update() {
        if (Time.timeScale == 0)
            return;

        time += Time.deltaTime;

        if (time > parTime) {
            parBonusText.text = "0";
        }

        m = "00";
        if ((int)time / 60 != 0)
            m = ((int)time / 60).ToString("f0");

        s = "00";
        if ((int)time % 60 != 0 && (int)time % 60 < 10)
            s = "0" + ((int)time % 60).ToString("f0");
        else
            s = ((int)time % 60).ToString("f0");
        timeText.text = m + ":" + s;
    }

    public void OnCollectableCollected(Collectable c) {
        collectables.Remove(c);
        Destroy(c.gameObject);

        score += scorePerCollectable;
        scoreText.text = score.ToString("f0");
        remainingText.text = collectables.Count.ToString();
        if (collectables.Count < 1) {
            if(time < parTime) {
                score += parBonus;
            }
            FindObjectOfType<GameManager>().LevelComplete(time, score);
        }
    }

}