using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float lookSpeedX, lookSpeedY, moveSpeed, cameraLimits, force = 50f;
    [SerializeField] bool spacePlayer;
    Transform playerCam;
    Inputs inputs;
    Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        inputs = FindObjectOfType<Inputs>();
        playerCam = transform.GetChild(0);
    }

    Vector2 mouseInput;
    float cameraAngle;
    void LateUpdate() {
        mouseInput = inputs.GetMouseMovement() * Time.deltaTime * 111f;
        transform.RotateAround(transform.position, transform.up, mouseInput.x * lookSpeedX);// * Time.deltaTime);
        if(!spacePlayer)
            cameraAngle = Mathf.Clamp(cameraAngle + mouseInput.y * -lookSpeedY, -cameraLimits, cameraLimits);
        else
            transform.RotateAround(transform.position, transform.right, mouseInput.y * -lookSpeedY);
        playerCam.localEulerAngles = new Vector3(cameraAngle,0,0);
    }

    Vector3 currentVelocity, movementInput;
    private void FixedUpdate() {
        if (Time.timeScale < 0)
            return;
        movementInput = inputs.GetMovementInput();
        currentVelocity = ((transform.right * movementInput.x + transform.forward * movementInput.z) * Time.deltaTime * moveSpeed * force);
        currentVelocity = Vector3Clamp(currentVelocity, -moveSpeed, moveSpeed);
        
        if(!spacePlayer)
            rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        else
            rb.velocity = new Vector3(currentVelocity.x, currentVelocity.y, currentVelocity.z);
    }

    private static Vector3 Vector3Clamp(Vector3 var, float min, float max) {
        return new Vector3(Mathf.Clamp(var.x, min, max), Mathf.Clamp(var.y, min, max), Mathf.Clamp(var.z, min, max));
    }
}
