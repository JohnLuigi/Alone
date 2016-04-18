using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreRegion : MonoBehaviour {

    // the two sprites used to show the cart that will be in front of and behind the stored food region
    private GameObject cartFront;
    private GameObject cartBack;

	// Use this for initialization
	void Start () {

        cartFront = GameObject.Find("CartFront");
        cartBack = GameObject.Find("CartBack");

        // hide or show the cart components in the level depending on whether it was collected in the market or not
        if(MainManager.cartGrabbed == false)
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
