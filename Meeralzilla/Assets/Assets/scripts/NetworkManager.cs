using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;

public class NetworkManager : MonoBehaviour {
    private static NetworkManager instance = null;

    private Queue<Event> eventQueue = new Queue<Event>();
    private Dictionary<string, bool> eventsSeen = new Dictionary<string, bool>();

    enum eventCodes { shoot = 1, planeDead = 2 };
    public enum playerType { plane, monster }

    private double timeSince = 0;
    private const double pollTime = 0.1;
    public playerType whatAmI = playerType.plane;
    private int eventCounter = 0;
    private string selfId;
    public string ServerUrlBase = ""; // No trailing slash

    private DroneManager dm;
    private monsterbase mb;
    private PlaneController pc;

    /*private NetworkManager() {
    }*/

    public static NetworkManager Instance {
        get {
            if (instance == null) {
                instance = new NetworkManager();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start() {
        this.dm = gameObject.GetComponent<DroneManager>();
        if (this.whatAmI == playerType.monster) {
    //        try {
                this.mb = GameObject.Find("Player").GetComponent<monsterbase>();
      //      }
        //    catch { }
        } else {
            this.pc = GameObject.Find("PlaneBase").GetComponent<PlaneController>();
        }
        
        this.RegisterWithServer();

        Debug.Log("sdalfasdfdasfa");
        Beep();
    }

    // Update is called once per frame
    void Update() {
        this.timeSince += Time.deltaTime;
        if (this.timeSince >= pollTime) {
            this.timeSince -= pollTime;
            PollServer();
        }
    }

    void RegisterWithServer() {
        string endpoint;

        if (this.whatAmI == playerType.plane) {
            endpoint = "/planes";
        } else {
            endpoint = "/monsters";
        }

        string url = this.ServerUrlBase + endpoint;
        string info = this.sendServerCall(url, "POST", "");

        RegisterResponse resp = JsonUtility.FromJson<RegisterResponse>(info);
        if (resp.id != "") {
            this.selfId = resp.id;
        }
    }

    void PollServer() {
        //Debug.Log("poll" + this.timeSince);
        GameState newState = new GameState();

        UpdateToServer serverUpdate = new UpdateToServer();

        if (this.whatAmI == playerType.monster) {
            //serverUpdate.selfMonster = new MonsterState();
            serverUpdate.selfMonster = this.mb.GetState();
            serverUpdate.selfMonster.exists = true;
        } else {
            //serverUpdate.selfPlane = new PlaneState();
            serverUpdate.selfPlane = this.pc.GetPlaneState();
            serverUpdate.selfPlane.id = this.selfId;
            serverUpdate.selfPlane.exists = true;
        }
        if (this.eventQueue.Count > 0) {
            int numEvents = this.eventQueue.Count;

            serverUpdate.eventsOccurred = new Event[numEvents];

            for (int i = 0; i < numEvents; i++) {
                serverUpdate.eventsOccurred[i] = this.eventQueue.Dequeue();
            }
        }
        // TODO: get plane or monster state from playermanager/monstermanager and insert into serverUpdate

        string serverUpdateJson = JsonUtility.ToJson(serverUpdate);
        Debug.Log(serverUpdateJson);
        string url = this.ServerUrlBase + "/update";

        // call server with update and get new state back
        string newStateJson = this.sendServerCall(url, "PUT", serverUpdateJson);
        Debug.Log(newStateJson);
        newState = JsonUtility.FromJson<GameState>(newStateJson);

        // Call any events if present
        if (newState.events != null) {
            for (int i = 0; i < newState.events.Length; i++) {
                bool blarg = false;
                Event thingy = newState.events[i];

                if (this.eventsSeen.TryGetValue(thingy.eventId, out blarg)) {
                    continue;
                }

                try {
                    this.eventsSeen.Add(thingy.eventId, true);
                }
                catch (ArgumentException) { }

                if (thingy.eventCode == (int)eventCodes.planeDead) {
                    this.dm.RemoveDrone(thingy.planeId);
                } else {
                    this.dm.FireShotFromPlane(thingy.planeId);
                }
            }
        }
        newState.events = new Event[0];

        // Check if any of the planes are us
        int foundSelfPlaneAtIndex = -1;
        for (int i = 0; i < newState.planeList.Length; i++) {
            if (newState.planeList[i].id == this.selfId) {
                foundSelfPlaneAtIndex = i;
                break;
            }
        }

        // If one of the planes is us, filter it out
        if (foundSelfPlaneAtIndex >= 0) {
            PlaneState[] newPlaneStates = new PlaneState[newState.planeList.Length - 1];
            for (int i = 0; i < newState.planeList.Length; i++) {
                if (i < foundSelfPlaneAtIndex) {
                    newPlaneStates[i] = newState.planeList[i];
                } else if (i > foundSelfPlaneAtIndex) {
                    newPlaneStates[i - 1] = newState.planeList[i];
                }
            }
            newState.planeList = newPlaneStates;
        }

        this.dm.updateState(newState);

        string json = JsonUtility.ToJson(newState);
        Debug.Log(json);
    }

    // Called when a plane controlled locally takes a shot
    public void ShootBullet(string planeId) {
        Event shootEvent = new Event();
        shootEvent.eventCode = (int)eventCodes.shoot;
        shootEvent.planeId = planeId;
        shootEvent.eventId = planeId + this.eventCounter++;

        try {
            this.eventsSeen.Add(shootEvent.eventId, true);
        }
        catch (ArgumentException) { }

        this.eventQueue.Enqueue(shootEvent);
    }

    // Called when a monster controlled locally smacks a plane
    public void KillPlane(string planeId) {
        Event killEvent = new Event();
        killEvent.eventCode = (int)eventCodes.planeDead;
        killEvent.planeId = planeId;
        killEvent.eventId = planeId + this.eventCounter++;

        try {
            this.eventsSeen.Add(killEvent.eventId, true);
        }
        catch (ArgumentException) { }

        this.eventQueue.Enqueue(killEvent);
    }

    private string sendServerCall(string url, string method, string data) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        if (method != "") {
            request.Method = method;
        }
        if (data.Length > 0) {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string responseFromServer;

        using (Stream dataStream = response.GetResponseStream()) {
            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
           // Debug.Log(responseFromServer);
        }

        response.Close();

        return responseFromServer;
    }

    public void Beep() {
        // Create a request for the URL.   
        WebRequest request = WebRequest.Create(
          "http://www.allsn.ca");
        // If required by the server, set the credentials.  
        request.Credentials = CredentialCache.DefaultCredentials;

        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Debug.Log(((HttpWebResponse)response).StatusDescription);

        // Get the stream containing content returned by the server. 
        // The using block ensures the stream is automatically closed. 
        using (Stream dataStream = response.GetResponseStream()) {
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            Debug.Log(responseFromServer);
        }

        // Close the response.  
        response.Close();
    }
}

[Serializable]
public class GameState {
    public MonsterState monster;
    public PlaneState[] planeList;
    public Event[] events;
}

[Serializable]
public class Event {
    public string eventId; // opaque
    public int eventCode;  // which type of event
    public string planeId; // the id string of the plane
}

[Serializable]
public class MonsterState {
    public bool exists;
    public int health;
    public double[] lh;
    public double[] lhRotation;
    public double[] rh;
    public double[] rhRotation;
    public double[] head;
    public double[] headRotation;
}

[Serializable]
public class PlaneState {
    public bool exists;
    public string id;
    public double[] rotation;
    public int lives;
    public string status; // "alive" or "dead"
}

[Serializable]
public class UpdateToServer {
    public PlaneState selfPlane;    // one of these two will be null
    public MonsterState selfMonster;
    public Event[] eventsOccurred;
}

[Serializable]
public class RegisterResponse {
    public string id;
}