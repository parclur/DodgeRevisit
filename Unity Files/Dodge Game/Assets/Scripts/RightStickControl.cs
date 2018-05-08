using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStickControl : MonoBehaviour {

    Vector3 startPos;
    Vector3 parentPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        parentPos = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
        SetParentPos();
        CheckRightInput();
	}

    void SetParentPos()
    {
        parentPos = transform.parent.position; // works and doesnt need to find the parent through hierarchy
    }

    void CheckRightInput()
    {
        Vector3 inputDirection = Vector3.zero;

        inputDirection.x = Input.GetAxis("RightJoystickHorizontal");
        inputDirection.y = Input.GetAxis("RightJoystickVertical");

        transform.position = parentPos + inputDirection;

    }
}
