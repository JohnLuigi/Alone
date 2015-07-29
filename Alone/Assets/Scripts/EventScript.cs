using UnityEngine;
using System.Collections;

public class EventScript : MonoBehaviour {

    // keep a single instance of this object per scene
    private static EventScript _instance;

    // set the object to not be destroyed on new scene loading
    void Awake()
    {
        //Debug.Log("created new grid");
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = this;
        //otherwise, if we do, kill this thing
        else
        {
            //Debug.Log("Error message should be right before this");
            Destroy(this.gameObject);
        }
            


        DontDestroyOnLoad(this.gameObject);

        //DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
