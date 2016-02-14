using UnityEngine;
using System.Collections;

public class FixFramerate : MonoBehaviour {

    public int fps = 60;
    public float timeScale = 1.0f;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = fps; // lock at 60fps
        Time.timeScale = timeScale;
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
