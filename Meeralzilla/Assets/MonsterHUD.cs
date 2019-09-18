using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHUD : MonoBehaviour
{
    // Start is called before the first frame update
    float HealthBar = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("GOODBYE");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        transform.localScale = new Vector3(HealthBar, 0.1f, 0.1f);
        if (Input.GetKey("x"))
        {
            damage();
        }
    }

    public void damage()
    {
        if (HealthBar > 0) {
            HealthBar -= .01f;
        } else
        {
            HealthBar = 0;
        }
    }
}
