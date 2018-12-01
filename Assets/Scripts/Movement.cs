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

public class Movement : MonoBehaviour {
	[SerializeField] float speed = 100.0f;
	[SerializeField] MovementBindings movementBindings;

	Rigidbody2D body;
    SpriteRenderer spriteRenderer;

	void Start () {
		body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate () {
		Vector2 move = Util.GetMovementVector(movementBindings);
		move = move.normalized * Time.deltaTime * speed;
		body.MovePosition(body.position + move);

		if (Input.GetKey(movementBindings.left)) spriteRenderer.flipX = true;
		if (Input.GetKey(movementBindings.right)) spriteRenderer.flipX = false;
	}
}
