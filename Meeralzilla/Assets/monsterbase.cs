using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterbase : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, -Input.GetAxis("Vertical") * speed);
    }

}
