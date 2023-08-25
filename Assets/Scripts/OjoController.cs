using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class OjoController : MonoBehaviour
{
	public bool inRange = false;
	public Vector3 velocity = Vector3.zero;
	
	public float speed = 100f;
	
	private bool isAttacking = false;
	private Transform orb;
	private Transform player;
	private Transform _StartEvadePosition;
	private Vector3 _InitialPosition;
    // Update is called once per frame
	void Start(){
		_InitialPosition = transform.position;
		orb = GameObject.FindGameObjectWithTag("Orb").GetComponent<Transform>();
	}
    void Update()
    {
	    //transform.position = Vector3.MoveTowards(transform.position, orb.position, plerp);
    }
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			if (!isAttacking)
			{
				isAttacking = true;
				_StartEvadePosition = transform;
				player = other.gameObject.GetComponent<Transform>();
				Debug.Log("Colision con Ojo");
				DOEvasion();
			}
		}
	}
    
	
	// Corroutine to move Eye towards player for 1 sec and then attack
	private IEnumerator MoveTowardsOrb()
	{
		yield return new WaitForSeconds(1f);
	}
	private void DOEvasion()
	{
		// random between 0.2 and 0.6
		float evasionOffset = UnityEngine.Random.Range(1f, 3f);

		Vector3 targetPosition = transform.position - player.position;// Dont use this
		if (player.position.y > transform.position.y)
			targetPosition = new Vector3(transform.position.x + (1 + evasionOffset) ,transform.position.y + (evasionOffset *0.5f) , 0f);
		else
			targetPosition = new Vector3(transform.position.x + (1 + evasionOffset) ,transform.position.y - (evasionOffset * 0.5f), 0f);
        //targetPosition.x = transform.position.x * 2f - (targetPosition.x);
		// Do Move with InSine ease
		Sequence moveSequence = DOTween.Sequence();
		//moveSequence.Append(transform.DOMove(targetPosition, 1f).SetEase(Ease.InSine));
		moveSequence.Append(transform.DOShakePosition(0.2f, 0.2f, 10, 90, false, false));
		moveSequence.Append(transform.DOPath(new Vector3[] { transform.position, targetPosition }, 0.4f,
			PathType.CatmullRom, PathMode.Sidescroller2D, 0));
		
		if (player.position.y > transform.position.y)
			targetPosition = new Vector3(transform.position.x + (1 + evasionOffset * 2f) ,transform.position.y - (1 + evasionOffset), 0f);
		else
			targetPosition = new Vector3(transform.position.x + (1 + evasionOffset * 2f) ,transform.position.y + (1 + evasionOffset), 0f);
		moveSequence.Append(transform.DOShakePosition(0.2f, 0.2f, 10, 90, false, false));
		moveSequence.Append(transform.DOMove(targetPosition, 0.6f).SetEase(Ease.InSine));
		moveSequence.AppendCallback(() =>
		{
			StartCoroutine(WatchPlayer());
		});
		
	}
	private IEnumerator WatchPlayer()
	{
		float startTime = Time.time; // Guarda el tiempo de inicio
		float timeLimit = 0.4f; 
		// Distance in X between self and player
		float xDistance = transform.position.x - player.position.x;; 
		while (Time.time - startTime < timeLimit)
		{
			// transform in X is the player position + the distance between self and player
			transform.position = new Vector3(player.position.x + xDistance, transform.position.y, transform.position.z);
			//transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x + xDistance, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);
			yield return null;			
		}
		StartCoroutine(AttackOrb());
	}
	// Corroutine for attack
	private IEnumerator AttackOrb()
	{
		float startTime = Time.time; // Guarda el tiempo de inicio
		float timeLimit = 10f; 
		while (Time.time - startTime < timeLimit)
		{
			transform.position = Vector3.SmoothDamp(transform.position, orb.position, ref velocity, speed * Time.deltaTime);
			yield return null;			
		}
		StartCoroutine(Redraw());
	}
    // Redraw to _StartEvadePosition
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Redraw()
    {
	    Debug.Log("Redraw");
	    while (true)
	    {
		    if (transform.position == _InitialPosition)
		    {
			    isAttacking = false;
			    yield break;
		    }
		    transform.position = Vector3.SmoothDamp(transform.position, _InitialPosition, ref velocity, speed * 2f * Time.deltaTime);
		    yield return null;			
	    }
	}
}
