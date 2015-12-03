using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class InventoryManager : MonoBehaviour {


    // keep a single instance of this object per scene
    private static InventoryManager _instance;

    public BackpackScript openBackScript;
    public BackpackScript closedBackScript;



    // create list containing all the inventory cells (slots)
    //List<GameObject> cells = new List<GameObject>();
    public GameObject[] cells;

    // the icon that can be clicked on to open the inventory
    // used when backpack is closed
    public GameObject closedBackpack;
    // icon to be used when backpack is open
    public GameObject openBackpack;
    // object that is the background of the grid,
    // also has the inventory slots as children of the background
    public GameObject gridBackground;

    // used to track the original inventory, so when a new scene
    // is created and a new inventory is made, only the original survives
    public bool original = false;

    // track if the bag is open or closed
    [HideInInspector]
    public bool isOpen = false;

    // booleans for the grid
    [HideInInspector]
    public bool gridVisible = true;
    [HideInInspector]
    public bool gridMoving = false;

    // is the invetory grid moving up?
    [HideInInspector]
    public bool up = false;

    //[HideInInspector]


    // String used to record the last saved level
    [HideInInspector]
    public string lastLevel;

    // set the object to not be destroyed on new scene loading
    void Awake()
    {
        //Debug.Log("created new grid");
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = this;
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);

        //DontDestroyOnLoad(transform.gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case 0:
                lastLevel = "BedroomScene";
                break;
            case 1:
                lastLevel = "KitchenScene";
                break;
            case 2:
                lastLevel = "LivingRoomScene";
                break;
            case 3:
                lastLevel = "BasementScene";
                break;
            case 4:
                lastLevel = "HouseFront";
                break;
            case 5:
                lastLevel = "HouseFrontMap";
                break;
            case 6:
                // Do not change last level if map is loaded
                break;
            case 7:
                lastLevel = "ParkScene";
                break;
            case 8:
                lastLevel = "LibraryScene";
                break;
            case 9:
                lastLevel = "IntroScene";
                break;
            case 10:
                lastLevel = "DockScene";
                break;
            case 11:
                lastLevel = "MarketScene";
                break;
            default:
                break;
        }
        //set the lastLevel to the just loaded level (used for the map path highlighting
        //lastLevel = Application.loadedLevelName;
        //Debug.Log(Application.loadedLevelName);
    }


	// Use this for initialization
	void Start () {

        // hid the open icon, the inventory is closed by default
        openBackpack.renderer.enabled = false;
	    
        // find all the objects in the scene with
        // name containing cell and add it to the list

        cells = GameObject.FindGameObjectsWithTag("Cell").OrderBy(go => go.name).ToArray();

        // check the scene for objects that are in the inventory
        // if there is an object in the scene that is also int he inventory, delete the
        // object in the scene

        // possible save the location of the original scene object to drop the item
        // from the inventory

        //foreach(GameObject inventoryItem in cells)
        //{
        //    if(inventoryItem.transform.childCount > 0)
        //    {
        //        GameObject[] sceneItems = GameObject.FindGameObjectsWithTag("Item");

        //        foreach (GameObject sceneItem in sceneItems)
        //        {
        //            if (sceneItem.name == inventoryItem.name && sceneItem.transform.parent != inventoryItem.transform)
        //            {
        //                Destroy(sceneItem);
        //            }
        //        }
        //    }
            
        //}

        //for (int i = 0; i < cells.Length; i++ )
        //{
        //    Debug.Log(cells[i].name);
        //}

        //Debug.Log("NEW OUTPUT");



        //foreach (GameObject cell in cells)
        //{
        //    Debug.Log(cell.name);
        //}


/*
        int invCount = 0;

        // look for any other created inventory manager objects that were created 
        // in the new scene load and delete the ones that are not the original
        // (the one that manually had the original boolean set to true)
        foreach(GameObject invObject in GameObject.FindGameObjectsWithTag("Inventory"))
        {
            invCount++;
            
            InventoryManager inventory = invObject.GetComponent<InventoryManager>();
            if(inventory != null && inventory.original == false)
            {
                GameObject.Destroy(invObject);
            }
            
        }
        if(invCount > 1)
        {
            Debug.Log("grid Destroyed");
            // if there is more than one copy of the 
            GameObject.Destroy(gameObject);
        }
*/
	}
	
	// Update is called once per frame
	void Update () {

        // reset the bag to appear as it should when not being hovered over
        if (isOpen == true)
        {
            openBackpack.renderer.enabled = true;
            closedBackpack.renderer.enabled = false;

            up = false;
            moveGrid(gridBackground, up);
            gridVisible = true;

        }
        // if the bag is closed, preivew it open when hovering over it
        else if (isOpen == false)
        {
            openBackpack.renderer.enabled = false;
            closedBackpack.renderer.enabled = true;
           
            up = true;
            moveGrid(gridBackground, up);
                
        }
        // New attempt below

        if (openBackScript.beingUsed == true || closedBackScript.beingUsed == true)
        {
            // preview the bag opening
            // if bag is open, preview it closed
            if (isOpen == true)
            {
                openBackpack.renderer.enabled = false;
                closedBackpack.renderer.enabled = true;

            }

            // if the bag is closed, preivew it open when hovering over it
            else if (isOpen == false)
            {
                openBackpack.renderer.enabled = true;
                closedBackpack.renderer.enabled = false;
            }

            // if the player clicks on the backpack, swap the value of isOpen
            // to open/close the backpack
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("clicked on the backpack");
                if (isOpen == true)
                {
                    isOpen = false;
                    openBackScript.beingUsed = false;
                    closedBackScript.beingUsed = false;
                }
                else
                {
                    isOpen = true;
                    openBackScript.beingUsed = false;
                    closedBackScript.beingUsed = false;
                }

            }
        }
        


        ////handle the moving of the grid here
        
        
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //if (hit != null && hit.collider != null)
        //{
        //    // see if the clicked on object is the closed backpack icon
        //    // by checking if it matches the boxcollider for the closed backpack
        //    if (hit.collider.gameObject.name == closedBackpack.name)
        //    {
        //        //Debug.Log("over the backpack");

        //        // preview the bag opening
        //        // if bag is open, preview it closed
        //        if(isOpen == true)
        //        {
        //            openBackpack.renderer.enabled = false;
        //            closedBackpack.renderer.enabled = true;
                    
        //        }

        //        // if the bag is closed, preivew it open when hovering over it
        //        else if(isOpen == false)
        //        {
        //            openBackpack.renderer.enabled = true;
        //            closedBackpack.renderer.enabled = false;
        //        }

        //        // if the player clicks on the backpack, swap the value of isOpen
        //        // to open/close the backpack
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            Debug.Log("clicked on the backpack");
        //            if(isOpen == true)
        //            {
        //                isOpen = false;
        //            }
        //            else
        //            {
        //                isOpen = true;
        //            }
                    
        //        }
        //    }

        //    // This line shows the name of the gameObject clicked on
        //    //Debug.Log(hit.collider.gameObject.name);
        //}
        
       

        // show contents of each cell
	    if(gridVisible)
        {

        }
	}

    

    void swapIcons()
    {
        // if bag is open, close the backpack
        if (isOpen == true)
        {
            openBackpack.renderer.enabled = false;
            closedBackpack.renderer.enabled = true;
            isOpen = false;
        }

        // if bag is closed, open the backpack
        else
        {
            openBackpack.renderer.enabled = true;
            closedBackpack.renderer.enabled = false;
            isOpen = true;
        }
    }

    // move the grid and cells accordingly
    void moveGrid(GameObject theGrid, bool up)
    {
        if(up == false && theGrid.transform.position.y >= 2.0f)
        {
            theGrid.transform.position -= new Vector3(0.0f, 0.20f, 0.0f);
        }
        else if(up == true && theGrid.transform.position.y <= 5.0f)
        {
            theGrid.transform.position += new Vector3(0.0f, 0.20f, 0.0f);
        }
    }

}
