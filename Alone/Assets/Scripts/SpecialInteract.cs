using UnityEngine;
using System.Collections;

public class SpecialInteract : MonoBehaviour {

    // This script is for objects that have unqiue interactions, like grabbing the cart or using the boat

    //private bool pressed = false;

    // This chunk of text seems to work for making object duplicates not reappear upon revisiting a scene
    void Awake()
    {
        if (GameObject.Find("IconHandler").GetComponent<IconHandler>().cartGrabbed == true)
        {
            Destroy(this.gameObject);
        }
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        //// Picking up the name of the boject clicked on
        //// there will be specific actions done depending on the item clicked
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //// What to do when one of the special objects' hitboxes is found
        //if (hit)
        //{
        //    // If the cart is clicked on
        //    if(hit.collider.gameObject.name == "Cart")
        //    {
        //        if(Input.GetMouseButtonDown(0))
        //        {    
        //            // set the rest of the cells in the inventory to be rendered

        //            // find the object with the inventorymanager script, then get the inventory cells
        //            InventoryManager invManager = GameObject.Find("InventoryHandler").GetComponent<InventoryManager>();
        //            GameObject[] inventoryArray = invManager.GetComponent<InventoryManager>().cells;

        //            Debug.Log(inventoryArray.Length);
        //            if(inventoryArray[4].renderer.enabled == false)
        //            {
        //                for (int i = 4; i < inventoryArray.Length; i++)
        //                {
        //                    inventoryArray[i].renderer.enabled = true;
        //                }
        //            }
        //            //else
        //            //{
        //            //    for (int i = 4; i < inventoryArray.Length; i++)
        //            //    {
        //            //        inventoryArray[i].renderer.enabled = false;
        //            //    }
        //            //}
                    
        //        }
                
        //    }
        //}
	}
}
