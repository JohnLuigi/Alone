using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreRegion : MonoBehaviour {

	// Use this for initialization
	void Start () {
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

        // look to see if there are objects in the stored food array, and if there are, set the existing copies
        // in the level to have the same position as the stored objects

        // the storedFoodNames list is the same length as the x, y, and z lists
        for (int i = 0; i < MainManager.storedFoodNames.Count; i++ )
        {
            if (MainManager.storedFoodNames[i] != null && GameObject.Find(MainManager.storedFoodNames[i]))
            {
                GameObject tempItem = GameObject.Find(MainManager.storedFoodNames[i]);
                tempItem.transform.position = new Vector3(MainManager.storedFoodX[i], MainManager.storedFoodY[i], MainManager.storedFoodZ[i]);

            }
        }

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
