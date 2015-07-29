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

    public bool stored = false;

    // later add things like weight
    // value
    // clickable
    // storable
    // information

	// Use this for initialization
	void Start () {
        
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
                    Debug.Log("destroyed copy");
                    Destroy(this.gameObject);
                    return;
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
        }
        // else, render the object
        else
        {
            renderer.enabled = true;
        }

	}
}
