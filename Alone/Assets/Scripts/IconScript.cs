using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class IconScript : MonoBehaviour {

    // Which icon type is the object
    // 0 = look
    // 1 = use
    // 2 = store
    public int iconType = 0;

    [HideInInspector]
    public GameObject linkedObject;

    public GameObject iconHandler;
    IconHandler iconHandlerScript;

    ObjectProperties objectPropertiesScript;

    // Text object to be used to display text on screen when look icon is clicked on
    public Text displayText;
    public Text shadowText;

    // timers used to count how long the text should be shown
    private float clickedTime = 0.0f, currentTime = 0.0f;

    // seconds to show the text
    private float timeToShow = 2.5f;

    // string to be used to display text
    public string objectText = "";

    GameObject[] inventoryArray;

    public Image textBackground;

    InventoryManager invManager;

    // game object that is just a hitbox to store the food in
    [HideInInspector]
    public GameObject foodRegion;

    
    //public static List<GameObject> tempFood;


    //Camera camera;

    void Awake()
    {
        //tempFood = new List<GameObject>(GameObject.Find("MainManager").GetComponent<MainManager>().storedFood);
        //tempFood = new List<GameObject>(MainManager.storedFood);
        DontDestroyOnLoad(transform.gameObject);

        //get the scripts necessary
        iconHandlerScript = iconHandler.GetComponent<IconHandler>();

        invManager = GameObject.Find("InventoryHandler").GetComponent<InventoryManager>();
        //inventoryArray = iconHandlerScript.invArray;
        inventoryArray = invManager.GetComponent<InventoryManager>().cells;

        // set the reference to the temporary food object from the icon handler script
        //tempFood = new List<GameObject>(iconHandler.GetComponent<IconHandler>().storedFood);
        
    }

    void OnLevelWasLoaded(int level)
    {
        // hide the icons upon a new level load if they wre showing when leaving the previous level
        if(level >= 0)
        {
            iconHandlerScript.beingUsed = false;
        }
        if(level == 3)
        {
            //tempFood = new List<GameObject>(GameObject.Find("MainManager").GetComponent<MainManager>().storedFood);

            // find the food region in the living room scene
            if (GameObject.Find("FoodRegion"))
            {
                foodRegion = GameObject.Find("FoodRegion");
            }

        }

        
    }

	// Use this for initialization
	void Start () {

        

        // initial shown text is blank (aka no text shown)
        displayText.text = "";
        shadowText.text = "";

        // initially set the textbackground to be invisible
        //textBackground.renderer.enabled = false;
        textBackground.enabled = false;

        


        // position the text background and tet object relative to the screen
        // TODO RESUME HERE
        //Transform tempTransform = GameObject.Find("NewText").GetComponent<Transform>();
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(tempTransform.position);
        //Debug.Log("target is " + screenPos.x + " pixels from the left");
	}
	
	// Update is called once per frame
	void Update () {

        // Display the neccesary text
        currentTime = Time.time;

        if (clickedTime != 0.0f)
        {
            // set the text to be "blank"
            if (Mathf.Abs(currentTime - clickedTime) > timeToShow)
            {
                clickedTime = 0.0f;
                displayText.text = "";
                textBackground.enabled = false;

            }

            // display the text background and the text itself
            else
            {
                textBackground.enabled = true;
                // originally had 
                // displayText.text = "That's the " + objectText + ".";
                displayText.text =  objectText;
            }

        }

        shadowText.text = displayText.text;
        
	}

    // if the icon is clicked on
    void OnMouseOver()
    {
        // if the icon is left clicked
        if (Input.GetMouseButtonDown(0))
        {
            objectPropertiesScript = linkedObject.GetComponent<ObjectProperties>();

            // ----------------------------------------------------------------------------------------------------------
            // if icon is look
            // ----------------------------------------------------------------------------------------------------------
            if (iconType == 0)
            {
                // check if the item is a certain type, otherwise do the standard description output            

                // block to handle the calendar when it is clicked on and stored in the inventory
                if (linkedObject.name == "Calendar")
                {
                    clickedTime = Time.time;

                    //TODO
                    // add different types of handling for one hour or multiple hours for grammar
                    // show this if the calendar is in the inventory
                    if(linkedObject.transform.parent.tag == "Cell")
                    {
                        string daysText = " days and ";
                        if(MainManager.days == 1)
                        {
                            daysText = " day and ";
                        }
                        string hoursText = " hours left. Weird.";
                        if(MainManager.hours == 1)
                        {
                            hoursText = " hour left. Weird.";
                        }
                        objectText = "It says I have " + MainManager.days + daysText + MainManager.hours + hoursText;
                    }
                    // otherwise show this text
                    else
                    {
                        objectText = "That calendar corresponds with that weird number on my door. I can take it with me.";
                    }
                    
                }

                // output text description stored on object                
                // if object has a description field with a string in it
                else if (!String.Equals(objectPropertiesScript.description, ""))
                {
                    // set the starting time for the text
                    clickedTime = Time.time;

                    objectText = objectPropertiesScript.description;
                }

                iconHandlerScript.beingUsed = false;
            }

            // ----------------------------------------------------------------------------------------------------------
            // if icon is use
            // ----------------------------------------------------------------------------------------------------------
            else if (iconType == 1)
            {
                // carry out the proper action based ont he object's properties

                // if the use type is 1, AKA swap
                // swap the visibility from on to off or vice versa
                if(objectPropertiesScript.useType == 1)
                {
                    SwapScript swapScript = linkedObject.GetComponent<SwapScript>();

                    swapScript.firstClicked = true;

                    //if (swapScript.status == true)
                    //{
                    //    swapScript.status = false;
                    //}
                    //else
                    //{
                    //    swapScript.status = true;
                    //}

                    iconHandlerScript.beingUsed = false;
                    
                }

                // CART BLOCK
                // check if the object being used is the cart, and if it is, hide the cart and make the
                // extra inventory slots usable

                if(linkedObject.name == "Cart")
                {
                    // find the object with the inventorymanager script, then get the inventory cells

                    // TEMP disabling the reassigning of invManager and the array to see if this works
                    //InventoryManager invManager = GameObject.Find("InventoryHandler").GetComponent<InventoryManager>();
                    //GameObject[] inventoryArray = invManager.GetComponent<InventoryManager>().cells;

                    //Debug.Log(inventoryArray.Length);
                    if (inventoryArray[4].renderer.enabled == false)
                    {
                        for (int i = 4; i < inventoryArray.Length; i++)
                        {
                            inventoryArray[i].renderer.enabled = true;
                        }
                    }

                    GameObject Cart = GameObject.Find("Cart");
                    Destroy(Cart);

                    iconHandlerScript.cartGrabbed = true;
                    MainManager.cartGrabbed = true;

                }

                // INVENTORY ITEM USE BLOCK
                // add the knife, saw, axe, etc. here
                if (linkedObject.name == "Knife" || linkedObject.name == "Saw" || linkedObject.name == "Axe")
                {
                    MainManager.itemInUse = true;
                    MainManager.itemBeingUsed = linkedObject.name;
                }

                iconHandlerScript.beingUsed = false;
            }
            // ----------------------------------------------------------------------------------------------------------
            //if icon is store
            // ----------------------------------------------------------------------------------------------------------

            else if (iconType == 2)
            {
                // This block is for food items that are already in the inventory
                // handling a food item in the inventory being used and tried to be stored in the living room
                if (objectPropertiesScript.isFood == true && GameObject.Find("FoodRegion"))
                {

                    //Debug.Log(foodRegion.collider2D.bounds.size);
                    linkedObject.transform.position = new Vector3(
                    UnityEngine.Random.Range(foodRegion.transform.position.x - (foodRegion.collider2D.bounds.size.x / 2),
                    foodRegion.transform.position.x + (foodRegion.collider2D.bounds.size.x / 2)),

                    UnityEngine.Random.Range(foodRegion.transform.position.y - (foodRegion.collider2D.bounds.size.y / 2),
                    foodRegion.transform.position.y + (foodRegion.collider2D.bounds.size.y / 2)),

                    //linkedObject.transform.position.z
                    foodRegion.transform.position.z
                    );

                    Debug.Log(linkedObject.name + "was added to the list");

                    //tempFood.Add(Instantiate(linkedObject) as GameObject);

                    //GameObject.Find("MainManager").GetComponent<MainManager>().storedFood.Add(Instantiate(linkedObject) as GameObject);
                    //MainManager.storedFood.Add(Instantiate(linkedObject) as GameObject);
                    //GameObject.Find("MainManager").GetComponent<MainManager>().storedFoodNames.Add(linkedObject.name);
                    MainManager.storedFoodNames.Add(linkedObject.name);
                    MainManager.storedFoodX.Add(linkedObject.transform.position.x);
                    MainManager.storedFoodY.Add(linkedObject.transform.position.y);
                    MainManager.storedFoodZ.Add(linkedObject.transform.position.z);

                    linkedObject.transform.parent = null; // un-parent the food item from the inventory cell                    

                    // set the starting time for the text
                    clickedTime = Time.time;
                    // remove the number from the end of the food's name
                    string tempString = linkedObject.name;
                    tempString = tempString.Remove(tempString.Length - 1);

                    objectText = "I'll store this " + tempString + " for my journey.";

                    iconHandlerScript.beingUsed = false;


                    

                    return;
                }
                
                // if the player tries to drop the logs, they have to be in the dock scene where the log storing region is
                // also make sure that the log being dropped is in the inventory with a Cell parent and not one of the logs on the ground
                else if(linkedObject.tag == "Log" && GameObject.Find("LogStoreRegion") && linkedObject.transform.parent.tag == "Cell")
                {
                    // temporary log name to be used to search for thehidden log of the same name as the log being dropped
                    string tempLogName = linkedObject.name;

                    // destroy the linked object
                    Destroy(linkedObject);

                    // add the corresponding log to the stored logs in the MainManager
                    MainManager.storedLogs.Add(tempLogName);

                    // show the corresponding log that was hidden behind the background
                    GameObject[] tempHiddenLogs = GameObject.FindGameObjectsWithTag("Log");
                    foreach(GameObject aLog in tempHiddenLogs)
                    {
                        if(aLog.name == tempLogName)
                        {                         
                            aLog.transform.position = new Vector3(aLog.transform.position.x, aLog.transform.position.y, -3.5f);
                        }                        
                    }         
                    
                    // set the starting time for the text
                    clickedTime = Time.time;
                    objectText = "I'll put this log here to build something with it later.";
                    iconHandlerScript.beingUsed = false;
                    return;
                }

                // block for items that are lying in the world to be stored
                else if(objectPropertiesScript.storable == true)
                {
                    //
                    // TODO
                    //inventoryArray = iconHandler.getComponent<>invManagerScript.getComponent<InventoryManager>()

                    // if trying to store a log, check that the player has grabbed the cart to carry the log, 
                    // otherwise do not pick it up
                    if (linkedObject.tag == "Log")
                    {
                        // if the player has grabbed the cart and thus expanded their inventory, store the item
                        if (MainManager.cartGrabbed)
                        {
                            // continue the loop to store the log
                        }
                        // if the cart was not grabbed, display a message
                        else
                        {
                            // set the starting time for the text
                            clickedTime = Time.time;

                            objectText = "I could move that log around if I had something to carry it in, like a cart.";
                            iconHandlerScript.beingUsed = false;
                            return;
                        }
                    }

                    for (int i = 0; i < inventoryArray.Length; i++)
                    {

                        // checks if the cell has no objects attached (aka items stored in it) and that it is selectable (such as if the inventory is
                        // expanded by the cart or not)
                        if (inventoryArray[i].transform.childCount == 0 && inventoryArray[i].renderer.enabled == true)
                        {
                            //move the item to the inventory cell (in front of the cell too on hte z-axis
                            linkedObject.transform.position = new Vector3(inventoryArray[i].transform.position.x,
                                inventoryArray[i].transform.position.y, inventoryArray[i].transform.position.z - 1.0f);

                            // rotate the item to match the inventory cell
                            //linkedObject.transform.rotation = inventoryArray[i].transform.rotation;

                            // scale the item to match the inventory cell

                            //float scale = 0.5f;
                            //linkedObject.transform.localScale = new Vector3(scale, scale, scale);

                            //Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;

                            Vector2 cellSize = inventoryArray[i].GetComponent<SpriteRenderer>().sprite.bounds.size;
                            Vector2 linkedSize = linkedObject.GetComponent<SpriteRenderer>().sprite.bounds.size;

                            float scale = Mathf.Min(cellSize.x, cellSize.y)/ Mathf.Max(linkedSize.x, linkedSize.y);
                            linkedObject.transform.localScale = new Vector3(scale, scale, scale);


                            //Vector2 sizeModifier = new Vector2(cellSize.x / linkedSize.x, cellSize.y / linkedSize.y);

                            //linkedObject.GetComponent<PolygonCollider2D>().size = new Vector2(cellSize.x, cellSize.y);
                            //linkedObject.transform.localScale = inventoryArray[i].renderer.bounds.size;

                            linkedObject.transform.parent = inventoryArray[i].transform;

                            // if the axe was grabbed, set the boolean in MainManager to true
                            if(linkedObject.name == "Axe")
                            {
                                MainManager.axeGrabbed = true;
                            }  

                            objectPropertiesScript.stored = true;
                            iconHandlerScript.beingUsed = false;
                            // set the starting time for the text
                            clickedTime = Time.time;
                            if(MainManager.cartGrabbed)
                            {
                                // since the logs have weird names, need to output this exact text.
                                if(linkedObject.tag == "Log")
                                {
                                    objectText = "I put the log in the cart.";
                                }
                                else
                                {
                                    if(objectPropertiesScript.isFood)
                                    {
                                        string tempString = linkedObject.name;
                                        tempString = tempString.Remove(tempString.Length - 1);

                                        objectText = "I put the " + tempString + " in the cart.";
                                    }
                                    else
                                    {
                                        objectText = "I put the " + linkedObject.name + " in the cart.";

                                    }
                                }
                            }
                            else
                            {
                                if (objectPropertiesScript.isFood)
                                {
                                    string tempString = linkedObject.name;
                                    tempString = tempString.Remove(tempString.Length - 1);

                                    objectText = "I put the " + tempString + " in my backpack.";
                                }
                                else
                                {
                                    objectText = "I put the " + linkedObject.name + " in my backpack";
                                }

                            }
                            iconHandlerScript.beingUsed = false;
                            return;
                        }

                    }

                    iconHandlerScript.beingUsed = false;
                }


                iconHandlerScript.beingUsed = false;
               
            }

        }
    }
}
