using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : PlayerController {
	[Header("Attack")]
	public bool canAttack;
	private bool attack;
	public float attacking = 0.5f;
	public bool combo;
	public bool canCombo;
	public int nCombo;
	public float combining = 0.5f;
	public float timeForReactive;

	[Header("Blocking")]
	public static bool block;  
	public static bool blocked;
	public static bool canBlock = true;
	public int nHit;
	public float recoverBlock;



	private void Start(){
		
	}

	private void Update(){

		if(Input.GetKeyDown(KeyCode.Z)){
			//Hit (1);
		}

		Block ();

		if(!block){
			Combo ();
			Attack ();
			if(!canAttack){
				timeForReactive += Time.deltaTime;
				if(timeForReactive >= 0.1f){
					canAttack = true;
					timeForReactive = 0f;
				}
			}
		}

	}

	private void Attack(){
		if(Input.GetMouseButtonDown(0) && canAttack && !combo && nCombo != 3){
			playerAnim.SetTrigger ("Attack");
			attack = true;
		}

		if(attack){
			attacking -= Time.deltaTime;
			if (attacking > 0f) {
				noAction = true;
			} else {
				attack = false;
				noAction = false;
				attacking = .5f;
			}
		}
		if(!noAction){
			playerAnim.SetFloat ("AttackX", playerLastMovement.x);
			playerAnim.SetFloat ("AttackY", playerLastMovement.y);
		}
	}

	private void Combo(){
		if (attacking < .5f) {
			if (canCombo) {
				if (Input.GetMouseButtonDown (0)) {
					combining = 1f;
					combo = true;
					if(nCombo < 2){
						nCombo++;
					}else{
						nCombo = 2;
					}
				}
			}
			canCombo = true;
		} else {
			canCombo = false;
			ResetCombo ();
		}

		if(combo && nCombo == 1){
			combining -= Time.deltaTime;
			if (combining < 1f) {
				playerAnim.SetTrigger ("Combo");
			}
		}

		if(combo && nCombo == 2){
			combining -= Time.deltaTime;
			if (combining < 1f) {
				playerAnim.SetTrigger ("Combo2");
			}
		}
	}



	private void ResetCombo(){
		if(combining <= 0f || nCombo > 2){
			nCombo = 0;
			combining = 1f;
			combo = false;
			playerAnim.ResetTrigger ("Combo");
			playerAnim.ResetTrigger ("Combo2");
			canAttack = false;
		}
	}


	private void Block(){
		
		block = (Input.GetMouseButton(1) && canBlock) ? true : false;
		ResetHiting ();

		playerAnim.SetBool ("Block", block);
		playerAnim.SetFloat ("BlockX", playerLastMovement.x);
		playerAnim.SetFloat ("BlockY", playerLastMovement.y);
	}

	private void Hit(int damage){
		if (block && damage > 0f && canBlock) {
			nHit++;

			if(nHit == 1){
				playerAnim.SetTrigger ("Hit1");
			}else if(nHit == 2){
				playerAnim.SetTrigger("Hit2");
			}else if(nHit == 3){
				playerAnim.SetTrigger("Hit3");
			}
		}
	}


	private void ResetHiting(){
		if(nHit >= 3){
			canBlock = false;
			recoverBlock += Time.deltaTime;
			if(recoverBlock >= 3f){
				nHit = 0;
				recoverBlock = 0f;
				canBlock = true;
				blocked = false;
			}
		}
	}
		

	private void OnCollisionEnter2D(Collision2D other){
		if(other.collider.tag == "HurtPoint"){
			if(other.otherCollider.tag == "Player"){
				Physics2D.IgnoreLayerCollision(12,9);
				Physics2D.IgnoreLayerCollision(12,11);

				playerAnim.SetTrigger("Hurt");
				Debug.Log ("Hurt");	
			}

			if(other.otherCollider.tag == "Basic Shield"){
				Physics2D.IgnoreLayerCollision(11,12);
				Debug.Log(other.otherCollider.name);
				Hit(1);
				blocked = true;

			}

			if(other.otherCollider.tag == "Basic Sword"){
				Physics2D.IgnoreLayerCollision(9,12);
				Debug.Log(other.otherCollider.name);
			}
		}
	}
}
