using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MovementBindings {
	[SerializeField] public KeyCode left;
	[SerializeField] public KeyCode right;
	[SerializeField] public KeyCode up;
	[SerializeField] public KeyCode down;
}

public abstract class Player : MonoBehaviour {
	[SerializeField] protected const float maxSpeed = 100.0f;
	[SerializeField] MovementBindings movementBindings;

	protected Rigidbody2D body;
    protected SpriteRenderer spriteRenderer;
    protected bool canMove;
	protected float speed;

	void Start() {
		speed = maxSpeed;
	}

	public Vector2 GetMoveVector() {
		Vector2 move = Vector2.zero;
		if (Input.GetKey(movementBindings.left)) move.x--;
		if (Input.GetKey(movementBindings.right)) move.x++;
		if (Input.GetKey(movementBindings.up)) move.y++;
		if (Input.GetKey(movementBindings.down)) move.y--;
		return move;
	}

	protected virtual void Awake() {
		body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canMove = true;
	}

	protected virtual void FixedUpdate() {
        if (canMove) {
            Vector2 move = GetMoveVector().normalized * Time.deltaTime * speed;
            body.MovePosition(body.position + move);
            if (Input.GetKey(movementBindings.left)) spriteRenderer.flipX = true;
            if (Input.GetKey(movementBindings.right)) spriteRenderer.flipX = false;
        }
	}
}
