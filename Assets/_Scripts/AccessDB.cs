using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AccessDB : MonoBehaviour {
    string url = "https://adv3dassignment2db.000webhostapp.com/";

    IEnumerator Start() {
        print("Starting Request");
        WWW www = new WWW(url + "UpdateScore.php");
        yield return www;
        string result = www.text;
        print(result);
        //GameObject.Find("high_scores").GetComponent<Text>().text = result;
    }

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