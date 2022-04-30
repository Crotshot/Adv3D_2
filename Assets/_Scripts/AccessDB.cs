using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AccessDB : MonoBehaviour {
    string url = "https://adv3dassignment2db.000webhostapp.com/";

    IEnumerator Start() {
        print("Starting Request");
        WWW www = new WWW(url + "Access.php");
        yield return www;
        string result = www.text;
        print(result);
    }

    //print("Starting Request");
    //WWW www = new WWW(url + "db.php");
    //yield return www;
    //string result = www.text;
    //print(result);

    //WWWForm form = new WWWForm();
    //form.AddField("score", 324423);
    //UnityWebRequest uwr = UnityWebRequest.Post(url + "insertscore.php", form);
    //yield return uwr.SendWebRequest();
    //if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError) {
    //    print(uwr.error);
    //}
    //else {
    //    print("Great Success!");
    //}

    //public void Insert() {
    //    StartCoroutine(InsertIntoDatabase());
    //}

    //IEnumerator InsertIntoDatabase() {
    //    WWWForm form = new WWWForm();
    //    form.AddField("score", 8905);
    //    print("Adding score");
    //    using (UnityWebRequest uwr = UnityWebRequest.Post(url + "insertscore.php", form)) {
    //        yield return uwr.SendWebRequest();
    //        if(uwr.isNetworkError || uwr.isHttpError) {
    //            print(uwr.error);
    //        }
    //        else {
    //            print("Record added");
    //        }
    //    }
    //}
}


/*
    [SerializeField] Text txtValue, txtMaxScore;


    public void GenerateNumber()
    {
        int number = GenerateRandomNumber();
        txtValue.text = number.ToString();
        StartCoroutine(InsertIntoDataBase(number));
        
    }

    int GenerateRandomNumber()
    {
        int rand = Random.Range(0, 1000001);
        return rand;
    }

    IEnumerator InsertIntoDataBase(int num)
    {
        // Change url to your own
        string url = "your url of the php script";
        WWWForm form = new WWWForm();
        form.AddField("score", num);
        using(UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                print(www.error);
            }
            else
            {
                //print(www.downloadHandler.text);
                txtMaxScore.text = "MAX SCORE: " + www.downloadHandler.text;
            }
        }
    }
*/
