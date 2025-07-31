using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] PlayerUI playerUI;

    [SerializeField] float xpPercentage;
    public float playerMaxXP, xpMultiplier;
    [SerializeField]int lvl;
    [SerializeField]float playerXP;
    float excessXP;
    public bool levelingUp;
    bool xpConverting;
    Animator[] cardAnimator;
    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
    }
    private void Start()
    {
        levelingUp = false;
        xpMultiplier = 1f;
        xpPercentage = 10f;
        playerXP = 1f;
        playerUI.UpdatePlayerXPBar(playerXP, playerMaxXP);
    }
    private void Update()
    {
        Debug.Log("LevelingUp: " + levelingUp);
        if (playerXP > playerMaxXP)
        {
            xpConverting = true;
            levelingUp = true;
        }

        if(levelingUp) 
        {

            if (xpConverting)
            {
                lvl += 1;
                excessXP = playerXP - playerMaxXP;
                playerXP = excessXP;

                playerMaxXP = playerMaxXP + (playerMaxXP * 0.2f);

                xpConverting = false;
            }

        }

    }



    public void PlayerTookXP(float xpValue)
    {
        playerXP += xpValue * xpMultiplier;
        playerUI.UpdatePlayerXPBar(playerXP, playerMaxXP);
    }
}
