using UnityEngine;
using System.Collections;

public class FairyMagic : MonoBehaviour {

	public GameObject WizardPos;

	public GameObject Wizard;

    GameObject healthBar;

    public Vector3 wizPosition;
	public Quaternion wizRotation;
	float distFromWiz;
	public GameObject Shield;
	public GameObject FairyShot;
	public GameObject FairyHeal;
	public Vector3 offsetHeal;

	public float DistToWiz;

	public GameObject SpeedEffect;

	public Collider NearDome;

	public float fairyshieldtimer = 10f;

	public AudioSource ShieldSound;
	public AudioSource ShootSound;
	public AudioSource SpeedSound;
	public AudioSource HealSound;

	// Use this for initialization
	void Start () {

		fairyshieldtimer = 10f;
        healthBar = GameObject.FindGameObjectWithTag("HealthContainer");

        //StartCoroutine(PresenceCheck());
    }

    IEnumerator PresenceCheck(){

		yield return new WaitForSeconds (1f);
	}

	// Update is called once per frame
	void Update () {

		distFromWiz = (WizardPos.transform.position - transform.position).magnitude;

		if (distFromWiz <= 6f) {
			if (fairyshieldtimer < 10) {
				fairyshieldtimer += (Time.deltaTime) * 1;
			}
		}
			
		if (fairyshieldtimer >= 10 && distFromWiz <= 5f) {
			fairyshieldtimer = 10;
			//if ( enemyspell is nearby){
			//}
			wizPosition = new Vector3 (WizardPos.transform.position.x, (WizardPos.transform.position.y), (WizardPos.transform.position.z));
			wizRotation = Quaternion.LookRotation (WizardPos.transform.position, Vector3.up);

			Collider[] hitColliders = Physics.OverlapSphere (wizPosition, 4f);
			int i = 0;

			//Collider[] hitColliders = Physics.OverlapSphere (wizPosition, 4f);

			while (i < hitColliders.Length) {
				if (hitColliders [i].tag == "EnemySpell") {
					ShieldSound.Play ();
					Shield.transform.position = wizPosition;
					Instantiate (Shield);
					fairyshieldtimer -= 10;
					i = hitColliders.Length;

				}
				i++;
			}

			if (Wizard.GetComponent<Human_Wizard> ().health <= 50) {
				i = 0;
				while (i < hitColliders.Length) {
					if (hitColliders [i].tag == "MobMelee" || hitColliders [i].tag == "MobLeader" || hitColliders [i].tag == "MobRanged") {
						//Speedboost code here!

						Wizard.GetComponent<Human_Wizard> ().speed = 16;
						Wizard.GetComponent<Human_Wizard> ().isSpeedBoosted = true;

						SpeedSound.Play ();

						SpeedEffect.transform.position = wizPosition;
						Instantiate (SpeedEffect);

						fairyshieldtimer -= 5;
						i = hitColliders.Length;
					}
					i++;
				}
			} else {
				Collider[] ShotTargetCollider = Physics.OverlapSphere (wizPosition, 15f);
				int j = 0;

				while (j < ShotTargetCollider.Length) {
		
					if (ShotTargetCollider [j].tag == "MobRanged" || ShotTargetCollider [j].tag == "MobMelee" || ShotTargetCollider [j].tag == "MobLeader") {
						FairyShot.transform.position = transform.position;
		
						ShootSound.Play ();
						Instantiate (FairyShot);
						FairyShot.GetComponent<FairyBullet>().SetTarget(ShotTargetCollider[j].gameObject);
						fairyshieldtimer -= 3;
						j = ShotTargetCollider.Length;

					}
					j++;
				}
			}

			if(fairyshieldtimer >= 10){
				if(Wizard.GetComponent<Human_Wizard>().health < 100 && Wizard.GetComponent<Human_Wizard>().health > 0)
                {
					Wizard.GetComponent<Human_Wizard>().health += 5;
                    healthBar.transform.GetChild(0).GetComponent<HealthBar>().updateHealthBar();

                    offsetHeal = new Vector3 (wizPosition.x, wizPosition.y + 2f, wizPosition.z);

					HealSound.Play ();
					FairyHeal.transform.position = offsetHeal;
					Instantiate (FairyHeal);

					if(Wizard.GetComponent<Human_Wizard>().health > 100){
						Wizard.GetComponent<Human_Wizard>().health = 100;
					}

					fairyshieldtimer -= 1;
				}	
			}



			//else{

			//if (Wizard.GetComponent<WizardStats> ().HP < 50) {
			//}





			//offsetShield = new Vector3(Shield.transform.position.x, Shield.transform.position.y + 0.5f, Shield.transform.position.z + 2);
			//Shield.transform.rotation = Quaternion.RotateTowards (transform.rotation, wizRotation, 999999f);
			//Shield.transform.position = offsetShield;
			//Instantiate (Shield);
			//fairyshieldtimer = 6;
		}

	}

	public void OnTriggerEnter(Collider other)
	{
		/*
		if (other.tag == "EnemySpell") {
			if (fairyshieldtimer >= 6) {
				Destroy (other.gameObject);
			}
		}
		*/
	}
}
