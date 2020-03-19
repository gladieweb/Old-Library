using UnityEngine;
using System.Collections;
using System;

public class GladieTimer : MonoBehaviour {
	
	public float delay = 0f;
	public bool repeat = false;
	public Action onTimeUp;
	
	public bool paused  = false;
	public bool running = false;
	
	private float lastStartTime;
	private float deltaTime;
	private float runnedTime;
	
	public GladieTimer() {}
	
	public void start() {	
		paused  = false;
		running = true;
		
		runnedTime = 0;
		lastStartTime = UnityEngine.Time.time;
		
		//Debug.Log("Starting timer: " + lastStartTime);
		
		StartCoroutine("startTimer", delay);
	}
	
	public void pause() {
		if(!running) return;
		paused  = true;
		running = false;
	
		float t = UnityEngine.Time.time;
		runnedTime += t - lastStartTime;
		
		deltaTime = delay - runnedTime;
		
		//Debug.Log("Pausing timer after: " + runnedTime);
			
		StopCoroutine("startTimer");
	}
	
	public void resume() {
		if(!paused) return;
		lastStartTime = UnityEngine.Time.time;
		
		//Debug.Log("Resuming timer at: " + deltaTime);
		
		paused  = false;
		running = true;
	
		StartCoroutine("startTimer", deltaTime);
	}
	
	public void stop() {
		paused = false;
		running = false;
		
		StopCoroutine("startTimer");
	}
	
	public bool stopped() {
		return !paused && !running;	
	}
	
    private IEnumerator startTimer(float timeDelay) {
    	//Esto es para que no lo afecte el TimeScale
//		System.DateTime timeToShowNextElement = System.DateTime.Now.AddMilliseconds(timeDelay*1000);
//		while (System.DateTime.Now < timeToShowNextElement) {
//			yield return null;
//		}
	  	yield return new WaitForSeconds(timeDelay);
    	
		//Esto es para el caso de que el StopCoroutine no llegue a deter todo a tiempo.
		if(running){
			if(!repeat){
				paused = false;
				running = false;
			}
			
			if(onTimeUp != null){
				onTimeUp();
			}
			
			if(repeat){
				start();
			}	
		}
    }
}
