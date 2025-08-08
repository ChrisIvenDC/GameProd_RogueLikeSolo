using UnityEngine;

public class IncreaseAtkSpeed : MonoBehaviour
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
            playerAttack.attackSpeed = (playerAttack.attackSpeed + .3f);
            cardInfo.cardLeveledUp = false;
        }
    }
}
