using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSDisplay : MonoBehaviour
{
    public float UpdateInterval = 1;
    private Text text;
    private int frames;
    
    
    // Use this for initialization
    void Start ()
    {
        Application.targetFrameRate = 1000;
        text = GetComponent<Text>();
		InvokeRepeating("UpdateFPS", UpdateInterval, UpdateInterval);
	}

    void UpdateFPS()
    {
        if (frames < 30) text.color = Color.red;
        if(frames >= 30 && frames <= 50) text.color = Color.yellow;
        else text.color = Color.green;
        text.text = "FPS: " + frames ;
        frames = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    frames++;
	}
}
