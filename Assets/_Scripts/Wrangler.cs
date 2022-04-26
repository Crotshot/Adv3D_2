using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wrangler : MonoBehaviour {
    public enum Version { Zero, One}
    [SerializeField] Version version = Version.One;

    private void Awake() {
        if (!FindObjectOfType<GameManager>()) {
            DontDestroyOnLoad(this);
            Debug.LogWarning("Test Started in wrong scene, wrangled back to _preload");
            if(version == (Version) 1)
                PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex);

            SceneManager.LoadScene("_preload");
        }
        else {
            Destroy(gameObject);
        }
    }
}