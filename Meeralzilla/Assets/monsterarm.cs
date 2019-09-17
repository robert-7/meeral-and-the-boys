using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterarm : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        Debug.Log("arm");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
