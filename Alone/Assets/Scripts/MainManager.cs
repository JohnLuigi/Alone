using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// This script is the handler of many of the variables that are going to be tracked throughout the game
// Stuff like whether an item was picked up or not will be noted here, and the item will then be destroyed on loading if it has already been used
// Otherwise, players could just re-use the same item over and over to accomplish the in-game goals


// delete the items here instead of on the items themselves




public class MainManager : MonoBehaviour {

    // lists that persit through level loads and store the names + locations of the food that you have picked up
    public static List<string> storedFoodNames;
    public static List<float> storedFoodX;
    public static List<float> storedFoodY;
    public static List<float> storedFoodZ;

    // public values that are used to track how much time has passed
    // static ints that track the days and hours left
    // they will change depending on the actions you take, and can only decrease
    public static int days = 3;
    public static int hours = 2;

    // text objects to be changed upon a level load
    private Text lookText;
    private Text lookTextShadow;
    private Image textBackground; // the gray background behind the text    

    // timers used to count how long the text should be shown
    private float loadedTime = 0.0f, currentTime = 0.0f;
    // seconds to show the text
    private float timeToShow = 3.0f;
    // string to be used to display text
    private string objectText = "";

    // used to track the previous loaded level (to make sure time is not taken when leaving the front door)
    public static string previousLevel = "";
    public static bool takeTime = false;

    // final super variable to load the end scene
    public static bool loadEnd = false;
    // variable used to see if the text object should be displaying anything or not
    private bool showingText = false;

    // images used to tint the screen for the time of day
    private GameObject sunsetOverlay;
    private GameObject nightOverlay;
    private GameObject dawnOverlay;

    // bool used to track if the cart was grabbed and whether to show it in the living room or not
    public static bool cartGrabbed = false;

    // bool used to see if the player is trying to use an item with an object in the game world
    // (after clicking the use icon)
    public static bool itemInUse = false;

    // string to track the name of the item being used.
    // if the right item being used is "used on" the correct item, take the necessary action
    public static string itemBeingUsed = "";

    // bool used to see if the axe is stored, and whether to hide/show the broken glass
    public static bool axeGrabbed = false;

    // bools used to see if the trees were used or not
    // to hide/show logs/trees on level load
    public static bool tree1Used = false;
    public static bool tree2Used = false;
    public static bool tree3Used = false;

    // list that will store the names of the logs to show
    public static List<string> storedLogs;

    // bool to check whether the raft has been built or not
    public static bool raftBuilt = false;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        //storedFood = new List<GameObject>();
        // initialize the lists as empty
        storedFoodNames = new List<string>();
        storedFoodX = new List<float>();
        storedFoodY = new List<float>();
        storedFoodZ = new List<float>();

