using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    bool goLeft;
    bool goRight;
    bool goUp;
    bool goDown;
    bool goShoot;
    GameObject plane;
    GameObject planeShoot;

    public float fireRate = 1f;
    private float nextTimeToShoot = 0f;


    void Start ()
    {
        plane = GameObject.Find("PlaneBase");
        planeShoot = GameObject.Find("Plane1_S2");
    }

    void Update ()
    {
        if (goLeft == true) {
            plane.GetComponent<PlaneController>().GoLeft();
        }
        if (goRight == true)
        {
            plane.GetComponent<PlaneController>().GoRight();
        }
        if (goUp == true)
        {
            plane.GetComponent<PlaneController>().GoUp();
        }
        if (goDown == true)
        {
            plane.GetComponent<PlaneController>().GoDown();
        }
        if (goShoot == true && Time.time >= nextTimeToShoot) {
            nextTimeToShoot = Time.time + 1f/fireRate;
            planeShoot.GetComponent<Plane>().Shoot();
        }
    }

    public void leftDown ()
    {
        goLeft = true;
    }

    public void leftUp ()
    {
        goLeft = false;
    }

    public void upDown()
    {
        goUp = true;
    }

    public void upUp()
    {
        goUp = false;
    }

    public void downDown()
    {
        goDown = true;
    }

    public void downUp()
    {
        goDown = false;
    }

    public void rightDown()
    {
        goRight = true;
    }

    public void rightUp()
    {
        goRight = false;
    }

    public void shootDown() {
        goShoot = true;
    }
    public void shootUp() {
        goShoot = false;
    }
}
