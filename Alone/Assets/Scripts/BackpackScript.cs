using UnityEngine;
using System.Collections;

public class BackpackScript : MonoBehaviour {

    public GameObject backpack;

    public bool open;

    public bool beingUsed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            beingUsed = true;
        }
    }
}
