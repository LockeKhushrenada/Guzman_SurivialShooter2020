using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	public float restartDelay = 5f;
    public SplitScreen twoPlayer;


    Animator anim;
	float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (twoPlayer.multiPlayer == true)
            if (playerHealth.currentHealth1 <= 0 && playerHealth.currentHealth2 <= 0)
            {
                anim.SetTrigger("GameOver");

			    restartTimer += Time.deltaTime;
                if (twoPlayer.playerCount <= 0)
                {
                    if (restartTimer >= restartDelay)
                    {
                        Application.LoadLevel(Application.loadedLevel);
                    }
                }
            }
        if (twoPlayer.multiPlayer == false)
            if (playerHealth.currentHealth1 <= 0)
            {
                anim.SetTrigger("GameOver");

                restartTimer += Time.deltaTime;
                if (twoPlayer.playerCount <= 0)
                {
                    if (restartTimer >= restartDelay)
                    {
                        Application.LoadLevel(Application.loadedLevel);
                    }
                }
            }
    }
}
