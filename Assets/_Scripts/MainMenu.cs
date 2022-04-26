using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {

    GameManager gM;
    [SerializeField] GameObject menuPanel, logInPanel;
    [SerializeField] TMP_Text highScore, fastestTime;

    private void Awake() {
        gM = FindObjectOfType<GameManager>();

        //if (!LoggedIn) {        //Check if the player is already logged in if yes then open main panel if no open loginpanel
            logInPanel.SetActive(true);
            menuPanel.SetActive(false);
        //}
        //else {
        //    logInPanel.SetActive(false);
        //    menuPanel.SetActive(true);
        //}
    }

    #region Menu
    public void OnBeginClicked() {
        gM.LoadScene(2);
    }

    public void LogOut() {
        //Something


        menuPanel.SetActive(false);
        logInPanel.SetActive(true);
    }
    #endregion

    #region LogIn

    public void Register() { //Currently just toggles the panels will log in and out later
        menuPanel.SetActive(true);
        logInPanel.SetActive(false);
    }

    public void Login() {
        menuPanel.SetActive(true);
        logInPanel.SetActive(false);
    }

    public void OnQuitClicked() {
        gM.QuitApplication();
    }
    #endregion
}