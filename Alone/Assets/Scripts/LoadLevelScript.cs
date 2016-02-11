using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor;

public class LoadLevelScript : MonoBehaviour {

    public string levelToLoad = "";
    public bool trueLoad = true;

    // This number is used to determine what sound is played when the level loads
    public int soundType = 0;

    public AudioSource audioSource;

    //private float fadeAmount = 0.5f;
    //AudioClip sound;

    // the black image that will fade on scene change
    GameObject screenFader;
    //Image screenFader;
    Color faderColor;

    private float fadeSpeed = 0.0f;          // Speed that the screen fades to and from black.
    private float fadeTime = 1.0f;
    private float timer = 0.0f;

    private bool sceneStarting = true;      // bool used to know if the scene is fading in or not
    private bool sceneEnding = false;

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
        //audioSource = (AudioSource)gameObject.AddComponent("AudioSource");
        //sound = (AudioClip)Resources.Load("DoorOpening");
        //audioSource.clip = sound;


        // find the object that has the image
        screenFader = GameObject.Find("ScreenFader");
        // set the reference to the image
        //screenFader = screenFaderObject.GetComponent<Image>();

        faderColor = screenFader.GetComponent<SpriteRenderer>().color;

        // make sure the screen fader object is centered on the main camera
        screenFader.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, screenFader.transform.position.z);

        // initially make the fader black, so it can fade to clear
        //screenFader.renderer.material.color = Color.black;
        faderColor.a = 1.0f;
        screenFader.GetComponent<SpriteRenderer>().color = faderColor;

        // default sound is the door opening
        // temporarily doing this to avoid loading new ones
        // might be better to have separate audio sources on each scene to reduce loading times
        if (soundType == 0)
        {
            if (GameObject.Find("DoorOpeningSFX"))
            {
                GameObject audioObject = GameObject.Find("DoorOpeningSFX");

                audioSource = audioObject.GetComponent<AudioSource>();
            }

        }
        // sound for the market scene (door chime)
        else if (soundType == 1)
        {
            if (GameObject.Find("ShopDoorBell"))
            {
                GameObject audioObject = GameObject.Find("ShopDoorBell");

                audioSource = audioObject.GetComponent<AudioSource>();
            }
        }

        // sound for footsteps or climbing stairs
        else if (soundType == 2)
        {
            if (GameObject.Find("Footsteps"))
            {
                GameObject audioObject = GameObject.Find("Footsteps");

                audioSource = audioObject.GetComponent<AudioSource>();
            }
        }



        
        
	}

    void OnMouseOver()
    {
        
        if(Input.GetMouseButtonDown(0) && (trueLoad == true))
        {

            

            if(audioSource != null)
            {
                // Original site for playing the door audio
                //audioSource.enabled = true;
                //audioSource.Play();
                //FadeToBlack();
                sceneEnding = true;
                PlayAudio();
                //EndScene();

                // If this line is enabled, the next scene will load without waiting for the audio to finish
                //Application.LoadLevel(levelToLoad);
            }
            else
            {
                //EndScene();
                //FadeToBlack();
                sceneEnding = true;
                Application.LoadLevel(levelToLoad);
            }

            //EndScene();

            //Application.LoadLevel(levelToLoad);
            //EditorApplication.OpenScene("Assets/Scenes/" + levelToLoad + ".unity");
        }
    }
	
	// Update is called once per frame
	void Update () {

        // incerement the timer by the amount of time passed since the last frame
        timer += Time.deltaTime;

	    if(sceneStarting)
        {
            //StartScene();
            FadeToClear();
        }

        if(sceneEnding)
        {
            //EndScene();
            FadeToBlack();
        }

        //screenFader.renderer.material.color = faderColor;
	}

    // Audio corooutine to play the level loading sound
    public void PlayAudio()
    {
        StartCoroutine("PlayAudioRoutine");
    }

    IEnumerator PlayAudioRoutine()
    {
        

        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Application.LoadLevel(levelToLoad);
    }

    void FadeToClear()
    {
        // Lerp the color of the texture between itself and transparent.
        faderColor = screenFader.GetComponent<SpriteRenderer>().color;
        //faderColor.a = Mathf.Lerp(1.0f, 0.0f, timer * fadeTime);
        
        //faderColor.a -= (fadeAmount * Time.deltaTime);

        //faderColor.a = Mathf.SmoothDamp(faderColor.a, 0.0f, ref fadeSpeed, fadeTime);
        faderColor.a = Mathf.SmoothDamp(faderColor.a, 0.0f, ref fadeSpeed, fadeTime);
        if(faderColor.a <= 0.05f)
        {
            faderColor.a = 0.0f;
            timer = 0.0f;
            sceneStarting = false;
        }

        screenFader.GetComponent<SpriteRenderer>().color = faderColor;
        

        


        //Debug.Log(screenFader.GetComponent<SpriteRenderer>().color.a);
    }


    void FadeToBlack()
    {
        // Lerp the color of the texture between itself and black.
        faderColor = screenFader.GetComponent<SpriteRenderer>().color;
        //faderColor.a += (fadeAmount * Time.deltaTime);
        //faderColor.a = Mathf.Lerp(0.0f, 1.0f, timer * fadeTime);

        //faderColor.a = Mathf.SmoothDamp(faderColor.a, 1.0f, ref fadeSpeed, fadeTime);
        faderColor.a = Mathf.SmoothDamp(faderColor.a, 1.0f, ref fadeSpeed, fadeTime);
        if(faderColor.a >= 0.95f)
        {
            faderColor.a = 1.0f;
            timer = 0.0f;
            sceneEnding = false;
        }
        
        
        screenFader.GetComponent<SpriteRenderer>().color = faderColor;
    }

    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (screenFader.GetComponent<SpriteRenderer>().color.a <= 0.05f)
        {
            // ... set the color to clear and disable the Image
            screenFader.GetComponent<SpriteRenderer>().color = Color.clear;
            screenFader.renderer.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        // Make sure the texture is enabled.
        screenFader.renderer.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (screenFader.GetComponent<SpriteRenderer>().color.a >= 0.95f)
        {
            
            // load the level
            //Application.LoadLevel(levelToLoad);
            //PlayAudio();
            return;
        }
            
    }
}
