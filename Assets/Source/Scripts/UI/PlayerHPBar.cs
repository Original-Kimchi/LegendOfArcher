using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{

    public static PlayerHPBar instance;
    
    [SerializeField] private Text text;
    [SerializeField] private Slider slider;
    private float maxHp;
    private float currentHp;
    private float startHp;
    private float LevelUpHp;

    public GameObject HpLineFolder;
    float unitHP = 200f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        maxHp = Player.Instance.hp;
        startHp = maxHp;
        LevelUpHp = 1;
    }
    private void Update()
    {
        slider.value = Player.Instance.hp / Player.Instance.maxHp;
        text.text = Player.Instance.hp.ToString();
        transform.position = Camera.main.WorldToScreenPoint(Player.Instance.transform.position + Vector3.up * 2f);

    }

    public void GetHpBoost()
    {
        float scaleX = startHp / maxHp;

        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach(Transform child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
