using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreRegion : MonoBehaviour {

    // the two sprites used to show the cart that will be in front of and behind the stored food region
    private GameObject cartFront;
    private GameObject cartBack;

    // upon level load, the logs will be searched for and added to this array
    private GameObject[] logs;


	// Use this for initialization
	void Start () {
        // do a check for the living room here
        if(Application.loadedLevel == 3)
        {
            cartFront = GameObject.Find("CartFront");
            cartBack = GameObject.Find("CartBack");

            // hide or show the cart components in the level depending on whether it was collected in the market or not
            if (MainManager.cartGrabbed == false)
            {
                cartFront.renderer.enabled = false;
                cartBack.renderer.enabled = false;
            }

            // look to see if there are objects in the stored food array, and if there are, set the existing copies
            // in the level to have the same position as the stored objects

            // the storedFoodNames list is the same length as the x, y, and z lists
            for (int i = 0; i < MainManager.storedFoodNames.Count; i++)
            {
                // move the hidden stored objects to the food region on level load
                if (MainManager.storedFoodNames[i] != null && GameObject.Find(MainManager.storedFoodNames[i]))
                {
                    GameObject tempItem = GameObject.Find(MainManager.storedFoodNames[i]);
                    // TODO maybe alter the Z value here so that it's not above the fade to black screen
                    // fade to black screen is at z value of -4
                    //tempItem.transform.position = new Vector3(MainManager.storedFoodX[i], MainManager.storedFoodY[i], MainManager.storedFoodZ[i]);
                    tempItem.transform.position = new Vector3(MainManager.storedFoodX[i], MainManager.storedFoodY[i], -2.5f);

                }
            }
        }

        // check if the dock is loaded
        if(Application.loadedLevel == 10)
        {
            // look for the logs with the tag "Log"
            logs = GameObject.FindGameObjectsWithTag("Log");
            
            // hide the logs initially, their default state should be behind the background
            foreach (GameObject tempLog in logs)
            {
                if(tempLog.transform.parent.tag == "Cell")
                {
                    // don't move the log if it is stored in the inventory
                }
                else
                {
                    tempLog.transform.position = new Vector3(tempLog.transform.position.x, tempLog.transform.position.y, 1.0f);
                }
            }



            // iterate through the stored logs list to see if any of the hidden logs has been stored,
            // if there are any stored, then make them appear
            for(int i = 0; i < MainManager.storedLogs.Count; i++)
            {
                // check that the logs exist in the level (by default they are hidden)
                if(MainManager.storedLogs[i] != null && GameObject.Find(MainManager.storedLogs[i]))
                {
                    // find the corresponding log
                    GameObject tempLog = GameObject.Find(MainManager.storedLogs[i]);
                    // move the log's z value up so that it can be seen
                    tempLog.transform.position = new Vector3(tempLog.transform.position.x, tempLog.transform.position.y, -3.5f);
                }                
            }

            // once all 6 logs are visible, check if the rope is there
            // if the rope is there, display the crafting icon to build the raft
            // building raft takes 3 hours or so
        }
        

        //Debug.Log("This was run");
        //List<GameObject> thisFood = new List<GameObject>(MainManager.storedFood);

        //foreach(string str in MainManager.storedFoodNames)
        //{
        //    Debug.Log(str + "survived the list purge");
        //}

        //foreach(float tform in MainManager.storedFoodX)
        //{
        //    Debug.Log(tform);
        //}
        //foreach (float tform in MainManager.storedFoodY)
        //{
        //    Debug.Log(tform);
        //}
        //foreach (float tform in MainManager.storedFoodZ)
        //{
        //    Debug.Log(tform);
        //}

        // old array copy, didn't work
        // GameObject.Find("MainManager").GetComponent<MainManager>().storedFood

        //System.Array.Copy(GameObject.Find("LookIcon").GetComponent<IconScript>().tempFood, thisFood, thisFood.Length);
        //List<GameObject> tempReference = IconScript.GameObject.Find("LookIcon").GetComponent<IconScript>().tempFood;
        //thisFood = new List<GameObject>(GameObject.Find("LookIcon").GetComponent<IconScript>().tempFood);

        //thisFood = new List<GameObject>(IconScript.tempFood);
        
        //foreach (string tempObj in thisFood)
        //{
        //    if(tempObj != null)
        //    {
        //        Debug.Log(tempObj.name);
        //    }
        //    else
        //    {
        //        //Debug.Log("the list is empty");
        //    }
            
        //}


        //Debug.Log("test " + GameObject.Find("LookIcon").GetComponent<IconScript>().tempFood[0]);

        // Somewhere in this block initialize the array that contains the stored food items

        

        //for (int i = 0; i < thisFood.Length; i++)
        //{
        //    // if an object is found in the array, set the existing copy of that object to equal to the stored object's position
        //    if (thisFood[i] != null)
        //    {
        //        Debug.Log(thisFood[i].name + " was found in the storedFood array");
        //        GameObject tempItem = GameObject.Find(thisFood[i].name);
        //        tempItem.transform.position = thisFood[i].transform.position;
        //    }
        //}

        // CHECK HERE to see if the food items reappear on level loading
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
