using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapScript : MonoBehaviour {

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    //List<GameObject> objects = new List<GameObject>();

    public bool status = true;

    [HideInInspector]
    public bool firstClicked = false;

    ObjectProperties objectPropertiesScript1;
    //ObjectProperties objectPropertiesScript2;

    List<ObjectProperties> objectScripts = new List<ObjectProperties>();

	// Use this for initialization
	void Start () {
        objectPropertiesScript1 = object1.GetComponent<ObjectProperties>();
        //objectPropertiesScript2 = object2.GetComponent<ObjectProperties>();

        //objects.Add(object2);

        //if (object3 != null)
        //{
        //    objects.Add(object3);
        //}

        objectScripts.Add(object2.GetComponent<ObjectProperties>());
        //objectScripts.Add(object3.GetComponent<ObjectProperties>());

        if (object3 != null)
        {
            objectScripts.Add(object3.GetComponent<ObjectProperties>());
        }

	}
	
	// Update is called once per frame
	void Update () {
        

        // if first set of visibility, set the first object to visible, second 
        // object to invis
        // oppposite if set to false
        if(firstClicked == true)
        {

            if(objectPropertiesScript1.visible == true)
            {
                objectPropertiesScript1.visible = false;


                foreach (ObjectProperties obj in objectScripts)
                {
                    obj.visible = true;
                }
            }
            else if (objectPropertiesScript1.visible == false)
            {
                objectPropertiesScript1.visible = true;


                foreach (ObjectProperties obj in objectScripts)
                {
                    obj.visible = false;

                }
            }
            //if (status == true)
            //{
            //    objectPropertiesScript1.visible = true;
            //    //objectPropertiesScript2.visible = false;

            //    foreach (ObjectProperties obj in objectScripts)
            //    {
            //        obj.visible = false;
            //    }
            //}
            //else
            //{
            //    objectPropertiesScript1.visible = false;
            //    //objectPropertiesScript2.visible = true;

            //    foreach (ObjectProperties obj in objectScripts)
            //    {
            //        obj.visible = true;
            //    }
            //}

            firstClicked = false;
        }
        
	}
}
