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

public abstract class Movement : MonoBehaviour {
	[SerializeField] float speed = 100.0f;
	[SerializeField] MovementBindings movementBindings;

	Rigidbody2D body;
    SpriteRenderer spriteRenderer;

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
	}

	protected virtual void FixedUpdate() {
		Vector2 move = GetMoveVector();
		move = move.normalized * Time.deltaTime * speed;
		body.MovePosition(body.position + move);

		if (Input.GetKey(movementBindings.left)) spriteRenderer.flipX = true;
		if (Input.GetKey(movementBindings.right)) spriteRenderer.flipX = false;
	}
}
