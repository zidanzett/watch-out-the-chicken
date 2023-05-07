using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using TMPro;

public class Chicken : MonoBehaviour {
    [SerializeField, Range(0,1)] float moveDuration = 0.1f;
    [SerializeField, Range(0,1)] float jumpHeight = 0.5f;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;

    [SerializeField] float playTime = 0f;
    [SerializeField] TMPro.TMP_Text playTimeText;

    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<int> OnGetCoin;
    public UnityEvent onCarCollision;
    public UnityEvent OnDie;

    private bool isMoveable = false;

    private void Update()  {
        if (isMoveable == false) {
            return;
        }
        else {
            playTime += Time.deltaTime;
        }

        UpdatePlayTime();

        if (DOTween.IsTweening(transform)) { return; }

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            direction += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction += Vector3.left;
        }

        if (direction == Vector3.zero) { return; }

        Move(direction);
    }

    private void Move(Vector3 direction) {
        var targetPosition = transform.position + direction;
        if (targetPosition.x < leftMoveLimit || targetPosition.x > rightMoveLimit || targetPosition.z < backMoveLimit || Tree.AllPositions.Contains(targetPosition)){ 
            targetPosition = transform.position;
            return;
        }

        // pake dotween
        transform.DOJump(
            transform.position + direction,
            jumpHeight,
            1,
            moveDuration).onComplete = BroadCastPositionOnJumpEnd;
        transform.forward = direction;
    }

    public void SetMoveable(bool value) {
        isMoveable= value;
    }

    public void UpdateMoveLiit(int horizontalSize, int backLimit) {
        leftMoveLimit = -horizontalSize / 2;
        rightMoveLimit = horizontalSize / 2;
        backMoveLimit = backLimit;
    }

    private void BroadCastPositionOnJumpEnd() {
        OnJumpEnd.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Car")) {
            if (transform.localScale.y == 0.1f)
                return;

            transform.DOScale(new Vector3(2, 0.1f, 2), 0.2f);
            isMoveable = false;
            onCarCollision.Invoke();
            Invoke("Die", 3);
        }
        else if (other.CompareTag("Coin")) {
            var coin = other.GetComponent<Coin>();
            OnGetCoin.Invoke(coin.Value);
            coin.Collected();
        }
        else if (other.CompareTag("Eagle")) {
            if (this.transform != other.transform) {
                this.transform.SetParent(other.transform);
                Invoke("Die", 3);
            }
        }
    }

    public void UpdatePlayTime() {
        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);
        playTimeText.text = "Play Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void Die() {
        UpdatePlayTime();
        OnDie.Invoke();
    }
}
