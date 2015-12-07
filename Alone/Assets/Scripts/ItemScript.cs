using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        // 2D HITBOX DETECTION IS WORKING WOO HOO
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // What to do when one of the item's hitboxes is found
        if (hit)
        {
            //string destination = hit.collider.gameObject.tag;
        }
	}
}
