﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[SerializeField] GameObject chickenPrefab;
	[SerializeField] List<GameObject> players;

	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }

	void Awake() {
		if (instance == null) instance = this;
	}

	void OnDestroy() {
		if (instance == this) instance = null;
	}

	void Start() {
		SpawnChicken();
		SpawnChicken();
		SpawnChicken();
		SpawnChicken();
	}

	public void SpawnChicken() {
		Vector2 spawnPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		Instantiate(chickenPrefab, spawnPos.normalized * 20f, Quaternion.identity);
	}

	public List<GameObject> GetPlayers() {
		return players;
	}
}
