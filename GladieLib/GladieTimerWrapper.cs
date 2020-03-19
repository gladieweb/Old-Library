using UnityEngine;
using System.Collections;
using System;

public class GladieTimerWrapper 
{
	private GameObject timerContainer;
	private GladieTimer timer;
	
	public GladieTimerWrapper() {
		timerContainer = new GameObject();
		timerContainer.name = "Timer";
		timer = timerContainer.AddComponent<GladieTimer>();
	}
	
	public bool Repeat {
		get {
			return timer.repeat;
		}
		set {
			timer.repeat = value;
		}
	}


	public float Delay {
		get {
			return timer.delay;
		}
		set {
			timer.delay = value;
		}
	}
	
	public bool isStopped {
		get {
			return timer.stopped();
		}
	}
	
	public bool isPaused {
		get {
			return timer.paused;
		}
	}
	
	public bool isRunning {
		get {
			return timer.running;
		}
	}
	
	public Action OnTimeUp {
		get {
			return timer.onTimeUp;
		}
		set {
			timer.onTimeUp = value;
		}
	}
	
	public void start() {
		timer.start();
	}
	
	public void stop() {
		timer.stop();
	}
	
	public void pause() {
		timer.pause();
	}
	
	public void resume() {
		timer.resume();
	}
	
	public void destroy() {
		timer.StopAllCoroutines();
		GameObject.Destroy(timerContainer);
	}
}

