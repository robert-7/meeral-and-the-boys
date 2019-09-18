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
    GameObject plane;


    void Start ()
    {
        plane = GameObject.Find("PlaneBase");
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
        Debug.Log("test");
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

}
