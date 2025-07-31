using System.Collections;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FiippingCards : MonoBehaviour
{

    [SerializeField] RectTransform[] cardTransform;
    [SerializeField] GameObject[] cards;
    [SerializeField] UnityEngine.UI.Image[] currentSprite;
    [SerializeField] UnityEngine.UI.Image[] frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] GameObject player;
    [SerializeField] GameObject cardsParent;
    PlayerLevelingUp playerLevelingUp;
    PlayerXP playerXP;
    [SerializeField] int cardsCount;
    [SerializeField] int UICardsCount;

    [SerializeField] GameObject[] UICards;

    float yRotation;
    bool gotComponent;
    private void Awake()
    {
        cardsParent = GameObject.FindGameObjectWithTag("Cards");// use get child from index instead
        player = GameObject.FindGameObjectWithTag("Player");// use parent instead
        playerLevelingUp = player.GetComponent<PlayerLevelingUp>();

        cardsCount = cardsParent.transform.childCount;
        cards = new GameObject[cardsCount];
        frontSprite = new UnityEngine.UI.Image[cardsCount];
        


        for (int i = 0; i < cardsCount; i++)
        {
            cards[i] = cardsParent.transform.GetChild(i).gameObject;
            frontSprite[i] = cards[i].GetComponent<UnityEngine.UI.Image>();
        }





    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {

        gotComponent = false;
    }
    private void Update()
    {
        if (playerLevelingUp.panelIsActive)
        {
            if (!gotComponent)
            {
                UICardsCount = transform.childCount;
                UICards = new GameObject[UICardsCount];
                cardTransform = new RectTransform[UICardsCount];
                currentSprite = new UnityEngine.UI.Image[UICardsCount];

                for (int i = 0; i < UICardsCount; i++)
                {
                    UICards[i] = transform.GetChild(i).gameObject;
                }

                for (int i = 0; i < UICardsCount; i++)
                {

                    cardTransform[i] = UICards[i].GetComponent<RectTransform>();
                    currentSprite[i] = UICards[i].GetComponent<UnityEngine.UI.Image>();
                }

                gotComponent = true;

            }
/*            StartCoroutine(AfterActiveDelay());*/
        }

        for (int i = 0; i < UICardsCount ;i++)
        {

            yRotation = cardTransform[i].rotation.eulerAngles.y;
/*            Debug.Log("Card " + i + " yRot: " +  yRotation);*/

            if (yRotation > 180)
            {
                yRotation = yRotation - 360;
            }

            if (Mathf.Abs(yRotation) > 90)
            {

                currentSprite[i].sprite = backSprite;
            }
            else
            {
                currentSprite[i].sprite = frontSprite[playerLevelingUp.randomNum[i]].sprite;
            }

        }




        /*        else if(playerXP.levelingUp)
                {
                    thisImage.sprite = frontSprite;
                }*/

    }
}
