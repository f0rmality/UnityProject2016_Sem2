using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour {

    public int level = 1;
    public Canvas MainUI;
    public Canvas controlsUI;
    public Image background;
    public Image background2;

    public AudioSource music;

    void Start()
    {
        MainUI.enabled = true;
        controlsUI.enabled = false;
    }

    void Update()
    {
        StartCoroutine(ChangeColor());
        //background.color = Random.ColorHSV();
    }

    public void PlayGame()
    {
        //StartCoroutine(FadeMusic());
        SceneManager.LoadScene(level);
    }

    public void DisplayControls()
    {
        MainUI.enabled = false;
        controlsUI.enabled = true;
    }

    public void BackToMenu()
    {
        MainUI.enabled = true;
        controlsUI.enabled = false;
    }

    public void QuitGame()
    {
        //this won't work while in the library
        Application.Quit();
    }

    IEnumerator FadeMusic()
    {
        while (music.volume > .1F)
        {
            music.volume = Mathf.Lerp(music.volume, 0F, Time.deltaTime);
            yield return 0;
        }
        music.volume = 0;
    }

    IEnumerator ChangeColor()
    {
        if (Time.frameCount % 8f == 0)
        {
            background.color = Random.ColorHSV(0, 1, 0, 0.75f, 0.5f, 1, 1, 1);
            //background2.color = Random.ColorHSV();
            background2.color = Random.ColorHSV(0, 1, 0, 0.75f, 0.5f, 1, 1, 1);
            yield return 0;
        }
    }
}
