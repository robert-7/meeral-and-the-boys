using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHUD : MonoBehaviour
{
    // Start is called before the first frame update
    private float initialXScale;
    float HealthBar = 1f;
    

    void Start()
    {
        initialXScale = transform.localScale.x;
        transform.localScale = new Vector3(initialXScale * HealthBar, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey("escape"))
        {
            Debug.Log("GOODBYE");
            Application.Quit();
          //  UnityEditor.EditorApplication.isPlaying = false;
        }

        
        if (Input.GetKeyDown("x"))
        {
            damage();
        }
    }

    public void damage()
    {
        if (HealthBar > 0) {
            HealthBar -= 0.1f;

        } else
        {
            HealthBar = 0;
        }
        transform.localScale = new Vector3(initialXScale * HealthBar, transform.localScale.y, transform.localScale.z);
    }
}
