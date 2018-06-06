using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour {
    public int progressLevel = 0;

    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;
    public Text objective5;


	// Use this for initialization
	void Start () {
        objective1.enabled = true;
        objective2.enabled = false;
        objective3.enabled = false;
        objective4.enabled = false;
        objective5.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        switch (progressLevel)
        {
            case 1:
                objective1.enabled = false;
                objective2.enabled = true;
                break;
            case 2:
                objective2.enabled = false;
                objective3.enabled = true;
                break;
            case 3:
                objective3.enabled = false;
                objective4.enabled = true;
                break;
            case 4:
                objective4.enabled = false;
                objective5.enabled = true;
                break;
            default:
                break;
        }
	
	}
}
