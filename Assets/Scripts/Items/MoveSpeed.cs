using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    ChosenCardEventHandler cardInfo;
    CharacterMovement characterMovement;

    private void Awake()
    {
        cardInfo = GetComponent<ChosenCardEventHandler>();
        characterMovement = transform.parent.transform.parent.GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        if (cardInfo.cardLeveledUp)
        {
            cardInfo.Lvl++;
            characterMovement.moveSpeed = (characterMovement.moveSpeed + .2f);
            cardInfo.cardLeveledUp = false;
        }
    }

}
