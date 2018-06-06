using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMainStats : MonoBehaviour {

    public bool isAlive = true;
    public float totalHealth = 100.0f;
    public float currentHealth = 100.0f;

    public float totalEnergy = 100.0f;
    public float currentEnergy = 100.0f;

    public float totalStamina = 100.0f;
    public float currentStamina = 100.0f;

    public int fullClip = 40;
    public int currentClip;
    public int ExtraAmmo;

    public int progressLevel = 0;

    public Text currentAmmo;
    public Text totalAmmo;

    public Image healthBar;
    public Image staminaBar;
    public Image energyBar; 
    public PlayerController playerScript;
    public PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {

        currentClip = 40;
        ExtraAmmo = 100;
	
	}
	
	// Update is called once per frame
	void Update () {

        //health bar totals
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        healthBar.fillAmount = currentHealth / totalHealth;

        //player regeneration

        if (currentEnergy != totalEnergy)
        {
            currentEnergy += 0.5f * Time.deltaTime;
        }

        //if not sprinting, allow that to happen
        //if springing, decrease by X amount per f * time

        if(currentStamina != totalStamina)
        {
            currentStamina += 1.0f * Time.deltaTime;
        }

        //check if player can fire

        if(ExtraAmmo != 0 || currentClip != 0)
        {
            playerScript.hasAmmo = true;
        }

        else
        {
            playerScript.hasAmmo = false;
        }

        if(currentHealth <= 0)
        {
            Death();
        }

        //update HUD ammo
        currentAmmo.text = currentClip.ToString();
        totalAmmo.text = ExtraAmmo.ToString();
	
	}

    void Death()
    {
        //camera fly away
        playerScript.playerCamera.transform.position += new Vector3(0f, 0.1f, -0.1f);
        playerScript.m_PlayerAnimator.SetBool("isAlive", false);
        playerMovement.playerAlive = false;

        //cause screen to fade and then go to the restart/quit screen
        //load the death scene here
        //SceneManager.LoadScene(2);
        StartCoroutine(playerDeath());
    }

    IEnumerator playerDeath()
    {
        float fadetime = GameObject.Find("_MAIN_").GetComponent<Fadeout>().BeginFade(1);
        yield return new WaitForSeconds(fadetime);
        SceneManager.LoadScene(2);
    }
}
