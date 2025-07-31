using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelingUp : MonoBehaviour
{
    [SerializeField] PlayerXP playerXp;
    public GameObject[] cards;
    [SerializeField] GameObject levelingUpPanel;
   
    [SerializeField] int itemsLength;
    
    [SerializeField] GameObject itemParent;
    public int[] randomNum;
    bool generateRanNum;
    bool[] cardIsClicked;

    public int chosenCard;

    public bool panelIsActive, anyCardIsClicked;
    Animator[] cardAnimator;

    private void Awake()
    {
        playerXp = GetComponent<PlayerXP>();


        cards[0].GetComponent<Button>().onClick.AddListener(() => CardIsClicked(0));//idk why forloop is not working, kahit foreach
        cards[1].GetComponent<Button>().onClick.AddListener(() => CardIsClicked(1));
        cards[2].GetComponent<Button>().onClick.AddListener(() => CardIsClicked(2));
        cardAnimator = new Animator[cards.Length];



        for (int i = 0; i < cards.Length; i++)
        {
            cardAnimator[i] = cards[i].GetComponent<Animator>();
        }
    }

    private void Start()
    {
        anyCardIsClicked = false;
        generateRanNum = true;
        itemsLength = itemParent.transform.childCount -1;
        randomNum = new int[cards.Length];
        cardIsClicked = new bool[cards.Length];
        Debug.Log("random.length= " + randomNum.Length);

        panelIsActive = false;
    }

    private void Update()
    {
        if (playerXp.levelingUp)
        {
            SetAllCardsFalse();
            //Playerchosen
            UpdatedPlayerChosenAnim(false);
            //isEnabletrue
            UpdateIsEnableCardAnim(true);
            levelingUpPanel.SetActive(true);
            panelIsActive = true;

            GenerateRandomNum();
            Debug.Log("ItemLength: " + itemsLength);

        }
        


    }

    private void CardIsClicked(int index)
    {
        playerXp.levelingUp = false;
        UpdatedPlayerChosenAnim(true);

        cardIsClicked[index] = true;
        UpdateIsClickedCardAnim();
        generateRanNum = true;


        chosenCard = randomNum[index];
        anyCardIsClicked = cardIsClicked[index];
        StartCoroutine(AfterLevelUpDelay());
    }


        void SetAllCardsFalse() 
    {
        Debug.Log("settingAllCardsToFalse");
        anyCardIsClicked = false;
        for (int i  = 0; i < cardIsClicked.Length; i++) 
        { 
            cardIsClicked[i] = false;
        }

    }

    IEnumerator AfterLevelUpDelay()
    {

        yield return new WaitForSeconds(.5f);
        //isenable False
        UpdateIsEnableCardAnim(false);
        yield return new WaitForSeconds(.5f);
        panelIsActive = false;
        levelingUpPanel.SetActive(false);
    }
    void GenerateRandomNum()
    {
        if (generateRanNum)
        {

            randomNum[0] = Random.Range(0, itemsLength);
            for (int i = 1; i < randomNum.Length; i++)
            {
                /*                Debug.Log("Line num: " + i);*/
                int newRandom = Random.Range(0, itemsLength);
                while (randomNum[i-1] == newRandom)
                {
                    newRandom = Random.Range(0, itemsLength);
                }

                randomNum[i] = newRandom;
            }
            Debug.Log(" randomNumIndex 1=" + randomNum[0]);//check debugging if unique num
            Debug.Log(" randomNumIndex 2=" + randomNum[1]);
            Debug.Log(" randomNumIndex 3=" + randomNum[2]);
            generateRanNum = false;//add true same with levelingup(leveling up shoudle b false after choosing)
        }
        Debug.Log("generatedrandomnum: " + generateRanNum);
    }

    void UpdateIsEnableCardAnim(bool isEnable)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cardAnimator[i].SetBool("isEnable", isEnable);
        }
    }

    void UpdateIsClickedCardAnim()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cardAnimator[i].SetBool("isClicked", cardIsClicked[i]);
        }
    }

    void UpdatedPlayerChosenAnim(bool isPlayerChosen)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cardAnimator[i].SetBool("hasPlayerChosen", isPlayerChosen);
        }
    }
}
