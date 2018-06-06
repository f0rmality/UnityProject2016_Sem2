using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win_Fail_Scripts : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //unlock curser
        Cursor.lockState = CursorLockMode.None;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        //this won't work in the editor
        Application.Quit();
    }
}
