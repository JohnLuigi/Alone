using UnityEngine;
using System.Collections;

public class IntroLoader : MonoBehaviour {

    // The three slides plus the intermediate black screen
    public GameObject Screen1;
    public GameObject Screen2;
    public GameObject Screen3;
    public GameObject BlackScreen;

    float fadeSpeed = 1.0f;

    bool reappearing = false;
    bool disappearing = true;

    float delayCounter = 0.0f;
    float delayTime = 3.0f;
    float delayIncrement = 0.1f;

    int slideNumber = 0;

    // used to change the alpha of the black screen
    Color tempColor;


	// Use this for initialization
	void Start () {

        //start with first screen visible, rest not visible
        Screen1.renderer.enabled = true;
        Screen2.renderer.enabled = false;
        Screen3.renderer.enabled = false;

        tempColor = BlackScreen.renderer.material.color;

        // fade the black screen out to reveal the first screen

	}
	
	// Update is called once per frame
	void Update () {

        // using linear interpolation
        // THIS WORKS USE THIS
        //tempColor.a = Mathf.Lerp(tempColor.a, 0, Time.deltaTime * fadeSpeed);

        //Debug.Log(tempColor.a);

        if (disappearing)
        {
            Disappear();
        }

        if(reappearing)
        {
            Reappear();
        }

        //when the black screen becomes invisible, do this
        if(tempColor.a <= 0.02f)
        {
            disappearing = false;
            //HERE DISPLAY THE TEXT

            //TODO





            //WAIT A FEW SECONDS
            delayCounter += delayIncrement;

            if(delayCounter >= delayTime)
            {
                //SET TO REAPPEAR
                reappearing = true;
                delayCounter = 0.0f;
            }

            
        }

        //when the black screen becomes visible again, do this
        else if (tempColor.a >= 0.98f)
        {
            reappearing = false;

            //HERE DISPLAY THE TEXT

            // TODO




            //WAIT A FEW SECONDS
            delayCounter += delayIncrement;

            if (delayCounter >= delayTime)
            {
                //SET TO Disappear
                disappearing = true;
                delayCounter = 0.0f;
                slideNumber++;
                if(slideNumber == 1)
                {
                    Screen1.renderer.enabled = false;
                    Screen2.renderer.enabled = true;
                    Screen3.renderer.enabled = false;
                }
                if(slideNumber == 2)
                {
                    Screen1.renderer.enabled = false;
                    Screen2.renderer.enabled = false;
                    Screen3.renderer.enabled = true;
                }
                if(slideNumber == 3)
                {
                    //LOAD FRONT OF HOUSE LEVEL LEVEL
                    Application.LoadLevel("HouseFront");
                }
                
            }

            
        }

        
        


        BlackScreen.renderer.material.color = tempColor;

        
	}

    // become visible
    void Reappear()
    {
        tempColor.a = Mathf.Lerp(tempColor.a, 1.0f, Time.deltaTime * fadeSpeed);
    }

    // become invisible
    void Disappear()
    {
        tempColor.a = Mathf.Lerp(tempColor.a, 0.0f, Time.deltaTime * fadeSpeed);
    }
}
