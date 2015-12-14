using UnityEngine;
using System.Collections;
//using UnityEditor;

public class LoadLevelScript : MonoBehaviour {

    public string levelToLoad = "";
    public bool trueLoad = true;
	// Use this for initialization
	void Start () {
	
	}

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && (trueLoad == true))
        {
            Application.LoadLevel(levelToLoad);
            //EditorApplication.OpenScene("Assets/Scenes/" + levelToLoad + ".unity");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
