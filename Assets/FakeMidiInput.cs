using UnityEngine;
using System.Collections;

public class FakeMidiInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static float GetKey(int i)
    {
        switch (i)
        {
            case 60:
                return Input.GetKey(KeyCode.A) ? 1f : 0f;
            case 61:
                return Input.GetKey(KeyCode.W) ? 1f : 0f;
            case 62:
                return Input.GetKey(KeyCode.S) ? 1f : 0f;
            case 63:
                return Input.GetKey(KeyCode.E) ? 1f : 0f;
            case 64:
                return Input.GetKey(KeyCode.D) ? 1f : 0f;
            case 65:
                return Input.GetKey(KeyCode.F) ? 1f : 0f;
            case 66:
                return Input.GetKey(KeyCode.T) ? 1f : 0f;
            case 67:
                return Input.GetKey(KeyCode.G) ? 1f : 0f;
            case 68:
                return Input.GetKey(KeyCode.Y) ? 1f : 0f;
            case 69:
                return Input.GetKey(KeyCode.H) ? 1f : 0f;
            case 70:
                return Input.GetKey(KeyCode.U) ? 1f : 0f;
            case 71:
                return Input.GetKey(KeyCode.J) ? 1f : 0f;
            default:
                return 0f;
        }
    }
}
