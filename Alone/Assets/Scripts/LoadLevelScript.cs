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
    //AudioClip sound;

    // the black image that will fade on scene change
    GameObject screenFaderObject;
    Image screenFader;

    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

    private bool sceneStarting = true;      // bool used to know if the scene is fading in or not

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
        //audioSource = (AudioSource)gameObject.AddComponent("AudioSource");
        //sound = (AudioClip)Resources.Load("DoorOpening");
        //audioSource.clip = sound;

        // find the object that has the image
        screenFaderObject = GameObject.Find("ScreenFader");
        // set the reference to the image
        screenFader = screenFaderObject.GetComponent<Image>();

        // default sound is the dor opening
        // temporarily doing this to avoid loading new ones
        // might be better to have separate audio sources on each scene to reduce loading times
        if(soundType == 0)
        {
            if(GameObject.Find("DoorOpeningSFX"))
            {
                GameObject audioObject = GameObject.Find("DoorOpeningSFX");

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
                PlayAudio();
                //EndScene();

                // If this line is enabled, the next scene will load without waiting
                //Application.LoadLevel(levelToLoad);
            }
            else
            {
                //EndScene();
                Application.LoadLevel(levelToLoad);
            }

            //EndScene();

            //Application.LoadLevel(levelToLoad);
            //EditorApplication.OpenScene("Assets/Scenes/" + levelToLoad + ".unity");
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(sceneStarting)
        {
            StartScene();
        }
	}

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
        screenFader.color = Color.Lerp(screenFader.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the color of the texture between itself and black.
        screenFader.color = Color.Lerp(screenFader.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (screenFader.color.a <= 0.05f)
        {
            // ... set the color to clear and disable the Image
            screenFader.color = Color.clear;
            screenFader.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        // Make sure the texture is enabled.
        screenFader.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (screenFader.color.a >= 0.95f)
        {
            
            // load the level
            Application.LoadLevel(levelToLoad);
        }
            
    }
}
