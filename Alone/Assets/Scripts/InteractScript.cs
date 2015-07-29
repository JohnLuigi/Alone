using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

    public GameObject iconHandlerObject;

    ObjectProperties objectPropertiesScript;
    IconHandler iconHandlerScript;

    IconScript lookIconScript;
    IconScript useIconScript;
    IconScript storeIconScript;

	// Use this for initialization
	void Start () {

        // set the script objects
        objectPropertiesScript = this.GetComponent<ObjectProperties>();
        iconHandlerScript = GameObject.Find("IconHandler").GetComponent<IconHandler>();
        
	}

    void OnMouseOver()
    {
        //overObject = true;

        // On clicking object, set icon positions to object center, 
        // then make them visible and scale them to full size while
        // moving out from center
        if (Input.GetMouseButtonDown(0))
        {

            if(objectPropertiesScript.isBackground == true)
            {
               iconHandlerScript.beingUsed = false;
            }
            else if (objectPropertiesScript.visible == true)
            {
                iconHandlerScript.startPos = this.transform.position;
                iconHandlerScript.beingUsed = true;
                iconHandlerScript.firstClicked = true;

                // get the icon scripts
                lookIconScript = iconHandlerScript.lookIcon.GetComponent<IconScript>();
                useIconScript = iconHandlerScript.useIcon.GetComponent<IconScript>();
                storeIconScript = iconHandlerScript.storeIcon.GetComponent<IconScript>();

                // set the linked object of the icon scripts to be the currently
                // click object

                lookIconScript.linkedObject = this.gameObject;
                useIconScript.linkedObject = this.gameObject;
                storeIconScript.linkedObject = this.gameObject;

            }
            

        }
    }

    void OnMouseExit()
    {
        //overObject = false;        
    }
	
	// Update is called once per frame
	void Update () {

	}
}
