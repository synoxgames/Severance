using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveInterval = 0.5f;
    public float moveSmoothing = 2f;

    public float zoomSpeed = 1.5f;
    public int zoom = 20;
    public int zoomRangeMin = 10, zoomRangeMax = 20;

    float moveTimer = 0.0f;
    float smoothTimer = 0.0f;

    Vector3 movement;

    private void Start() {
        movement = transform.position;
    }

    public void Update() {

        float zoomAxis = Input.GetAxisRaw("Mouse ScrollWheel");

        if (zoomAxis > 0 && zoom < zoomRangeMax) {
            zoom += 1;
            Camera.main.orthographicSize = zoom;
        } 

        if (zoomAxis < 0 && zoom > zoomRangeMin) {
            zoom -= 1;
            Camera.main.orthographicSize = zoom;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        if (horizontal < 0 && moveTimer <= 0) {
            movement += Vector3.left * NumberGenerator.instance.xSpacing;
            moveTimer = moveInterval;
            smoothTimer = moveSmoothing;
        }

        if (horizontal > 0 && moveTimer <= 0) {
            movement += Vector3.right * NumberGenerator.instance.xSpacing;
            moveTimer = moveInterval;
            smoothTimer = moveSmoothing;
        }

        if (vertical < 0 && moveTimer <= 0) {
            movement += Vector3.down * NumberGenerator.instance.ySpacing;
            moveTimer = moveInterval;
            smoothTimer = moveSmoothing;
        }

        if (vertical > 0 && moveTimer <= 0) {
            movement += Vector3.up * NumberGenerator.instance.ySpacing;
            moveTimer = moveInterval;
            smoothTimer = moveSmoothing;
        }

        if (moveTimer > 0) {
            moveTimer -= Time.deltaTime;
        } else {
            moveTimer = 0;
        }

        if (smoothTimer > 0) {
            transform.position = Vector3.Lerp(transform.position, movement, 1 - (smoothTimer / moveSmoothing));
            smoothTimer -= Time.deltaTime;
        } else {
            smoothTimer = 0;
        }
    }
}
