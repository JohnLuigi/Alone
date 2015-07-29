using UnityEngine;
using System.Collections;

public class DragScript : MonoBehaviour {

    public SpringJoint2D spring;

    void Awake()
    {
        spring = this.gameObject.GetComponent<SpringJoint2D>();

        // initial anchor point based on starting position of game object
        spring.connectedAnchor = gameObject.transform.position;
    }

    void OnMouseDown()
    {
        spring.enabled = true;
    }

    void OnMouseDrag()
    {
        if(spring.enabled == true)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            spring.connectedAnchor = cursorPosition;
        }
    }

    void OnMouseUp()
    {
        //disable on release
        spring.enabled = false;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
