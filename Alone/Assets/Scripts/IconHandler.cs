using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconHandler : MonoBehaviour {

    // keep a single instance of this object per scene
    private static IconHandler _instance;

    // Icons to be used in the rest of the scene
    public GameObject lookIcon;
    public GameObject useIcon;
    public GameObject storeIcon;

    // frames to travel out from object center to icon final positions
    public float travelTime = 15.0f;

    // hides the variable below in the inspector
    //[HideInInspector]
    public bool beingUsed = false;
    public bool firstClicked = false;

    [HideInInspector]
    public Vector3 startPos = new Vector3(0.0f, 0.0f, -5.0f);

    public GameObject invManager;
    [HideInInspector]
    public InventoryManager invManagerScript;

    [HideInInspector]
    public GameObject[] invArray;// = new GameObject[16];

    // tracks if the cart was used and therefore should be rendered when visiting the scene
    [HideInInspector]
    public bool cartGrabbed = false;

    // expanding list of stored food items
    // trying a list now instead of an array
    [HideInInspector]
    public List<GameObject> storedFood;

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

        // set the inventory manager script
        invManagerScript = invManager.GetComponent<InventoryManager>();

        // set the script's inventory cells
        invArray = invManager.GetComponent<InventoryManager>().cells;

        // initialize the storedFood array
        // make it big enough that the player could not fill it, so maybe 100 size?
        storedFood = new List<GameObject>();

        
    }

	// Use this for initialization
	void Start () {

        

        lookIcon.renderer.enabled = false;
        useIcon.renderer.enabled = false;
        storeIcon.renderer.enabled = false;

        //Debug.Log(invManager.GetComponent<InventoryManager>().cells.Length);

        //Debug.Log(invArray.Length);

        //for (int i = 0; i < invArray.Length; i++)
        //{
        //    Debug.Log(invArray[i].name);
        //}

        
	}
	
	// Update is called once per frame
	void Update () {

	    if(beingUsed == true)
        {
            lookIcon.renderer.enabled = true;
            useIcon.renderer.enabled = true;
            storeIcon.renderer.enabled = true;

            //movement block

            // if this is the first click on hte object, move each
            // icon to the starting position (aka the center of the clikced object)
            if(firstClicked == true)
            {
                lookIcon.transform.position = startPos;
                useIcon.transform.position = startPos;
                storeIcon.transform.position = startPos;
                firstClicked = false;
            }

            //move up to the desired icon location
            else if (lookIcon.transform.position.x > startPos.x - 0.8f)
            {
                lookIcon.transform.position += new Vector3(-0.8f / travelTime, 0.6f / travelTime, 0.0f);
                useIcon.transform.position += new Vector3(0.0f, 1.0f / travelTime, 0.0f);
                storeIcon.transform.position += new Vector3(0.8f / travelTime, 0.6f / travelTime, 0.0f);
            }



        }
        else 
        {
            beingUsed = false;
            lookIcon.renderer.enabled = false;
            useIcon.renderer.enabled = false;
            storeIcon.renderer.enabled = false;
        }

        

	}
}
