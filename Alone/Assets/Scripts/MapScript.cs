using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {

    // Set each of these to match up to each of the corresponding sprite objects in the scene
    private GameObject DockObject;
    private GameObject HomeObject;
    private GameObject ParkObject;
    private GameObject TownHallObject;
    private GameObject StationObject;
    private GameObject MarketObject;

    // to be used for holding the paths
    private GameObject[] Paths;

    // to be used for holding the red paths
    private GameObject[] RedPaths;

	// Use this for initialization
	void Start () {

        // create GameObjects by searching for their names
        GameObject DockObject = GameObject.Find("Dock");
        GameObject HomeObject = GameObject.Find("Home");
        GameObject ParkObject = GameObject.Find("Park");
        GameObject TownHallObject = GameObject.Find("TownHall");
        GameObject StationObject = GameObject.Find("Station");
        GameObject MarketObject = GameObject.Find("Market");

	    // create array with all the path objects
        GameObject[] Paths = GameObject.FindGameObjectsWithTag("Path");

        // create array with all the red path objects
        GameObject[] RedPaths = GameObject.FindGameObjectsWithTag("RedPath");

	}
	
	// Update is called once per frame
	void Update () {
	    // when one of the destinations is clicked on, ask the player if they want to travel there

        // display how much time it will take to get there

        // highlight the path with the red paths taking the place of the black paths

        // if the player accepts, subtract time from the total time, then load to the next scene

        // 2D HITBOX DETECTION IS WORKING WOO HOO
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if(hit)    
        {
            Debug.Log("You hit: "+ hit.collider.gameObject.name);
        }
	}
}
