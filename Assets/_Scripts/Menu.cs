using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] GameObject pauseCanvas;
    Inputs inputs;

    private void Start() {
        inputs = GetComponent<Inputs>();
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update() {
        if (inputs.GetEscapeInput() > 0) {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
    }

    public void OnUnpauseClicked() {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void OnMenuClicked() {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
