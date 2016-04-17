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

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        //storedFood = new List<GameObject>();
        storedFoodNames = new List<string>();
        storedFoodX = new List<float>();
        storedFoodY = new List<float>();
        storedFoodZ = new List<float>();
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
            Debug.Log(Application.loadedLevelName);
            Debug.Log("Days left: " + MainManager.days + ", hours left: " + MainManager.hours);

            // center the overlays on the camera
            sunsetOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.5f);
            nightOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.5f);
            dawnOverlay.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3.5f);

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

                break;

            // reaching the park
            case 8:
                SetText("Used to play in this park often as a kid. This took two hours?");
                UpdateTimeLeft(2);
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
}
