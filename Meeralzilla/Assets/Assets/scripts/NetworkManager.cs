using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;

public class NetworkManager : MonoBehaviour {
    private static NetworkManager instance = null;

    private Queue<Event> eventQueue = new Queue<Event>();

    enum eventCodes {shoot = 1, planeDead = 2};

    private double timeSince = 0;
    private const double pollTime = 0.1;

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

    void PollServer() {
        //Debug.Log("poll" + this.timeSince);
        GameState state = new GameState();
        string json = JsonUtility.ToJson(state);
        Debug.Log(json);
    }

    // Called when a plane controlled locally takes a shot
    public void ShootBullet(string planeId) {
        Event shootEvent = new Event();
        shootEvent.eventCode = (int)eventCodes.shoot;
        this.eventQueue.Enqueue(shootEvent);
    }

    // Called when a monster controlled locally smacks a plane
    public void KillPlane(string planeId) {
        Event killEvent = new Event();
        killEvent.eventCode = (int)eventCodes.planeDead;
        killEvent.targetId = planeId;
        this.eventQueue.Enqueue(killEvent);
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
    public PlaneState[] planes;
    public Event[] events;
}

[Serializable]
public class Event {
    public string eventId; // opaque
    public int eventCode;  // which type of event
    public string targetId; // the id string of the plane
}

[Serializable]
public class MonsterState {
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
    public string id;
    public double[] rotation;
    public int lives;
}

public class UpdateToServer {
    public PlaneState selfPlane;    // one of these two will be null
    public MonsterState selfMonster;
    public Event[] eventsOccurred;
}