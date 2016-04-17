using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class MapScript : MonoBehaviour {

    // Set each of these to match up to each of the corresponding sprite objects in the scene
    //private GameObject DockObject;
    //private GameObject HomeObject;
    //private GameObject ParkObject;
    //private GameObject TownHallObject;
    //private GameObject StationObject;
    //private GameObject MarketObject;

    // icons used to traveling to each area
    public GameObject AcceptIcon;
    public GameObject CancelIcon;

    public float iconDistance = 3.0f;
    public float iconSpeed = 0.2f;

    // to be used for holding the paths
    public GameObject[] Paths;

    // to be used for holding the red paths
    public GameObject[] RedPaths;

    // This string will be changed upon the loading of the map screen
    // Right before the map is loaded, a global string containing the last visited area will be set
    public string lastArea = "HouseFrontMap";

    // string to store the level that will be loaded
    private string LevelToLoad;

    //// text objects to be updated upon level load confirmation
    //private Text lookText;
    //private Text lookTextShadow;
    //private Image textBackground; // the gray background behind the text

    //// timers used to count how long the text should be shown
    //private float loadedTime = 0.0f, currentTime = 0.0f;
    //// seconds to show the text
    //private float timeToShow = 3.0f;
    //// string to be used to display text
    //private string objectText = "";

    //// variable used to see if the text object should be displaying anything or not
    //private bool showingText = false;


	// Use this for initialization
	void Start () {

        //// find and set the references to the text object and images
        //lookText = GameObject.Find("LookText").GetComponent<Text>();
        //lookTextShadow = GameObject.Find("LookTextShadow").GetComponent<Text>();
        //textBackground = GameObject.Find("TextBackgroundGUI").GetComponent<Image>();

        if(GameObject.Find("InventoryHandler"))
        {
            //Debug.Log("it was found");
            //GameObject TempObject = ;
            lastArea = GameObject.Find("InventoryHandler").GetComponent<InventoryManager>().lastLevel;
            lastArea = MainManager.previousLevel;
            //Debug.Log(lastArea);
        }
        
        // create GameObjects by searching for their names
        //GameObject DockObject = GameObject.Find("Dock");
        //GameObject HomeObject = GameObject.Find("Home");
        //GameObject ParkObject = GameObject.Find("Park");
        //GameObject TownHallObject = GameObject.Find("TownHall");
        //GameObject StationObject = GameObject.Find("Station");
        //GameObject MarketObject = GameObject.Find("Market");

	    // create array with all the path objects, sort the array when making it
        Paths = GameObject.FindGameObjectsWithTag("Path").OrderBy(go => go.name).ToArray();

        /*
         * Paths[0] = Dock to Park
         * Paths[1] = Market to Station
         * Paths[2] = Park to Home
         * Paths[3] = Station to Home
         * Paths[4] = TownHall to Home
         * Paths[5] = TownHall to Station
         * Paths[6] = Market to TownHall
         * Paths[7] = Park to TownHall
         * Paths[8] = Dock to Market
         * Paths[9] = Dock to TownHall 
         * */

        // create array with all the red path objects, sort the array upon creation
        RedPaths = GameObject.FindGameObjectsWithTag("RedPath").OrderBy(go => go.name).ToArray();

        /*
         * RedPaths[0] = Dock to Park
         * RedPaths[1] = Market to Station
         * RedPaths[2] = Park to Home
         * RedPaths[3] = Station to Home
         * RedPaths[4] = TownHall to Home
         * RedPaths[5] = TownHall to Station
         * RedPaths[6] = Market to TownHall
         * RedPaths[7] = Park to TownHall
         * RedPaths[8] = Dock to Market
         * RedPaths[9] = Dock to TownHall 
         * */

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

        // What to do when one of the area's hitboxes is found
        if(hit && hit.collider.gameObject.name != "MapBackground")    
        {
            //Debug.Log("You hit: "+ hit.collider.gameObject.name);

            // HOME TO OTHER AREAS
            if(string.Equals(lastArea, "HouseFrontMap"))
            {
                string destination = hit.collider.gameObject.name;

                switch (destination)
                {
                    // home to park
                    case "Park":
                        Paths[2].renderer.enabled = false;
                        RedPaths[2].renderer.enabled = true;
                        break;

                    // home to park, park to dock
                    case "Dock":
                        Paths[2].renderer.enabled = false;
                        RedPaths[2].renderer.enabled = true;
                        Paths[0].renderer.enabled = false;
                        RedPaths[0].renderer.enabled = true;
                        break;

                    // home to station
                    case "Station":
                        Paths[3].renderer.enabled = false;
                        RedPaths[3].renderer.enabled = true;
                        break;

                    // home to townhall
                    case "TownHall":
                        Paths[4].renderer.enabled = false;
                        RedPaths[4].renderer.enabled = true;
                        break;

                    // home to market
                    case "Market":
                        Paths[4].renderer.enabled = false;
                        RedPaths[4].renderer.enabled = true;
                        Paths[6].renderer.enabled = false;
                        RedPaths[6].renderer.enabled = true;
                        break;

                    default:
                        break;
                }
            }

            // PARK TO OTHER AREAS
            if (string.Equals(lastArea, "ParkScene"))
            {
                string destination = hit.collider.gameObject.name;

                switch (destination)
                {
                    // home to park
                    case "Home":
                        Paths[2].renderer.enabled = false;
                        RedPaths[2].renderer.enabled = true;
                        break;

                    // park to dock
                    case "Dock":
                        Paths[0].renderer.enabled = false;
                        RedPaths[0].renderer.enabled = true;
                        break;

                    // park to station
                    case "Station":
                        Paths[7].renderer.enabled = false;
                        RedPaths[7].renderer.enabled = true;
                        Paths[5].renderer.enabled = false;
                        RedPaths[5].renderer.enabled = true;
                        break;

                    // park to townhall
                    case "TownHall":
                        Paths[7].renderer.enabled = false;
                        RedPaths[7].renderer.enabled = true;
                        break;

                    // park to market
                    case "Market":
                        Paths[7].renderer.enabled = false;
                        RedPaths[7].renderer.enabled = true;
                        Paths[6].renderer.enabled = false;
                        RedPaths[6].renderer.enabled = true;
                        break;

                    default:
                        break;
                }
            }

            // DOCK TO OTHER AREAS
            if (string.Equals(lastArea, "DockScene"))
            {
                string destination = hit.collider.gameObject.name;

                switch (destination)
                {
                    // dock to home
                    case "Home":
                        Paths[0].renderer.enabled = false;
                        RedPaths[0].renderer.enabled = true;
                        Paths[2].renderer.enabled = false;
                        RedPaths[2].renderer.enabled = true;
                        break;

                    // dock to park
                    case "Park":
                        Paths[0].renderer.enabled = false;
                        RedPaths[0].renderer.enabled = true;
                        break;

                    // dock to station
                    case "Station":
                        Paths[9].renderer.enabled = false;
                        RedPaths[9].renderer.enabled = true;
                        Paths[5].renderer.enabled = false;
                        RedPaths[5].renderer.enabled = true;
                        break;

                    // dock to townhall
                    case "TownHall":
                        Paths[9].renderer.enabled = false;
                        RedPaths[9].renderer.enabled = true;
                        break;

                    // dock to market
                    case "Market":
                        Paths[8].renderer.enabled = false;
                        RedPaths[8].renderer.enabled = true;
                        break;

                    default:
                        break;
                }
            }

            // MARKET TO OTHER AREAS
            if (string.Equals(lastArea, "MarketScene"))
            {
                string destination = hit.collider.gameObject.name;

                switch (destination)
                {
                    // market to home
                    case "Home":
                        Paths[6].renderer.enabled = false;
                        RedPaths[6].renderer.enabled = true;
                        Paths[4].renderer.enabled = false;
                        RedPaths[4].renderer.enabled = true;
                        break;

                    // market to park
                    case "Park":
                        Paths[6].renderer.enabled = false;
                        RedPaths[6].renderer.enabled = true;
                        Paths[7].renderer.enabled = false;
                        RedPaths[7].renderer.enabled = true;
                        break;

                    // market to station
                    case "Station":
                        Paths[1].renderer.enabled = false;
                        RedPaths[1].renderer.enabled = true;
                        break;

                    // market to townhall
                    case "TownHall":
                        Paths[6].renderer.enabled = false;
                        RedPaths[6].renderer.enabled = true;
                        break;

                    // market to dock
                    case "Dock":
                        Paths[8].renderer.enabled = false;
                        RedPaths[8].renderer.enabled = true;
                        break;

                    default:
                        break;
                }
            }

            // TOWNHALL TO OTHER AREAS
            if (string.Equals(lastArea, "LibraryScene"))
            {
                string destination = hit.collider.gameObject.name;

                switch (destination)
                {
                    // townhall to home
                    case "Home":
                        Paths[4].renderer.enabled = false;
                        RedPaths[4].renderer.enabled = true;
                        break;

                    // townhall to park
                    case "Park":
                        Paths[7].renderer.enabled = false;
                        RedPaths[7].renderer.enabled = true;
                        break;

                    // townhall to station
                    case "Station":
                        Paths[5].renderer.enabled = false;
                        RedPaths[5].renderer.enabled = true;
                        break;

                    // townhall to market
                    case "Market":
                        Paths[6].renderer.enabled = false;
                        RedPaths[6].renderer.enabled = true;
                        break;

                    // townhall to dock
                    case "Dock":
                        Paths[9].renderer.enabled = false;
                        RedPaths[9].renderer.enabled = true;
                        break;

                    default:
                        break;
                }
            }

            // STATION TO OTHER AREAS
            if (string.Equals(lastArea, "StationScene"))
            {
                string destination = hit.collider.gameObject.name;

                switch (destination)
                {
                    // station to home
                    case "Home":
                        Paths[3].renderer.enabled = false;
                        RedPaths[3].renderer.enabled = true;
                        break;

                    // station to park
                    case "Park":
                        Paths[7].renderer.enabled = false;
                        RedPaths[7].renderer.enabled = true;
                        Paths[5].renderer.enabled = false;
                        RedPaths[5].renderer.enabled = true;
                        break;

                    // station to townhall
                    case "TownHall":
                        Paths[5].renderer.enabled = false;
                        RedPaths[5].renderer.enabled = true;
                        break;

                    // station to market
                    case "Market":
                        Paths[1].renderer.enabled = false;
                        RedPaths[1].renderer.enabled = true;
                        break;

                    // station to dock
                    case "Dock":
                        Paths[9].renderer.enabled = false;
                        RedPaths[9].renderer.enabled = true;
                        Paths[5].renderer.enabled = false;
                        RedPaths[5].renderer.enabled = true;
                        break;

                    default:
                        break;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                // get the scene to load string to be used in travelcheck
                if(hit.collider.gameObject.name != "AcceptIcon" && hit.collider.gameObject.name != "CancelIcon" )
                {
                    LevelToLoad = hit.collider.gameObject.name;
                    GameObject findLevel = GameObject.Find(LevelToLoad);
                    LevelToLoad = findLevel.GetComponent<LoadLevelScript>().levelToLoad;
                    IconsAppear(hit.collider.gameObject);
                }                   

                //.GetComponent<LoadLevelScript>().levelToLoad;
                
                
            }

            
        }
        else
        {
            for (int i = 0; i < Paths.Length; i++)
            {
                Paths[i].renderer.enabled = true;
                RedPaths[i].renderer.enabled = false;
            }
        }

        CheckIcons();
	}



    void IconsAppear(GameObject spawnPoint)
    {
        // make the two icons visible
        AcceptIcon.renderer.enabled = true;
        CancelIcon.renderer.enabled = true;
        switch(spawnPoint.name)
        {
            case "Dock":
                // set their starting positions at the destination clicked on, but move these icons a specific way for certain locations
            AcceptIcon.transform.position = spawnPoint.transform.position + new Vector3(
                //boxcollider of spawn + radius of icon
                (spawnPoint.GetComponent<BoxCollider2D>().size.x / 2.0f) + AcceptIcon.GetComponent<CircleCollider2D>().radius,
                0.0f,
                -4.0f);
            CancelIcon.transform.position = spawnPoint.transform.position + new Vector3(
                0.0f,
                -(spawnPoint.GetComponent<BoxCollider2D>().size.y / 2.0f) - CancelIcon.GetComponent<CircleCollider2D>().radius,
                -4.0f);
                break;

            case "Home":
                // set their starting positions at the destination clicked on, but move these icons a specific way for certain locations
                AcceptIcon.transform.position = spawnPoint.transform.position + new Vector3(
                    //boxcollider of spawn + radius of icon
                    0.0f,
                    -(spawnPoint.GetComponent<BoxCollider2D>().size.y / 2.0f) - AcceptIcon.GetComponent<CircleCollider2D>().radius,
                    -4.0f);
                CancelIcon.transform.position = spawnPoint.transform.position + new Vector3(
                    -(spawnPoint.GetComponent<BoxCollider2D>().size.x / 2.0f) - CancelIcon.GetComponent<CircleCollider2D>().radius,
                    0.0f,
                    -4.0f);
                break;

            case "Station":
                // set their starting positions at the destination clicked on, but move these icons a specific way for certain locations
                AcceptIcon.transform.position = spawnPoint.transform.position + new Vector3(
                    //boxcollider of spawn + radius of icon
                    0.0f,
                    +(spawnPoint.GetComponent<BoxCollider2D>().size.y / 2.0f) + AcceptIcon.GetComponent<CircleCollider2D>().radius,
                    -4.0f);
                CancelIcon.transform.position = spawnPoint.transform.position + new Vector3(
                    -(spawnPoint.GetComponent<BoxCollider2D>().size.x / 2.0f) - CancelIcon.GetComponent<CircleCollider2D>().radius,
                    0.0f,
                    -4.0f);
                break;

            default:
                // set their starting positions at the destination clicked on
                AcceptIcon.transform.position = spawnPoint.transform.position + new Vector3(
                    //boxcollider of spawn + radius of icon
                    (spawnPoint.GetComponent<BoxCollider2D>().size.x / 2.0f) + CancelIcon.GetComponent<CircleCollider2D>().radius,
                    0.0f,
                    -4.0f);
                CancelIcon.transform.position = spawnPoint.transform.position + new Vector3(

                    -(spawnPoint.GetComponent<BoxCollider2D>().size.x / 2.0f) - CancelIcon.GetComponent<CircleCollider2D>().radius,
                    0.0f,
                    -4.0f);
                break;
        }
        

        

        //Debug.Log(spawnPoint.transform.position);
        //Debug.Log(spawnPoint.GetComponent<BoxCollider2D>().size);

        //// move the icons to their end positions, a short distance from the starting points
        //for (float i = 0; i < iconDistance; i += iconSpeed)
        //{
        //    AcceptIcon.transform.position -= new Vector3(iconSpeed, AcceptIcon.transform.position.y, AcceptIcon.transform.position.z);
        //    AcceptIcon.transform.position += new Vector3(iconSpeed, AcceptIcon.transform.position.y, AcceptIcon.transform.position.z);
        //}

    }

    void CheckIcons()
    {
        // if the accept is clicked, load the next level
        
        // 2D HITBOX DETECTION IS WORKING WOO HOO
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction);
        // if the cancel is clicked, make the icons disappear

        if (hit2)
        {
            string status = hit2.collider.gameObject.name;
            
                if (string.Equals(status, "AcceptIcon"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log("clicked an icon");
                        // do not subtract time from going to the map or if you are going back to the same area
                        if (LevelToLoad == "MainMap" || string.Equals(MainManager.previousLevel, LevelToLoad))
                        {
                            MainManager.takeTime = false;
                        }
                        //only take time away if you are leaving the map to go to a new area
                        else
                        {
                            MainManager.takeTime = true;
                        }

                        // set the previous level to be the current level right before you load the next one
                        MainManager.previousLevel = Application.loadedLevelName;

                        Application.LoadLevel(LevelToLoad);
                    }

                }
                else if (string.Equals(status, "CancelIcon"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log("clicked an icon");
                        AcceptIcon.renderer.enabled = false;
                        CancelIcon.renderer.enabled = false;
                        // TODO add the confirmation text disappearing here
                    }
                }
            

        }
        
    }
       
            
        
                    
        
            
    

}
