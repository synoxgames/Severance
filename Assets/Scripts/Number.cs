using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class Number : MonoBehaviour
{
    public int number;
    public float hoverScaler = 2f;
    public float smoothTimer = 1.25f;

    public Emotion emotion;

    float timer = 0;
    Vector2 scaleVector = new Vector2(1, 1);
    Vector2 initalVector = new Vector2(1, 1);
    Vector2 gridPosition = new Vector2();

    NumberWiggle wiggle;
    SpriteRenderer sprite;
    BoxCollider2D boxCollider;


    bool heldDown;

    public void Setup(NumberWiggle wiggle, SpriteRenderer sprite, BoxCollider2D coll, Emotion emotion) {
        this.wiggle = wiggle;
        this.sprite = sprite;
        boxCollider = coll;
        this.emotion = emotion;

        if (emotion != Emotion.Null) this.sprite.color = Color.red;

        OnBecameInvisible();
    }

    public void SetPosition(int x, int y) {
        gridPosition = new Vector2(x, y);
    }

    public void SetNumber(int value) {
        number = value;
    }

    public Vector2 GetPosition() {
        return gridPosition;
    }

    private void Update() {
            Vector2 smoothedScale = Vector2.LerpUnclamped(transform.localScale, scaleVector, smoothTimer);
            transform.localScale = smoothedScale;
    }

    public void OnMouseEnter() {
        scaleVector = new Vector2(hoverScaler, hoverScaler);
        timer = smoothTimer;
    }

    public void OnMouseOver() {
        if (!heldDown) {
            CursorNumberHolder.instance.AddToNumbers(this);
        }
    }

    public void OnMouseExit() {
        if (heldDown) return;
        scaleVector = initalVector;
        timer = smoothTimer;
    }

    public bool IsHeldDown() {
        return heldDown;
    }

    public void SetHoldState(bool heldDown) {
        this.heldDown = heldDown;
    }

    public void OnBecameInvisible() {
        wiggle.enabled = false;
        boxCollider.enabled = false;
    }

    public void OnBecameVisible() {
        wiggle.enabled = true;
        boxCollider.enabled = true;
    }

    public void SetNumberType() {

    }

    public void MoveToBin() {
       // transform.position = Vector3.Lerp(transform.position);
    }
}
