using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private GameObject player;
	public Animator enemyAnim;

	public Vector2 playerPos;
	public float enemySpeed;
	public bool canMove;
	private Vector2 enemyLastMovement;
	private Vector2 enemyMovement;
	private Vector2 playerMovement;
	private bool canAttack;
	public float timeForAttack;
	public float minDistanceFromObj;
	public float maxDistanceFromObj;
	private bool isFollowObj;
	private bool hasSpaceForAttack;
	public float distanceFromPlayer;
	public float timeForAppearing;
	public float timeForHurting;
	public bool hurt;
	public bool right;

	private void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	private void Update(){
		FollowPlayer ();
		Attack();
	}

	private void FollowPlayer(){

		if (CanFollowPlayer() && !hurt) {

			playerPos = player.transform.position;

			if(Mathf.Abs(DistanceFromPlayer().x) > 0f){
				playerPos.x = player.transform.position.x;
				playerPos.y = 0f;
			}else{
				playerPos.y = player.transform.position.y;
			}
								
			//transform.position = Vector2.MoveTowards (transform.position, player.transform.position, enemySpeed * Time.deltaTime);

			enemyMovement = DistanceFromPlayer() / DistanceFromPlayer().magnitude;

			enemyMovement.x = enemyMovement.x > 0.1f ? 1f : enemyMovement.x < -0.1f ? -1f : 0f;
			enemyMovement.y = enemyMovement.y > 0.1f ? 1f : enemyMovement.y < -0.1f ? -1f : 0f;

			enemyLastMovement = new Vector2(enemyMovement.x, enemyMovement.y);
			enemyLastMovement.x = enemyLastMovement.x > 0.1f ? 1f : enemyLastMovement.x < -0.1f ? -1f : 0f;
			enemyLastMovement.y = enemyLastMovement.y > 0.1f ? 1f : enemyLastMovement.y < -0.1f ? -1f : 0f;

			enemyAnim.SetFloat("MovementX", enemyMovement.x);
			enemyAnim.SetFloat("MovementY", enemyMovement.y);
		}else{
			if(hurt){
				timeForHurting += Time.deltaTime;
				if(timeForHurting > 0.8f){
					hurt = false;
					timeForHurting = 0f;
				}
			}
		}

		enemyAnim.SetFloat("IdleX", enemyLastMovement.x);
		enemyAnim.SetFloat("IdleY", enemyLastMovement.y);
		enemyAnim.SetBool("CanMove", canMove);
	}

	public virtual void Attack(){
		if(DistanceFromPlayer().magnitude <= 1f){
			enemyAnim.SetFloat("AttackX", enemyLastMovement.x);
			enemyAnim.SetFloat("AttackY", enemyLastMovement.y);
			enemyAnim.SetTrigger("Attack");
		}
	}

	private Vector3 DistanceFromPlayer(){
		return player.transform.position - transform.position;
	}

	private bool CanFollowPlayer(){
		if(timeForAppearing <= 0f){
			return canMove = (DistanceFromPlayer().magnitude < maxDistanceFromObj && DistanceFromPlayer().magnitude > minDistanceFromObj) ? true : false;
		}else{
			timeForAppearing -= Time.deltaTime;
			return false;
		}
	}

	private void OnCollisionEnter2D(Collision2D other){
		if(other.collider.tag == "Basic Sword"){
			
			Debug.Log ("Hit!");
			enemyAnim.SetTrigger("Hurt");
			hurt = true;
			Debug.Log(other.collider.name);
		}
	}
	
}
