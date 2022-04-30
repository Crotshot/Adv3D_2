using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Currently only debugs .logs stuff but later it will make the debigs appear on screen so they work in game
public class Logger : MonoBehaviour {
    public void Log(string log) {
        Debug.Log(log);
    }
}