using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeJugador : MonoBehaviour
{
	//Activar animaciones
	private Animator animator;
	private Rigidbody2D rb;
	//Variables de desplazamiento
	public float velocidadHorizontal;
	public float fuerzaVertical;
	//Estados de movimiento
	private bool saltar;
	private bool correr;
	public bool enAire;
	private bool jumpKey;
	private bool downKey;
	private float xDir;
	private float yDir;

	void Awake(){
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	void Start(){
		velocidadHorizontal = 0.9f;
		fuerzaVertical = 30f;
		saltar = false;
		correr = false;
		enAire = false;
    }
    // Update is called once per frame
    void Update(){
		xDir	= Input.GetAxisRaw("Horizontal");
		yDir	= Input.GetAxisRaw("Vertical");
    }
	void FixedUpdate(){
		//Saltar
		if(yDir>0.1f && !enAire){
			rb.AddForce(new Vector2(0, yDir*fuerzaVertical),ForceMode2D.Impulse);
		}
		//Desecender rapido
		if(yDir<-0.1f && enAire){
			rb.AddForce(new Vector2(0, yDir),ForceMode2D.Impulse);
		}
		//Mov Horizontal
		if(xDir>0.1f || xDir<-0.1f){
			if(enAire){
				rb.AddForce(new Vector2(xDir*velocidadHorizontal*0.6f,0),ForceMode2D.Impulse);
			}else{
				rb.AddForce(new Vector2(xDir*velocidadHorizontal,0),ForceMode2D.Impulse);
			}
			
		}
	}
	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Suelo"){
			enAire = false;
		}
	}
	void OnTriggerExit2D(Collider2D collision) {
		if(collision.gameObject.tag == "Suelo"){
			enAire = true;
		}
	}
}
