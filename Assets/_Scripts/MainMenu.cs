using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {
    GameManager gM;
    [SerializeField] GameObject menuPanel, logInPanel, loadLevelButton;
    [SerializeField] TMP_Text highScore, fastestTime, user;
    [SerializeField] TMP_InputField username, passsword;
    int savedLevel = -1;
    Logger l;

    private void Start() {
        gM = FindObjectOfType<GameManager>();
        l = gM.GetComponent<Logger>();
        if (PlayerPrefs.GetString("Current_User").Equals("NA")) {
            logInPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        else {
            user.text = PlayerPrefs.GetString("Current_User");
            StartCoroutine(GrabScore());
            StartCoroutine(GrabTime());
            StartCoroutine(GrabLevel());
            logInPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }

    #region Menu
    public void OnBeginClicked() {
        gM.LoadScene(1);
    }

    public void LogOut() {
        PlayerPrefs.SetString("Current_User", "NA");
        highScore.text = "NA";
        fastestTime.text = "NA";

        menuPanel.SetActive(false);
        logInPanel.SetActive(true);
    }
    #endregion

    #region LogIn
    public void Login() {
        StartCoroutine(LoginUser());
        logInPanel.SetActive(false);
    }

    IEnumerator LoginUser() {
        string uName = username.text, pWord = passsword.text;
        if (uName.Length < 4 || pWord.Length < 4) {//Check for validity of username and password
            l.Log("Username or password is too short, ensure they are both at least 4 characters long");
            yield break;// later this could aslo check for special chracters in the password
        }

        string url = "https://adv3dassignment2db.000webhostapp.com/LoginUser.php";
        url += "?username=" + uName + "&password=" + pWord;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        l.Log(result);

        if (result.Contains("Success_01")) {
            user.text = uName;
            PlayerPrefs.SetString("Current_User", uName);
            menuPanel.SetActive(true);
            StartCoroutine(GrabScore());
            StartCoroutine(GrabTime());
            StartCoroutine(GrabLevel());
        }
        else {
            logInPanel.SetActive(true);
        }
    }

    IEnumerator GrabScore() {
        string url = "https://adv3dassignment2db.000webhostapp.com/AccessScore.php";
        url += "?username=" + user.text;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        l.Log(result);

        if (!result.Contains("Error")) {
            highScore.text = result;
        }
    }

    IEnumerator GrabTime() {
        string url = "https://adv3dassignment2db.000webhostapp.com/AccessTime.php";
        url += "?username=" + user.text;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        l.Log(result);

        if (!result.Contains("Error")) {
            int t = int.Parse(result);
            string m, s;
            m = "00";
            if (t % 60 < 10)
                m = "0" + (t / 60).ToString("f0");
            else
                m = (t / 60).ToString("f0");

            s = "00";
            if (/*t % 60 != 0 && */t % 60 < 10)
                s = "0" + (t % 60).ToString("f0");
            else
                s = (t % 60).ToString("f0");

            fastestTime.text = m + ":" + s;
        }
    }

    IEnumerator GrabLevel() {
        string url = "https://adv3dassignment2db.000webhostapp.com/AccessLevel.php";
        url += "?username=" + user.text;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        l.Log(result);

        if (!result.Contains("Error")) {
            savedLevel = int.Parse(result);
            loadLevelButton.SetActive(true);
        }
    }
    #endregion


    #region Registration
    public void Register() {
        StartCoroutine(RegisterUser());
        logInPanel.SetActive(false);
    }

    IEnumerator RegisterUser() {
        string uName = username.text, pWord = passsword.text;
        if (uName.Length < 4 || pWord.Length < 4) {//Check for validity of username and password
            l.Log("Username or password is too short, ensure they are both at least 4 characters long");
            yield break;// later this could aslo check for special chracters in the password
        }

        l.Log("Creating new user");
        string url = "https://adv3dassignment2db.000webhostapp.com/RegisterUser.php";
        url += "?username=" + uName + "&password=" + pWord;
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        l.Log(result);

        logInPanel.SetActive(true);
    }
    #endregion

    public void OnQuitClicked() {
        gM.QuitApplication();
    }

    public void OnLoadLevelPressed() {
        gM.LoadScene(savedLevel);
    }
}