using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField, Range(0,100)] float rotationSpeed;

    public int Value { get => value; }

    public void Collected() {
        GetComponent<Collider>().enabled = false;
        this.transform.DOJump(
            this.transform.position,
            1.5f,
            1,
            0.6f
        ).onComplete = SelfDestruct;
    }

    private void SelfDestruct() {
        Destroy( this.gameObject );
    }

    private void Update() {
        transform.Rotate(0, 360*rotationSpeed*Time.deltaTime, 0);
    }
}
