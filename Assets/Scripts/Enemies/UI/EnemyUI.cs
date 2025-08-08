using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Vector3 sliderDistance;
    [SerializeField] Slider slider;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject tookDamageUI;
    List<GameObject> UILists = new List<GameObject>();
    GameObject UIDamage;
    TMP_Text damageUIText;


    private void Awake()
    {

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void UpdateEnemyHealthBar(float curretValue, float maxValue)
    {
        slider.value = curretValue/maxValue;
    }

    public void UpdateEnemyDamageTook(float damageAmount)
    {

        Debug.Log("ShouldShow New UI");
        UIDamage = Instantiate(tookDamageUI);
        UIDamage.transform.position = transform.position;
        damageUIText = UIDamage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        damageUIText.text = Mathf.Round(damageAmount).ToString();

        UILists.Add(UIDamage);
        Destroy(UIDamage, 1f);
    }


    private void Update()
    {
        slider.transform.position = transform.position + sliderDistance;
        slider.transform.rotation = playerCamera.transform.rotation;
    }
}
