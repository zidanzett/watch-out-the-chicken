using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0,1)] float moveDuration = 0.1f;
    [SerializeField, Range(0,1)] float jumpHeight = 1f;
    bool isMoving;
    void Update()
    {
        if (DOTween.IsTweening(transform))
            return;

        Vector3 direction = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // transform.DOMoveZ(transform.position.z + 1, 0.5f);
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // transform.DOMoveZ(transform.position.z - 1, 0.5f);
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // transform.DOMoveX(transform.position.x + 1, 0.5f);
            direction += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // transform.DOMoveX(transform.position.x - 1, 0.5f);
            direction += Vector3.left;
        }

        if (direction == Vector3.zero)
            return;

        Move(direction);

    }

    public void Move(Vector3 direction)
    {
        //isMoving= true;
        //var moveTween = transform.DOMove(transform.position + direction, 0.1f);
        //moveTween.onComplete = () => isMoving = false;
        //transform.DOMoveZ(transform.position.z + direction.z, moveDuration);
        //transform.DOMoveX(transform.position.x + direction.x, moveDuration);
        //var seq = DOTween.Sequence();
        //seq.Append(transform.DOMoveY(jumpHeight, moveDuration * 0.5f));
        //seq.Append(transform.DOMoveY(0, moveDuration * 0.5f));
        transform.DOJump(
            transform.position + direction,
            jumpHeight,
            1,
            moveDuration);
        transform.forward = direction;
    }

}