        storedLogs = new List<string>();
    }

	// Use this for initialization
	void Start () {

        

        // find and set the references to the text object and images
        lookText = GameObject.Find("LookText").GetComponent<Text>();
        lookTextShadow = GameObject.Find("LookTextShadow").GetComponent<Text>();
        textBackground = GameObject.Find("TextBackgroundGUI").GetComponent<Image>();

        // find and set the references to the color overlays
        sunsetOverlay = GameObject.Find("SunsetOverlay");
        nightOverlay = GameObject.Find("NightOverlay");
        dawnOverlay = GameObject.Find("DawnOverlay");

        // center the overlays on the camera
        sunsetOverlay.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -3.5f);
        nightOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.5f);
        dawnOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.5f);

        // hide the overlays at the start
        sunsetOverlay.renderer.enabled = false;
        nightOverlay.renderer.enabled = false;
        dawnOverlay.renderer.enabled = false;
	}

    // do something when a level is loaded
    void OnLevelWasLoaded(int level)
    {
        // on any level load, check if the time left is zero
        if(level >= 0)
        {
            //Debug.Log(Application.loadedLevelName);
            Debug.Log("Days left: " + MainManager.days + ", hours left: " + MainManager.hours);

            // center the overlays on the camera
            sunsetOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.99f);
            nightOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.99f);
            dawnOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.99f);

            // hide and text that was to be shown from the previous level
            showingText = false;
            // if the time left is zero or somehow less than zero, load the final scene to let the player know the progress they made
            if(loadEnd)
            {
                loadEnd = false;
                Application.LoadLevel("EndScene");
            }
            
            
        }

        // level ranges:
        // 0 is the intro scene
        // 1-4 are inside the house
        // 5-6 are reaching the house itself
        // 7 is the main map
        // 8-12 are town areas
        // 13 is end scene

        // only update the time taken if the previous level traveled was the map scene

        // use the text display/update that was used in the IconScript here
        switch (level)
        {
            // reaching the house
            case 6:
                // if the previous level was the living room or the same level, don't subtract time taken
                if (MainManager.previousLevel == "LivingRoomScene")
                {
                    SetText("Time to hit the town.");
                }
                else
                {
                    SetText("Home sweet home. Took two hours to get here.");
                    UpdateTimeLeft(2);
                }

                GameObject one = GameObject.Find("One");
                GameObject two = GameObject.Find("Two");

                GameObject oneGoo = GameObject.Find("OneGoo");
                GameObject twoGoo = GameObject.Find("TwoGoo");
                GameObject threeGoo = GameObject.Find("ThreeGoo");

                oneGoo.renderer.enabled = false;
                twoGoo.renderer.enabled = false;
                threeGoo.renderer.enabled = false;

                // goo number on door handling
                if(MainManager.days == 2)
                {
                    one.renderer.enabled = false;
                    two.renderer.enabled = true;
                }
                else if (MainManager.days == 1)
                {
                    one.renderer.enabled = true;
                    two.renderer.enabled = true;
                }
                else if(MainManager.days == 0)
                {
                    one.renderer.enabled = true;
                    two.renderer.enabled = true;

                    oneGoo.renderer.enabled = true;
                    twoGoo.renderer.enabled = true;
                    threeGoo.renderer.enabled = true;
                    

                    // show all the other goo too when it's down to the final hours
                }
                else
                {
                    // hide the one and two numbers
                    one.renderer.enabled = false;
                    two.renderer.enabled = false;
                }

                break;

            // reaching the park
            case 8:
                SetText("Used to play in this park often as a kid. This took two hours?");
                UpdateTimeLeft(2);

                // hide/show trees here
                // tree 1 stuff
                if (MainManager.tree1Used == true)
                {
                    // hide the tree, show the logs (don't need to show the stump since they were originally
                    // behind the trees)
                    GameObject tempTree = GameObject.Find("Tree1");
                    tempTree.renderer.enabled = false;
                    GameObject tempLog1 = GameObject.Find("Log1_1");
                    MoveLog(tempLog1, -2.5f);
                    GameObject tempLog2 = GameObject.Find("Log1_2");
                    MoveLog(tempLog2, -2.5f);
                }
                else // if the tree has NOT been used, show the tree, hide the logs
                {
                    GameObject tempTree = GameObject.Find("Tree1");
                    tempTree.renderer.enabled = true;
                    // move hte logs behind the background to hide htem and so they can't be clicked on
                    GameObject tempLog1 = GameObject.Find("Log1_1");
                    MoveLog(tempLog1, 1.0f);
                    GameObject tempLog2 = GameObject.Find("Log1_2");
                    MoveLog(tempLog2, 1.0f);
                }
                
                // tree 2 stuff
                if(MainManager.tree2Used == true)
                {
                    // hide the tree, show the logs (don't need to show the stump since they were originally
                    // behind the trees)
                    GameObject tempTree = GameObject.Find("Tree2");
                    tempTree.renderer.enabled = false;
                    GameObject tempLog1 = GameObject.Find("Log2_1");
                    MoveLog(tempLog1, -2.4f);
                    GameObject tempLog2 = GameObject.Find("Log2_2");
                    MoveLog(tempLog2, -2.4f);
                }
                else // if the tree has NOT been used, show the tree, hide the logs
                {
                    GameObject tempTree = GameObject.Find("Tree2");
                    tempTree.renderer.enabled = true;
                    GameObject tempLog1 = GameObject.Find("Log2_1");
                    MoveLog(tempLog1, 1.0f);
                    GameObject tempLog2 = GameObject.Find("Log2_2");
                    MoveLog(tempLog2, 1.0f);
                }
                
                // tree 3 stuff
                if(MainManager.tree3Used == true)
                {

                    // hide the tree, show the logs (don't need to show the stump since they were originally
                    // behind the trees)
                    GameObject tempTree = GameObject.Find("Tree3");
                    tempTree.renderer.enabled = false;
                    GameObject tempLog1 = GameObject.Find("Log3_1");
                    MoveLog(tempLog1, -3.5f);                    
                    GameObject tempLog2 = GameObject.Find("Log3_2");
                    MoveLog(tempLog2, -3.5f);
                }
                else // if the tree has NOT been used, show the tree, hide the logs
                {
                    GameObject tempTree = GameObject.Find("Tree3");
                    tempTree.renderer.enabled = true;
                    GameObject tempLog1 = GameObject.Find("Log3_1");
                    MoveLog(tempLog1, 1.0f);
                    GameObject tempLog2 = GameObject.Find("Log3_2");
                    MoveLog(tempLog2, 1.0f);
                }

                // TODO
                // add the checking of the used logs list to determine which logs to show/hide
                break;

            // reaching the library
            case 9:
                SetText("There's good information here. I might take a while.");
                UpdateTimeLeft(4); // 4
                break;

            // reaching the dock
            case 10:
                // TODO
                // maybe check if the raft is ready or not here
                SetText("Always wanted to go down the river.");
                UpdateTimeLeft(3); //3

                // check if the raft has been built or not.
                // if it has, show the raft, else, hide it
                GameObject raft = GameObject.Find("Raft");
                if(MainManager.raftBuilt)
                {                    
                    raft.transform.position = new Vector3(raft.transform.position.x, raft.transform.position.y, -3.5f);

                    // hide all the logs
                    foreach (string logName in MainManager.storedLogs)
                    {
                        GameObject tempLog = GameObject.Find(logName);
                        Destroy(tempLog);
                    }
                }
                else
                {
                    raft.transform.position = new Vector3(raft.transform.position.x, raft.transform.position.y, 1.0f);
                }
                break;

            // reaching the market
            case 11:
                SetText("This just might be my only source of food now.");
                UpdateTimeLeft(3);
                break;

            // reahcing the police station
            case 12:
                SetText("Creepy that it's empty. Useful stuff here though, assuming they never come back.");
                UpdateTimeLeft(2);

                //hide/show the broken axe glass and the axe
                if(MainManager.axeGrabbed == true)
                {
                    // hide the axe and normal glass, show the broken glass
                    GameObject tempAxe = GameObject.Find("Axe");
                    tempAxe.renderer.enabled = false;

                    GameObject tempGlass = GameObject.Find("Glass");
                    tempGlass.renderer.enabled = false;

                    GameObject tempBrokenGlass = GameObject.Find("BrokenGlass");
                    tempBrokenGlass.renderer.enabled = true;

                }
                // the axe hasn't been grabbed so show the axe and normal glass, hide the broken glass
                else
                {
                    // show the axe and normal glass, hide the broken glass
                    GameObject tempAxe = GameObject.Find("Axe");
                    tempAxe.renderer.enabled = true;

                    GameObject tempGlass = GameObject.Find("Glass");
                    tempGlass.renderer.enabled = true;

                    GameObject tempBrokenGlass = GameObject.Find("BrokenGlass");
                    tempBrokenGlass.renderer.enabled = false;
                }
                break;
            
            // reaching the end scene
            case 13:
                loadEnd = false;
                // show the final time passed as the initial time available minus the time that was left
                if(MainManager.days < 0)
                {
                    // if all time was used, set the values to their starting points
                    MainManager.days = 3;
                    MainManager.hours = 2;
                }
                else if (MainManager.days < 3)
                {
                    MainManager.days = 3 - MainManager.days;
                    MainManager.hours = 24 - MainManager.hours;
                }
                
                
                break;

            default:
                break;
        }
        

        

    }

	// Update is called once per frame
	void Update () {

        // block handling text and text background
        if(showingText)
        {
            // Display the neccesary text
            currentTime = Time.time;

            if (loadedTime != 0.0f)
            {
                // set the text to be "blank"
                if (Mathf.Abs(currentTime - loadedTime) > timeToShow)
                {
                    loadedTime = 0.0f;
                    lookText.text = "";
                    textBackground.enabled = false;

                }

                // display the text background and the text itself
                else
                {
                    textBackground.enabled = true;
                    lookText.text = objectText;
                }

            }

            lookTextShadow.text = lookText.text;
        }
        else
        {
            lookText.text = "";
            lookTextShadow.text = lookText.text;
            textBackground.enabled = false;

        }
        
        // maybe add a block here to check for certain levels to be tinted or not

        // block handling color overlay display
        if (MainManager.hours == 6 || MainManager.hours == 7)
        {
            // enable dawn overlay
            dawnOverlay.renderer.enabled = true;
            sunsetOverlay.renderer.enabled = false;
            nightOverlay.renderer.enabled = false;
        }
        else if (MainManager.hours == 17 || MainManager.hours == 18)
        {
            // enable sunset overlay
            sunsetOverlay.renderer.enabled = true;
            dawnOverlay.renderer.enabled = false;
            nightOverlay.renderer.enabled = false;
        }
        else if (MainManager.hours <= 5 || MainManager.hours >= 19)
        {
            // enable night overlay
            nightOverlay.renderer.enabled = true;
            sunsetOverlay.renderer.enabled = false;
            dawnOverlay.renderer.enabled = false;
        }
        else
        {
            // normal display, don't show any overlays
            nightOverlay.renderer.enabled = false;
            sunsetOverlay.renderer.enabled = false;
            dawnOverlay.renderer.enabled = false;
        }


          

        //-----------------------------------------------------------------------------
        // block for item handling
        //-----------------------------------------------------------------------------

        // display text letting the player know that they are trying to use an item with a certain object
        // if an item is being used (set by the use icon above a usable item)
        if (MainManager.itemInUse == true)
        {
            SetText("Use " + MainManager.itemBeingUsed + " with...");
        }
        
        // make a ray at the mouse's location and check the item that it clicked on
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // What to do when a certain item is clicked on hitboxes is found
        // INCLUDE HERE
        // interactions like saw and tree, or knife and rope, etc.]

        // if we hover over an object
        if (hit)
        {
            // if the left mouse button is clicked, and NOT clicked on the initial use icon or the backpack icons
            if (hit.collider.gameObject.name != "UseIcon" && hit.collider.gameObject.name != "OpenBackpackIcon" 
                && hit.collider.gameObject.name != "ClosedBackpackIcon"
                && Input.GetMouseButtonDown(0))
            {
                // if item is still in use
                if (MainManager.itemInUse == true)
                {
                    // if the axe is used on the tree,
                    // hide the original tree, show the stump, and show the logs
                    if (MainManager.itemBeingUsed == "Axe")
                    {
                        // if one of the trees is clicked on
                        if (hit.collider.gameObject.name == "Tree1")
                        {
                            // hide the tree, show the logs (don't need to show the stump since they were originally
                            // behind the trees
                            GameObject tempTree = GameObject.Find("Tree1");
                            tempTree.renderer.enabled = false;
                            GameObject tempLog1 = GameObject.Find("Log1_1");
                            tempLog1.transform.position = new Vector3(tempLog1.transform.position.x, tempLog1.transform.position.y, -2.5f);
                            GameObject tempLog2 = GameObject.Find("Log1_2");
                            tempLog2.transform.position = new Vector3(tempLog2.transform.position.x, tempLog2.transform.position.y, -2.5f);

                            // set the bool for the tree being used to hide stuff on other loads
                            MainManager.tree1Used = true;

                            // display time taken and update time remaining
                            SetText("That took two hours, whew.");
                            UpdateTimeLeft(2);

                            // stop using the axe
                            MainManager.itemInUse = false;
                            Application.LoadLevel(Application.loadedLevel);
                            


                        }
                        else if (hit.collider.gameObject.name == "Tree2")
                        {
                            // hide the tree, show the logs (don't need to show the stump since they were originally
                            // behind the trees
                            GameObject tempTree = GameObject.Find("Tree2");
                            tempTree.renderer.enabled = false;
                            GameObject tempLog1 = GameObject.Find("Log2_1");
                            tempLog1.transform.position = new Vector3(tempLog1.transform.position.x, tempLog1.transform.position.y, -2.4f);
                            GameObject tempLog2 = GameObject.Find("Log2_2");
                            tempLog2.transform.position = new Vector3(tempLog2.transform.position.x, tempLog2.transform.position.y, -2.4f);

                            // set the bool for the tree being used to hide stuff on other loads
                            MainManager.tree2Used = true;

                            // display time taken and update time remaining
                            SetText("That took two hours, whew.");
                            UpdateTimeLeft(2);

                            // stop using the axe
                            MainManager.itemInUse = false;
                            Application.LoadLevel(Application.loadedLevel);


                        }
                        else if (hit.collider.gameObject.name == "Tree3")
                        {
                            // hide the tree, show the logs (don't need to show the stump since they were originally
                            // behind the trees
                            GameObject tempTree = GameObject.Find("Tree3");
                            tempTree.renderer.enabled = false;
                            GameObject tempLog1 = GameObject.Find("Log3_1");
                            tempLog1.transform.position = new Vector3(tempLog1.transform.position.x, tempLog1.transform.position.y, -3.5f);
                            GameObject tempLog2 = GameObject.Find("Log3_2");
                            tempLog2.transform.position = new Vector3(tempLog2.transform.position.x, tempLog2.transform.position.y, -3.5f);

                            // set the bool for the tree being used to hide stuff on other loads
                            MainManager.tree3Used = true;

                            // display time taken and update time remaining
                            SetText("That took two hours, whew.");
                            UpdateTimeLeft(2);

                            // stop using the axe
                            MainManager.itemInUse = false;
                            Application.LoadLevel(Application.loadedLevel);


                        }
                        // show the default message of I can't use that on that
                        else
                        {
                            SetText("I can't use the " + MainManager.itemBeingUsed + " with that.");
                            // stop using an item
                            MainManager.itemInUse = false;
                        }

                    }
                    // end of axe block


                    // add the other items that could be used with here
                    // like 
                    //if (MainManager.itemBeingUsed == "Knife")
                    //{
                    //}



                    // defualt to cancel item being used with
                    //else
                    //{
                    //    MainManager.itemInUse = false;
                    //}
                }

            }

            // if the build icon is clicked on
            if (hit.collider.gameObject.name == "BuildIcon" && Input.GetMouseButtonDown(0))
            {
                // show text asking if they want to build the raft and how long it will take
                // (3 hours)
                SetText("Do you want to build a raft?");
                // show the cancel and accept icons
                GameObject acceptIcon = GameObject.Find("AcceptIcon");
                acceptIcon.transform.position = new Vector3(acceptIcon.transform.position.x, acceptIcon.transform.position.y, -3.9f);
                GameObject cancelIcon = GameObject.Find("CancelIcon");
                cancelIcon.transform.position = new Vector3(cancelIcon.transform.position.x, cancelIcon.transform.position.y, -3.9f);
            }

            //
            //

            // if the accept icon is clicked
            if (hit.collider.gameObject.name == "AcceptIcon" && Application.loadedLevel == 10 && Input.GetMouseButtonDown(0))
            {
                // if the player clicks the accept icon when the raft has not been built (shows only when the build icon appears)
                if(MainManager.raftBuilt == false)
                {
                    MainManager.raftBuilt = true;

                    // hide the cancel and accept icons
                    GameObject acceptIcon = GameObject.Find("AcceptIcon");
                    acceptIcon.transform.position = new Vector3(acceptIcon.transform.position.x, acceptIcon.transform.position.y, 1.0f);
                    GameObject cancelIcon = GameObject.Find("CancelIcon");
                    cancelIcon.transform.position = new Vector3(cancelIcon.transform.position.x, cancelIcon.transform.position.y, 1.0f);

                    // update time taken
                    UpdateTimeLeft(3);

                    // reload the scene
                    Application.LoadLevel(Application.loadedLevel);
                }
                // if clicking the accept icon when the raft has been built, load the final scene
                else if(MainManager.raftBuilt == true)
                {
                    Application.LoadLevel(13);
                }
                
               
            }
            else if (hit.collider.gameObject.name == "CancelIcon" && Application.loadedLevel == 10 && Input.GetMouseButtonDown(0))
            {
                // hide the cancel and accept icons
                GameObject acceptIcon = GameObject.Find("AcceptIcon");
                acceptIcon.transform.position = new Vector3(acceptIcon.transform.position.x, acceptIcon.transform.position.y, 1.0f);
                GameObject cancelIcon = GameObject.Find("CancelIcon");
                cancelIcon.transform.position = new Vector3(cancelIcon.transform.position.x, cancelIcon.transform.position.y, 1.0f);
            }

        }
        

        

	}

    void SetText(string inputString)
    {
        showingText = true;

        loadedTime = Time.time;

        objectText = inputString;
    }

    void UpdateTimeLeft(int timeTaken)
    {
        // only subtract time if it is supposed to (aka leaving from the main map instead of going to it)
        if(MainManager.takeTime == true)
        {
            // TEMP FIX
            // for some reason, the time being taken away is doubled, so I'll just halve it for now to make it
            // the right amount to reduce by
            MainManager.hours -= (timeTaken / 2);

            // if hours left goes to zero, subtract a day and start the hours again at 24
            if (MainManager.hours <= 0)
            {
                MainManager.hours = 24 + MainManager.hours;
                MainManager.days -= 1;

                if (MainManager.days < 0 )
                {
                    loadEnd = true;
                }

            }
        }
        

    }

    // method to move the log forward or back
    // direction of 0 = back
    // direction of 1 = forward
    void MoveLog(GameObject temp, float direction)
    {
        if (temp.transform.parent.tag == "Cell")
        {
            // don't move the object if it is in the inventory
        }
        else
        {            
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, direction);           
        }
    }
}
