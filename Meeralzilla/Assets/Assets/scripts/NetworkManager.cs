using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;

public class NetworkManager : MonoBehaviour {
    private double timeSince = 0;
    private const double pollTime = 0.1;

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
}

[Serializable]
public class Event {
    public int eventCode;
    public string targetId;
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
    public double[] coordinates;
    public double rotation;
    public int lives;
}
