using UnityEngine;

public class IncreaseAttack : MonoBehaviour
{
    ChosenCardEventHandler cardInfo;
    PlayerAttack playerAttack;

    private void Awake()
    {
        playerAttack = transform.parent.transform.parent.GetChild(0).GetComponent<PlayerAttack>();
        cardInfo = GetComponent<ChosenCardEventHandler>();
    }
    private void Update()
    {
        if (cardInfo.cardLeveledUp)
        {
            cardInfo.Lvl++;
            playerAttack.playerAttackDamage = (playerAttack.playerAttackDamage + 30);
            cardInfo.cardLeveledUp = false;
        }
    }
        

}
