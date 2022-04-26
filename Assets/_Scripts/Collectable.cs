using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    [SerializeField] float spin = 36f;

    private void FixedUpdate() {
        transform.RotateAround(transform.position, transform.up, spin * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerMovement pM)) {
            FindObjectOfType<Level>().OnCollectableCollected(this);
        }
    }
}