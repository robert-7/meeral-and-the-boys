using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public GameObject dronePrefab;

    private Dictionary<string, GameObject> droneMap;
   
    // Start is called before the first frame update
    void Start()
    {
        droneMap = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateState(GameState gs) {
        //Planes
        foreach (PlaneState ps in gs.planes) {
           if (droneMap.ContainsKey(ps.id)) {
               //update position
           } else {
               //add new plane
            }
            //mark plane dirty
        }

            //remove planes not marked dirty
    }

    private void CreateDrone(string s, Vector3 rotation) {
        if (!droneMap.ContainsKey(s))
        {
            GameObject newPlane = Instantiate(dronePrefab, new Vector3(), Quaternion.Euler(0, rotation.y, rotation.z));
            droneMap.Add(s, newPlane);
        }
    } 

    private void RemoveDrone(string s)
    {
        if (droneMap.ContainsKey(s))
        {
            GameObject doomedPlane = droneMap[s];
            GameObject.Destroy(doomedPlane);
            droneMap.Remove(s);
        }
    }

}
