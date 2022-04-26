using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.TryGetComponent(out PlayerMovement p)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
