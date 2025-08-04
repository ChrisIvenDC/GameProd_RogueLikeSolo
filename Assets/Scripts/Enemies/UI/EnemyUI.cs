using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyUI : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject tookDamageUI;
    [SerializeField] EnemyMovement enemyMovement;
    List<GameObject> UILists = new List<GameObject>();
    GameObject UIDamage;
    TMP_Text damageUIText;


    private void Awake()
    {
/*        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();*/
        enemyMovement = transform.parent.GetComponent<EnemyMovement>(); 
    }
    private void FixedUpdate()
    {

    }
    public void UpdateEnemyHealthBar(float curretValue, float maxValue)
    {
        slider.value = curretValue/maxValue;
    }

    public void UpdateEnemyDamageTook(float damageAmount)
    {
        Debug.Log("ShouldShow New UI");
        UIDamage = Instantiate(tookDamageUI, this.transform);
        damageUIText = UIDamage.GetComponent<TextMeshProUGUI>();
        damageUIText.text = Mathf.Round(damageAmount).ToString();


        if (!enemyMovement.isFacingLeft)
        {
            Vector3 ls = UIDamage.transform.localScale;
            ls.x *= -1f;
            UIDamage.transform.localScale = ls;
        }

        UILists.Add(UIDamage);
        Destroy(UIDamage, 1f);
    }


    private void Update()
    {
        slider.transform.rotation = playerCamera.transform.rotation;
    }

}
