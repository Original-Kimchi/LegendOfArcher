using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    private void Update()
    {
        text.text = Player.Instance.hp.ToString();
        image.fillAmount = Player.Instance.hp / Player.Instance.originHp;
        transform.position = Camera.main.WorldToScreenPoint(Player.Instance.transform.position) + Vector3.up * 2f;
    }
}
