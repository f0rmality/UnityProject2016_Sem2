using UnityEngine;
using System.Collections;

//** script learned from a youtube tutorial **//

public class Fadeout : MonoBehaviour {

    public Texture2D fadeoutTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDirection = -1;

    void OnGUI()
    {
        alpha += fadeDirection * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeoutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDirection = direction;
        return (fadeSpeed);
    }

    void OnLevelWasLoaded()
    {
        //fade into new scene
        BeginFade(-1);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
