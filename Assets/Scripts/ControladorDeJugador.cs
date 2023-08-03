using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeJugador : MonoBehaviour{
	//Activar animaciones
	private Animator animator;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private BoxCollider2D aQ;
	public GameObject ataque1;
	public Transform linterna;
	//Variables de desplazamiento
	public float velocidadHorizontal = 1f;
	public float fuerzaVertical = 10f;
	//Estados de movimiento
	private bool enAire;
	private float xDir;
	private float yDir;
	//Ataques
	private bool ataqueQ;
	private bool ataqueW;
	private bool ataqueE;
	private bool ataqueR;
	private bool enAtaque;
	private bool canPlay = false;
	
	void Awake(){
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		aQ = ataque1.GetComponent<BoxCollider2D>();
	}
	void Start(){
		enAire = true;
		StartCoroutine(GetControl(4f));
	}
    // Update is called once per frame
    void Update()
    {
	    if (!canPlay) return;
		xDir	= Input.GetAxisRaw("Horizontal");
		yDir	= Input.GetAxisRaw("Vertical");
		ataqueQ	= Input.GetButtonDown("Fire1");
		ataqueW	= Input.GetButtonDown("Fire2");
		ataqueE	= Input.GetButtonDown("Fire3");
		ataqueR	= Input.GetButtonDown("Fire4");
		
		//Ataques
		if(!enAtaque){
			if(ataqueQ){
				Debug.Log("Ataque Q");
				animator.SetTrigger("abilityQ");
				StartCoroutine(AtaqueQ());
				return;
			}
			if(ataqueW){
				Debug.Log("Ataque W");
				animator.SetTrigger("ataqueW");
			}
			if(ataqueE){
				Debug.Log("Ataque E");
				animator.SetTrigger("ataqueE");
			}
			if(ataqueR){
				Debug.Log("Ataque R");
				animator.SetTrigger("ataqueR");
			}
		}
    }
	void FixedUpdate(){
		//Desecender
		if(yDir > 0.1f){
			animator.SetBool("descender",false);
		}
		if(yDir < -0.1f){
			animator.SetBool("descender",true);
			if(enAire){
				rb.AddForce(new Vector2(0, yDir),ForceMode2D.Impulse);
			}
		}
		//Mov Horizontal
		if(xDir==0f){
			animator.SetBool("Running",false);
		}
		if(xDir>0.1f || xDir<-0.1f){
			if (xDir<-0.1f){
				sr.flipX = true;
				aQ.offset = new Vector2(-1.5f,0);
				linterna.transform.rotation = Quaternion.Euler(0,0,90);

			}else if(xDir>0.1f){
				sr.flipX = false;
				aQ.offset = new Vector2(1.5f,0);
				linterna.transform.rotation = Quaternion.Euler(0,0,270);
			}		
			if(enAire){
				rb.AddForce(new Vector2(xDir*velocidadHorizontal*0.5f,0),ForceMode2D.Impulse);
			}else if(yDir<-0.1f){
				rb.AddForce(new Vector2(xDir*velocidadHorizontal*0.7f,0),ForceMode2D.Impulse);
			}else{
				rb.AddForce(new Vector2(xDir*velocidadHorizontal,0),ForceMode2D.Impulse);
				animator.SetBool("Running",true);
			}
		}
		//Saltar
		if(yDir>0.1f && !enAire){
			rb.AddForce(new Vector2(0, yDir*fuerzaVertical),ForceMode2D.Impulse);
			animator.SetBool("saltar",true);
		}
	}
	//Lugar
	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Floor"){
			enAire = false;
			animator.SetBool("saltar",false);
		}
	}
	void OnTriggerExit2D(Collider2D collision) {
		if(collision.gameObject.tag == "Floor"){
			enAire = true;
		}
	}
	//Ataques
	IEnumerator AtaqueQ(){
		enAtaque = true;
		aQ.enabled = true;
		yield return new WaitForSeconds(0.3f);
		aQ.enabled = false;
		enAtaque = false;
	}
	
	IEnumerator GetControl(float time){
		yield return new WaitForSeconds(time);
		canPlay = true;
	}
}
