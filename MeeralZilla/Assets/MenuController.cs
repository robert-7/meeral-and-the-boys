using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public int type = 0;

    // Update is called once per frame
    void Update()
    {

        if (type == 0)
        {
            if (Input.GetKey("return"))
            {
                Debug.Log("shouldLoad");
                SceneManager.LoadScene("Main Scene");
            }
        }
        if (type == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("TestPlane");

            }
        }


    }
}
