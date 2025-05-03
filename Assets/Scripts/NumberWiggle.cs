using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWiggle : MonoBehaviour
{
    [Header("Base Movement")]
    public float minMoveSpeed = 5;
    public float maxMoveSpeed = 10;
    public float moveSpeed, radius = .5f;
    float angle;

    Vector3 initalPos;

    public void SetupWiggle(float min, float max, float radius) {
        minMoveSpeed = min;
        maxMoveSpeed = max;
        this.radius = radius;
    }

    private void Start() {
        initalPos = transform.localPosition;
        if (Random.value < 0.5f) {
            moveSpeed = Random.Range(-minMoveSpeed, -maxMoveSpeed);
        } else moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    public void Update() {
        angle += (moveSpeed / (radius * Mathf.PI * 2.0f)) * Time.deltaTime;
        transform.localPosition = initalPos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}
