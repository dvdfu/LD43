using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour {
	[SerializeField] protected const float maxSpeed = 100.0f;
	[SerializeField] protected PlayerData playerData;

	protected Rigidbody2D body;
    protected SpriteRenderer spriteRenderer;
    protected bool canMove;
	protected float speed;

	void Start() {
		speed = maxSpeed;
	}

	public Vector2 GetMoveVector() {
		Vector2 move = Vector2.zero;
		if (Input.GetKey(playerData.movementBindings.left)) move.x--;
		if (Input.GetKey(playerData.movementBindings.right)) move.x++;
		if (Input.GetKey(playerData.movementBindings.up)) move.y++;
		if (Input.GetKey(playerData.movementBindings.down)) move.y--;
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
            if (Input.GetKey(playerData.movementBindings.left)) spriteRenderer.flipX = true;
            if (Input.GetKey(playerData.movementBindings.right)) spriteRenderer.flipX = false;
        }
	}
}
