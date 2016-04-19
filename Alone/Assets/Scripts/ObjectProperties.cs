using UnityEngine;
using System.Collections;
using System.Linq;

public class ObjectProperties : MonoBehaviour {

    // can the object be seen
    public bool visible = true;

    // is the object (a switch, etc.) activated?
    public bool on = true;

    // is the object the background?
    // (if this is clicked on, the icons are hidden)
    public bool isBackground = false;

    // Different actions that occur when the object is used
    // includes the following:
    // none = 0 = nothing occurs when "used" (possible mesage pops up saying "I can't use that")
    // swap = 1 = changes between states such as open or closed (toggles visibility with another object)
    public int useType = 0;

    // Text to be output when the object is "looked at" via an icon click
    public string description;

    public bool storable = false;
    public bool isFood = false;

    public bool stored = false;

    // later add things like weight
    // value
    // clickable
    // storable
    // information

	// Use this for initialization
	void Start () {

        foreach(string str in MainManager.storedFoodNames)
        {
            // delete the item that was stored if it is not on the level that it was stored in (in this case
            // the lving room aka level 3)
            if(this.name == str && Application.loadedLevel != 3)
            {
                Destroy(this.gameObject);
            }
        }
	}

    void Awake()
    {
        //if (storable == true && stored == false)
        //    Destroy(this.gameObject);

        // look for inventory system's cells that hold the items
        // then look for this object in the cells
        // if this object can be found int he cells, do not create it at the start 
        // of the scene
        
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell").OrderBy(go => go.name).ToArray();
        // FIX THIS SO DUPLICATES WORK
        foreach (GameObject item in cells)
        {
            // look at the children of each cell (can only contain one child, aka an item)
            foreach (Transform child in item.transform)
            {
                if (child.name == this.name)
                {
                    
                    // check that this item is not in the cell itself, but in the world
                    if(this.gameObject.transform.parent.tag == "Cell")
                    {                        
                        // do nothing if the object is in the inventory
                    }                    
                    else if(this.gameObject.tag == "Log" && Application.loadedLevel == 10)
                    {
                        // do not destroy the log if it is hiding in the dock scene
                    }
                    else
                    {
                        // destroy the object if it is in the gmae world
                        Debug.Log("destroyed copy");
                        Destroy(this.gameObject);
                    }
                    
                    return;
                }
            }

        }

        // check if the log was used previously and thus stored in the MainManager
        // also check if it is loading in the park
        if(this.gameObject.tag == "Log" && Application.loadedLevel == 8)
        {
            // check the stored logs in the MainManager
            foreach(string tempStr in MainManager.storedLogs)
            {
                if(this.gameObject.name == tempStr)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        // if object is not visible, do not render it
	    if(visible == false)
        {
            renderer.enabled = false;
            // if the object has a polygon collider, disable it (mostly being used for the cabinets
            if (this.gameObject.GetComponent<PolygonCollider2D>())
            {
                this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
            
        }
        // else, render the object
        else
        {
            renderer.enabled = true;
            // if the object has a polygon collider, reenable it (mostly being used for the cabinets
            if (this.gameObject.GetComponent<PolygonCollider2D>())
            {
                this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }

	}
}
