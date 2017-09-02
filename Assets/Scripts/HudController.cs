using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

	[Header("Life")]
	public Sprite[] lifeTipes;
	public int playerLife = 5;
	public Image life;

	[Header("Shield")]

	public Image[] shields;
	public Sprite[] shieldTipes;
	public int shieldCount;
	public float shieldCharge = 0.1f;
	private int maxShieldCharge = 3;
	public int contador;
	public float timeForRefill;
	public int activeSprite = 0;
	public int activeShield = 0;
	public bool needRefill;
	public float refillTime;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLifeManager();
		ShieldManager();
		EmptyFill();
	}

	private void ShieldManager(){
		if(needRefill && !FightManager.block){
			timeForRefill += Time.deltaTime;
			if(timeForRefill >= refillTime){
				if(activeSprite <= 11){
					shields[activeShield].sprite = shieldTipes[activeSprite];
					activeSprite++;
				}else{
					if(activeShield <= 2){
						activeShield++;
						if(activeShield >= 3){
							needRefill = false;
						}
						activeSprite = 0;
					}
				}
				timeForRefill = 0f;
			}
		}
	}

	public void EmptyFill(){
		if(FightManager.blocked){
			int tempACtiveSprite;
			tempACtiveSprite = activeSprite;
			shields[activeShield].sprite = shieldTipes[0];
			shields[activeShield].sprite = shieldTipes[tempACtiveSprite];
		}
	}

	private void PlayerLifeManager(){
		switch(playerLife){

		case 0:
			life.sprite = lifeTipes[0];
			break;

		case 1:
			life.sprite = lifeTipes[1];
			break;

		case 2:
			life.sprite = lifeTipes[2];
			break;
		case 3:
			life.sprite = lifeTipes[3];
			break;

		case 4:
			life.sprite = lifeTipes[4];
			break;

		case 5:
			life.sprite = lifeTipes[5];
			break;
		}
	}
}
