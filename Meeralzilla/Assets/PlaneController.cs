using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private float tiltAroundY = 0;
    private float tiltAroundZ = 0;

    private bool movingLeft = true;
    private bool facingLeft = false;
    // Update is called once per frame

    private float SideSpeed = 5.0f;
    private float UpdownSpeed = 1.0f;
    private float maxHeight = 40;
    private float minHeight = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            tiltAroundZ = tiltAroundZ + 0.5f;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            tiltAroundZ = tiltAroundZ - 0.5f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movingLeft = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movingLeft = false;
        }

        if (movingLeft && !facingLeft)
        {
            GameObject plane = GameObject.Find("Plane1_S2");
            plane.transform.localRotation *= Quaternion.Euler(0, 0, 180);
            facingLeft = true;
        } else if (!movingLeft && facingLeft)
        {
            GameObject plane = GameObject.Find("Plane1_S2");
            plane.transform.localRotation *= Quaternion.Euler(0, 0, 180);
            facingLeft = false;
        }


        if (movingLeft) {
            tiltAroundY = tiltAroundY + SideSpeed;
            
        }
        else {
            tiltAroundY = tiltAroundY - SideSpeed;
        }


        Quaternion target = Quaternion.Euler(0, tiltAroundY, tiltAroundZ);
        transform.rotation = target;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
    }


    public void GoLeft() {
        movingLeft = true;
    }
    public void GoRight() {
        movingLeft = false;
    }

    public void GoUp() {
        if (transform.eulerAngles.z < maxHeight)
        {
            tiltAroundZ = tiltAroundZ + UpdownSpeed;
        }
    }

    public void GoDown() {
        if (transform.eulerAngles.z > minHeight)
        {
            tiltAroundZ = tiltAroundZ - UpdownSpeed;
        }
    }


    public PlaneState GetPlaneState() {
        PlaneState r = new PlaneState();
        r.rotation = new double[] {gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z };

        return r;
    }
}
