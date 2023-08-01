using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class OrbeController : MonoBehaviour
{
	//public Volume volume;
	public int life =5;
	public float speed = 50f;
	public Vector3 velocity = Vector3.zero;
	
	private Animator anim;
    private FollowPlayer followPlayer;
    private Vector3 targetPosition;
    private bool intro = true;
    
	private void Awake() {
		anim = GetComponent<Animator>();
		followPlayer = GetComponentInParent<FollowPlayer>();
	}

	private void Start()
	{
		targetPosition = transform.localPosition;
	}
	private void Update()
	{
		transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, speed * Time.deltaTime);
	}
	//Funcion de colision con enemigo
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Enemigo")){
			life--;
			RandomYMovement();
			Debug.Log("Life: " + life);
			Destroy(other.gameObject);
			anim.SetTrigger("Damage");
			float saturation = (5-life)*20f;
			followPlayer.ChangeSaturation(-saturation);
			followPlayer.ChangeVignette(5-life);
			if(life <= 0){
				followPlayer.ChangeAudioVolume(0f);
				Destroy(gameObject, 8f);
			}

			if (life == 5 || life == 4)
			{
				followPlayer.ChangeAudioClip(0);
			}
			else if (life == 3 || life == 2)
			{
				followPlayer.ChangeAudioClip(1);
			}
			else if (life == 1 || life == 0)
			{
				followPlayer.ChangeAudioClip(2);
			}
		}
	}
	public void Heal(){
		if (life < 5) life++;
		float saturation = (5-life)*20f;
		followPlayer.ChangeSaturation(saturation);
		followPlayer.ChangeVignette(5-life);
		if (life == 5 || life == 4)
		{
			followPlayer.ChangeAudioClip(0);
		}
		else if (life == 3 || life == 2)
		{
			followPlayer.ChangeAudioClip(1);
		}
		else if (life == 1 || life == 0)
		{
			followPlayer.ChangeAudioClip(2);
		}
		anim.SetTrigger("Heal");
		Debug.Log("Life: " + life);
	}

	private IEnumerator Intro()
	{
		targetPosition = new Vector3(transform.localPosition.x, 8f, transform.localPosition.z);
		yield return new WaitForSeconds(4f);
	}
	private IEnumerator RandomMovementOnY()
	{
		while (true)
		{
			yield return new WaitForSeconds(8f);
			RandomYMovement();
		}
	}
	
	public void StartIntro(){
		StartCoroutine(Intro());
	}
	public void StartRandomMovement(){
		StartCoroutine(RandomMovementOnY());
	}
	
	private void RandomYMovement()
	{
		float randomY;
		if (intro)
		{
			randomY = Random.Range(-8f, 0f);
			intro = false;
		}
		else randomY = Random.Range(-8f, 8f);
		targetPosition = new Vector3(transform.localPosition.x, randomY, transform.localPosition.z);
	}
}
