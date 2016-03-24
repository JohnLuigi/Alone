using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public static int days = 3;
    public static int hours = 24;

    // static ints that track the days and hours left
    // they will change depending on the actions you take, and can only decrease

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
	
	}

    // do something when a level is loaded
    void OnLevelWasLoaded(int level)
    {
        if (level != null)
        {
            Debug.Log("Level " + level + " was loaded");
        }

    }

	// Update is called once per frame
	void Update () {
	    

	}
}
