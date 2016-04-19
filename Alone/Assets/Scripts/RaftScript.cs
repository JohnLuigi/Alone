using UnityEngine;
using System.Collections;

public class RaftScript : MonoBehaviour {

    // the icon to click on to show the build prompt
    private GameObject buildIcon;

    // the rope to hide if the raft has been built
    private GameObject rope;

	// Use this for initialization
	void Start () {

	    // get and set the build icon and rope
        buildIcon = GameObject.Find("BuildIcon");
        rope = GameObject.Find("Rope");

        if(MainManager.raftBuilt)
        {
            rope.transform.position = new Vector3(rope.transform.position.x, rope.transform.position.y, 1.0f);
            buildIcon.transform.position = new Vector3(buildIcon.transform.position.x, buildIcon.transform.position.y, 1.0f);
        }

        // hide the accept and cancel icons
        GameObject acceptIcon = GameObject.Find("AcceptIcon");
        acceptIcon.transform.position = new Vector3(acceptIcon.transform.position.x, acceptIcon.transform.position.y, 1.0f);
        GameObject cancelIcon = GameObject.Find("CancelIcon");
        cancelIcon.transform.position = new Vector3(cancelIcon.transform.position.x, cancelIcon.transform.position.y, 1.0f);


	}
	
	// Update is called once per frame
    // only checking for this stuff once per frame so that as soon as the player drops down the 6th log, they can build
	void Update () {

	    // check if there are 6 logs now shown
        if(MainManager.storedLogs.Count == 6 && MainManager.raftBuilt == false)
        {
            // show the build icon
            buildIcon.transform.position = new Vector3(buildIcon.transform.position.x, buildIcon.transform.position.y, -3.9f);
        }
        else
        {
            // hide the build icon
            buildIcon.transform.position = new Vector3(buildIcon.transform.position.x, buildIcon.transform.position.y, 1.0f);
        }
	}
}
