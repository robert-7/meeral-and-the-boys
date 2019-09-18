﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public GameObject dronePrefab;
    public GameObject LeftHandPrefab;
    public GameObject RightHandPrefab;
    public GameObject Head;

    private Dictionary<string, GameObject> droneMap;
    private Dictionary<string, bool> dirtyMap;

       
    // Start is called before the first frame update
    void Start()
    {
        droneMap = new Dictionary<string, GameObject>();
        dirtyMap = new Dictionary<string, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateState(GameState gs) {
        //Planes
        dirtyMap.Clear();

        foreach (PlaneState ps in gs.planes) {
           if (droneMap.ContainsKey(ps.id)) {
                //make planes move around
                GameObject droneObj = droneMap[ps.id];
                Quaternion target = Quaternion.Euler(0, (float)ps.rotation[0], (float)ps.rotation[1]);
                droneObj.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
            } else {
                this.CreateDrone(ps.id, new Vector3(0, (float)ps.rotation[0], (float)ps.rotation[1]));
            }
            //mark plane dirty
            dirtyMap.Add(ps.id, true);
        }

        //remove planes not marked dirty
        foreach (var key in droneMap.Keys) {
            if (!dirtyMap.ContainsKey(key)){
                RemoveDrone(key);
            }
        }



    }

    public void CreateDrone(string id, Vector3 rotation) {
        if (!droneMap.ContainsKey(id))
        {
            GameObject newPlane = Instantiate(dronePrefab, new Vector3(), Quaternion.Euler(0, rotation.y, rotation.z));
            droneMap.Add(id, newPlane);
        }
    }

    public void RemoveDrone(string id)
    {
        if (droneMap.ContainsKey(id))
        {
            GameObject doomedPlane = droneMap[id];
            GameObject.Destroy(doomedPlane);
            droneMap.Remove(id);
        }
    }

    public void FireShotFromPlane(string id)
    {

    }

}