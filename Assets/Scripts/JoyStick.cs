using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    protected RectTransform joystick;
    [SerializeField]
    protected float radius;
    [SerializeField]
    protected Camera cam;
    private Vector2 firstPos;
    private Vector2 nowPos;
    private Vector2 vec;

    private void Start()
    {
        firstPos = GetComponent<RectTransform>().position;
        radius = GetComponent<RectTransform>().sizeDelta.x * 0.125f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        nowPos = eventData.position;
        vec = (nowPos - firstPos);
        Player.Instance.direction = new Vector3(vec.x, 0, vec.y);
        // Debug.Log("firstPos" + firstPos);
        // Debug.Log("nowPos " + nowPos);
        
        // Debug.Log("vec: " + vec);

        if (vec.sqrMagnitude < radius * radius)
        {
            joystick.position = firstPos + vec;
            Player.Instance.moveSpeed = vec.magnitude;
            // Debug.Log(vec.magnitude);
        }
        else
        {
            joystick.position = firstPos + vec.normalized * radius;
            Player.Instance.moveSpeed = radius;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joystick.position = firstPos;
        Player.Instance.isWalking = false;
        Player.Instance.StartCoroutine(Player.Instance.coroutine);
        Player.Instance.Anim.SetBool("IsWalking", false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Player.Instance.isWalking = true;
        Player.Instance.StopCoroutine(Player.Instance.coroutine);
        Player.Instance.Anim.SetBool("IsWalking", true);
        Player.Instance.targeting = false;
    }
}