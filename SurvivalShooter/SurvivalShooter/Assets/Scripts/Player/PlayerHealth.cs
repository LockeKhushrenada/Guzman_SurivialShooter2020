using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth1;
    public int currentHealth2;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip playerHurt;
    public AudioClip heartBeat;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public SplitScreen twoPlayer;
    [SerializeField]
    int playerIndex = 1;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    CameraFollow camShake;
    SplitScreen splitScreen;
    public bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth1 = startingHealth;
        currentHealth2 = startingHealth;
        camShake = Camera.main.GetComponent<CameraFollow>();
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
            //camShake.CameraShakeFunction();
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        if (playerIndex == 1)
        {
            currentHealth1 -= amount;
            healthSlider.value = currentHealth1;
            playerAudio.PlayOneShot(playerHurt);

            if (currentHealth1 <= 25 && currentHealth1 > 0)
            {
                playerAudio.Play();
                playerAudio.loop = true;
            }
            if (currentHealth1 <= 0 && !isDead)
            {
                //camShake.DramaticZoomFunction();
                twoPlayer.playerCount--;
                Death();
            }
        }
        

    }
    public void TakeDamage2(int amount)
    {
        if (playerIndex == 2)
        {
            currentHealth2 -= amount;
            healthSlider.value = currentHealth2;
            playerAudio.PlayOneShot(playerHurt);
            if (currentHealth2 <= 25 && currentHealth2 > 0)
            {
                playerAudio.Play();
                playerAudio.loop = true;

            }
            if (currentHealth2 <= 0 && !isDead)
            {
                twoPlayer.playerCount--;
                Death();
            }
        }
    }



    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        //camShake.DramaticZoomFunction();
        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.loop = false;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
