using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusTextScript : MonoBehaviour {

    public Text StatusText;

    //public GUIText thisText;
    public GameObject ParentObject;


    private Transform target;

    //private RectTransform rectTransform;

    //private Vector3 viewPos;

    //public Camera camera;

    private float cx = Screen.width - 20;
    private float cy = Screen.height -20;
    private float radius = Screen.height / 4.0f;
    //private float speed = 6.0f;
    //private float speedScale;

    

	// Use this for initialization
	void Start () {

        //speedScale = (0.001f * 2.0f * Mathf.PI) / speed;
        //camera = GetComponent<Camera>();

        //rectTransform = StatusText.GetComponent<RectTransform>();

        
	}
	
	// Update is called once per frame
	void Update () {
        //target = ParentObject.transform;

        //viewPos = camera.WorldToViewportPoint(target.position);
        //Debug.Log("x: " + target.position.x);
        //Debug.Log("y:" + target.position.y);

        //Debug.Log("Vx: " + viewPos.x);
        //Debug.Log("Vy:" + viewPos.y);

        //rectTransform.anchoredPosition = new Vector2(1.0F, 1.0F);

        //StatusText.transform.position = new Vector3(0.5f, 0.5f, 5.0f);

        target = ParentObject.GetComponent<Transform>();

        Vector3 temp = Camera.main.WorldToViewportPoint(target.position);

        Debug.Log(temp);

        //StatusText.transform.position = new Vector3(1.0f, 1.0f, 1.0f);

        //      OLD
        //StatusText.transform.position = new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0);

        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);

        //Debug.Log(StatusText.transform.position);
        //Debug.Log(temp);

        // THIS CHUNK MOVES THE TEXT IN A CIRCLE
        float angle = Time.time;// *speedScale;
        StatusText.transform.position = new Vector3(cx + Mathf.Sin(angle) * radius, cy + Mathf.Cos(angle) * radius, 0);
        //Debug.Log(StatusText.transform.position);


	}
}
