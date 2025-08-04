using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    ChosenCardEventHandler cardInfo;
    [SerializeField] CharacterMovement characterMovement;

    private void Awake()
    {
        cardInfo = GetComponent<ChosenCardEventHandler>();
        characterMovement = transform.parent.transform.parent.GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        if(cardInfo.cardLeveledUp)
        {
            Debug.Log("Maxjump add");
            cardInfo.Lvl++;
            characterMovement.maxJump ++;
            cardInfo.cardLeveledUp = false;
        }
    }
}
