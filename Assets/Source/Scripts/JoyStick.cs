using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private RectTransform joystick;
    [SerializeField] private float radius;
    [SerializeField] private Player player;
    private Vector2 nowPos;
    private Vector2 firstPos;

    private void Start()
    {
        radius = GetComponent<RectTransform>().sizeDelta.x * 0.125f;
        firstPos = joystick.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        nowPos = eventData.position - firstPos;
        player.moveDir = new Vector3(nowPos.x, 0, nowPos.y);
        // Debug.Log("firstPos" + firstPos);
        // Debug.Log("nowPos " + nowPos);
        
        // Debug.Log("vec: " + vec);

        if (nowPos.sqrMagnitude < radius * radius)
        {
            joystick.localPosition =  nowPos;
            player.moveSpeed = nowPos.magnitude;
            // Debug.Log(vec.magnitude);
        }
        else
        {
            joystick.localPosition = nowPos.normalized * radius;
            player.moveSpeed = radius;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (player.gameObject.activeSelf)
        {
            if (player.gameObject.activeSelf)
            {
                joystick.localPosition = Vector2.zero;
                // Player.Instance.StartCoroutine(Player.Instance.coroutine);
                player.Anim.SetBool("IsWalking", false);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Player.Instance.StopCoroutine(Player.Instance.coroutine);
        player.Anim.SetBool("IsWalking", true);
        player.targeting = false;
    }
}