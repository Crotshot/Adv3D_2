using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {
    public Vector3 GetMovementInput() { return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); }
    public Vector3 GetMousePosition() { return Input.mousePosition; }
    public Vector2 GetMouseMovement() { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    public float GetAttackInput() { return Input.GetAxis("Attack"); }
    public float GetEscapeInput() { return Input.GetAxis("Escape"); }

}