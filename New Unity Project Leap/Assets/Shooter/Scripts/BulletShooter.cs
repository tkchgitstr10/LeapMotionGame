using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour {

	public GameObject[] bullet;
	public float shootdelay = 0.1f;

	public bool emitter = false;
	public bool shootable = false;
	public bool playershooter = false;

	public int maxWaitCount = 10;

	int sushiWaitCount;
	Manager manager;
	// Use this for initialization
	
	IEnumerator Start(){
		RandomizeSushi ();
		manager = GameObject.Find ("GameManager").GetComponent<Manager> ();


		while (true) {
			if (shootable) {
				if (emitter) {
					if(--sushiWaitCount < 0){
					Instantiate (bullet [Random.Range (1, bullet.Length - 1)], gameObject.transform.position, gameObject.transform.rotation);
					RandomizeSushi ();
					}else{
						Instantiate (bullet [0], gameObject.transform.position, gameObject.transform.rotation);
					}

				}else{
					if(manager.isIkuraMode){
						Instantiate (bullet [1], gameObject.transform.position, gameObject.transform.rotation);
					}else{
						Instantiate (bullet [0], gameObject.transform.position, gameObject.transform.rotation);
					}

				}
				//sushiWaitCount-- <= 0

			}
			yield return new WaitForSeconds (shootdelay);
		}
	}

	void RandomizeSushi(){
		sushiWaitCount = Random.Range (0, maxWaitCount);
	}
}
