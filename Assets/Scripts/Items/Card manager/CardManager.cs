using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    PlayerLevelingUp playerLevelingup;
    [SerializeField]GameObject[] cards;
    int cardCount;

    private void Awake()
    {
        playerLevelingup = transform.parent.GetComponent<PlayerLevelingUp>();
        cardCount = transform.childCount;
        cards = new GameObject[cardCount];
        for (int i = 0; i < cardCount; i++)
        {
            cards[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        Debug.Log(cardCount);
        for (int i = 0; i < cardCount; i++)
        {
            Debug.Log("inCardManager: " + cards[i].name);
        }
    }

    private void Update()
    {
        Debug.Log("CardChosen Updating events: " + playerLevelingup.anyCardIsClicked);
        if (playerLevelingup.anyCardIsClicked)
        {
            Debug.Log("OneCardLeveled Up");
            cards[playerLevelingup.chosenCard].GetComponent<ChosenCardEventHandler>().cardLeveledUp = true;
            playerLevelingup.anyCardIsClicked = false;
        }
    }
}
