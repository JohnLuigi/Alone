using UnityEngine;
using System.Collections;
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
    public float timeToShow = 1.5f;

    // string to be used to display text
    public string objectText = "";

    GameObject[] inventoryArray;

    public GameObject textBackground;

	// Use this for initialization
	void Start () {

        //get the scripts necessary
        iconHandlerScript = iconHandler.GetComponent<IconHandler>();

        // initial shown text is blank (aka no text shown)
        displayText.text = "";
        shadowText.text = "";

        // initially set the textbackground to be invisible
        textBackground.renderer.enabled = false;
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
                textBackground.renderer.enabled = false;

            }

            // display the text background and the text itself
            else
            {
                textBackground.renderer.enabled = true;
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
            
            // if icon is look
            if (iconType == 0)
            {
                // output text description stored on object
                
                // if object has a description field with a string in it
                if(!String.Equals(objectPropertiesScript.description, ""))
                {
                    // set the starting time for the text
                    clickedTime = Time.time;

                    objectText = objectPropertiesScript.description;
                }
            }

            // if icon is use
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
            }

            //if icon is store
            else if (iconType == 2)
            {
                if(objectPropertiesScript.storable == true)
                {
                    inventoryArray = iconHandlerScript.invArray;

                    for (int i = 0; i < inventoryArray.Length; i++)
                    {

                        if (inventoryArray[i].transform.childCount == 0)
                        {
                            linkedObject.transform.position = new Vector3(inventoryArray[i].transform.position.x,
                                inventoryArray[i].transform.position.y, inventoryArray[i].transform.position.z - 0.5f);
                            linkedObject.transform.rotation = inventoryArray[i].transform.rotation;

                            linkedObject.transform.parent = inventoryArray[i].transform;
                            objectPropertiesScript.stored = true;
                            iconHandlerScript.beingUsed = false;
                            return;
                        }

                    }
                }

                iconHandlerScript.beingUsed = false;
               
            }

        }
    }
}
