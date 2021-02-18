using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;

    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;
    public bool facingRight = true;

    //runs at the beginning of the game
    protected virtual void Start() {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input) {
       moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //switch sprite direction 
        if (moveDelta.x > 0) { //right
            transform.localScale = originalSize; // same as new Vector3(1,1,1)
            facingRight = true;
        }
        else if (moveDelta.x < 0) { //left
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
            facingRight = false;
        }

        //add push vector
        moveDelta += pushDirection;

        //reduce push force every frame based on recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //make sure we can move in this direction by casting a box first --> if return null, can move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
