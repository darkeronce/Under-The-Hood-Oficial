using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[HideInInspector]
	public Animator playerAnim;

	public float playerSpeed;
	[HideInInspector]
	public Vector2 playerMovement;
	public static Vector2 playerLastMovement;
	public bool canMove;
	public bool playerLose;
	public static bool noAction;
	bool canCheckFirtsTouched;
	public bool xFirst, yFirst;
	bool getRand;
	bool chooset;
	float movX,movY;
	bool firstHitX, firstHitY;


	private void Awake(){
		playerAnim = GetComponent<Animator> ();
		playerMovement = new Vector2 (0f, -1f);
		playerLastMovement = new Vector2 (0f, -1f);
	}

	private void Update(){
		playerMovement = new Vector2(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		if(!playerLose && !noAction){
			MovementAllWays (playerMovement);
		}
	}

	private void MovementOnlyOneWay(Vector2 Axis){
		if (Mathf.Abs (Axis.x) == Mathf.Abs (Axis.y)) { Axis.x = 0f; Axis.y = 0f;}
		canMove = (Axis.x != 0f || Axis.y != 0f) ? true : false;

		if(canMove){
			transform.position += Vector3.Lerp (transform.position, Axis, 1f) * Time.deltaTime * playerSpeed;
			if(Input.GetAxis("Horizontal") <= 0.1f || Input.GetAxis("Vertical") <= 0.1f){
				playerLastMovement = Axis;
			}
			playerAnim.SetFloat ("MovementX", Axis.x);
			playerAnim.SetFloat ("MovementY", Axis.y);
		}
			
		playerAnim.SetFloat ("LastMovementX", playerLastMovement.x);
		playerAnim.SetFloat ("LastMovementY", playerLastMovement.y);
		playerAnim.SetBool ("CanMove", canMove);
	}


	private void MovementAllWays(Vector2 Axis){

		if(xFirst || !firstHitX){
			 movX = Input.GetAxisRaw ("Horizontal");
			if(movX > 0f){
				Debug.Log("Mayor");
			}
			if(xFirst){
				firstHitX = true;
			}
		}
		if(yFirst || !firstHitY){
			movY = Input.GetAxisRaw("Vertical");
			if(movY > 0f){
				Debug.Log("Mayor");
			}
			if(yFirst){
				firstHitY = true;
			}
		}

		if(xFirst){
			if(movX == 0f){
				xFirst = false;
				getRand = false;
				firstHitX = false;
				firstHitY = false;
			}
			movY = 0f;
		}

		if(yFirst){
			if(movY == 0f){
				yFirst = false;
				getRand = false;
				firstHitX = false;
				firstHitY = false;
			}
			movX = 0f;
		}

		if(Mathf.Abs(movX) > Mathf.Abs(movY) && !yFirst){
			xFirst = true;
			movY = 0f;
		}else if(Mathf.Abs(movY) > Mathf.Abs(movX) && !xFirst){
			yFirst = true;
			movX = 0f;
		}else if(Mathf.Abs(movX) == 1f && Mathf.Abs(movY) == 1f && !getRand){
			int ran = Random.Range(0,2);
			Debug.Log(ran);
			if(ran == 0){
				xFirst = true;
				movY = 0f;
			}else if(ran == 1){
				yFirst = true;
				movX = 0f;
			}

			getRand = true;
		}








		canMove = (Axis.x != 0f || Axis.y != 0f) ? true : false;
		if(canMove){
			transform.position += Vector3.Lerp (transform.position, Axis, 1f) * Time.deltaTime * playerSpeed;
			if(Input.GetAxis("Horizontal") <= 0.1f || Input.GetAxis("Vertical") <= 0.1f){
				playerLastMovement = new Vector2(movX, movY);
			}
			playerAnim.SetFloat ("MovementX", movX);
			playerAnim.SetFloat ("MovementY",movY);
		}

		playerAnim.SetFloat ("LastMovementX", playerLastMovement.x);
		playerAnim.SetFloat ("LastMovementY", playerLastMovement.y);
		playerAnim.SetBool ("CanMove", canMove);

	}
}
