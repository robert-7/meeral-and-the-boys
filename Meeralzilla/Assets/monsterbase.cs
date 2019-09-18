using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterbase : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, -Input.GetAxis("Vertical") * speed);
    }


    public MonsterState GetState()
    {
        MonsterState r = new MonsterState();
        double[] headpos = new double[] { transform.position.x, transform.position.y, transform.position.z};
        r.head = headpos;

        double[] headrot = new double[] { transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z };
        r.headRotation = headrot;

        GameObject LH = GameObject.Find("LeftHand");      
        double[] lhPos = new double[] { LH.transform.position.x, LH.transform.position.y, LH.transform.position.z };
        r.lh = lhPos;

        double[] lhRot = new double[] { LH.transform.rotation.eulerAngles.x, LH.transform.rotation.eulerAngles.y, LH.transform.rotation.eulerAngles.z };
        r.lhRotation = lhRot;

        GameObject RH = GameObject.Find("RightHand");
        double[] rhPos = new double[] { RH.transform.position.x, RH.transform.position.y, RH.transform.position.z };
        r.rh = rhPos;

        double[] rhRot = new double[] { RH.transform.rotation.eulerAngles.x, RH.transform.rotation.eulerAngles.y, RH.transform.rotation.eulerAngles.z };
        r.rhRotation = rhRot;
    

        return r;
    }

}
