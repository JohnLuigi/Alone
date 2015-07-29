using UnityEngine;
using System.Collections;

public class OnClickText : MonoBehaviour {

    public string id = "";
    public float timeToShow = 1.5f;
    private float clickedTime = 0.0f, currentTime = 0.0f;
    public GUIText displayText;
    private int count = 0;
    
	// Use this for initialization
	void Start () {

        displayText.text = "";
	}

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // when the mouse moves over a box collider
    void OnMouseOver()
    {
        // If the left mouse button is clicked on the box collider for an object
        if(Input.GetMouseButtonDown(0))
        {
            // set the starting time for the text
            clickedTime = Time.time;
            
            // Different reactions for each object
            if(string.Equals(id, "bed"))
            {
                DisplayText();
            }
            else if(string.Equals(id, "desk"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "bookshelf"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "window"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "poster"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "PC"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "monitor"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "book1"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "book2"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "snowglobe"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "outside"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "door"))
            {
                DisplayText();
            }
            else if (string.Equals(id, "stickman"))
            {
                DisplayText();
            }

            
        }
    }

    void DisplayText()
    {
        Debug.Log("You clicked on " + id);
    }

	// Update is called once per frame
	void Update () {
        currentTime = Time.time;

        if(clickedTime != 0.0f)
        {
            //Debug.Log("clickedTime is not set");
            //Debug.Log(count);
            count++;
            if (Mathf.Abs(currentTime - clickedTime) > timeToShow)
            {
                clickedTime = 0.0f;
                displayText.text = "";
                
            }
            else
            {
                displayText.text = "That's the " + id;
            }
                
        }

        
	}

}
