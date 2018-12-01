using System.Collections;
using System.Collections.Generic;

public class Timer {
	public bool isRunning { get; private set; }
	public float timeElapsed { get; private set; }
	bool countUp;
	float initialValue;

	public Timer(float startTime = 0f, bool isDone = false) {
		isRunning = false;
		countUp = startTime == 0;
		timeElapsed = (isDone ? 0f : startTime);
		initialValue = startTime;
	}

	public void start() {
		reset();
		isRunning = true;
	}

	public void update(float dt) {
		if (!isRunning) return;

		if (countUp) timeElapsed += dt;
		else timeElapsed -= dt;
	}

	// Only for count down timers
	public bool isDone() {
		return timeElapsed <= 0f;
	}

	public void reset() {
		timeElapsed = initialValue;
		isRunning = false;
	}

	public float stop() {
		float ret = timeElapsed;
		reset();
		return ret;
	}
}
