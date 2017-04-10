using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	//public GameObject Inkpanel;
	public float destroydelay = 2.0f;
	public float launchforce = 20.0f;
	public int Score = 100;
	public bool isUfo = false;
	public bool isSquid = false;
	public bool isIkura = false;
	Transform tranceform_ufo;
	AudioSource audiosource;
	public AudioClip audioclip;

	// Use this for initialization
	void Start () {
	
		Rigidbody rigidbody = gameObject.GetComponent<Rigidbody> ();
		Transform transform = gameObject.transform;
		if (isUfo)
		tranceform_ufo = gameObject.transform.GetChild (0);
		audiosource = gameObject.GetComponent<AudioSource> ();
		rigidbody.AddForce (transform.forward * launchforce,ForceMode.Impulse);
		Destroy (gameObject, destroydelay);
	}

	void Update(){
		if (isUfo)
			tranceform_ufo.Rotate (0, 0, 1f);
	}


	void OnCollisionEnter(Collision c)
	{
		if (isUfo) {
			if (c.gameObject.transform.tag == "Bullet") {
				Instantiate (Resources.Load ("Explosion2"), gameObject.transform.position, gameObject.transform.rotation);
				audiosource.PlayOneShot(audioclip);
				isUfo = false;
				Destroy(c.gameObject);
				FindObjectOfType<Manager>().AddScore(Score);
				if(isSquid)
					FindObjectOfType<Manager>().AddSquidTime();
				if(isIkura)
					FindObjectOfType<Manager>().AddIkuraTime();
					
				Destroy (gameObject, 0.3f);
			}
		}
		//ink bullet
		/*
		if (c.gameObject.tag == "Wall") {

			//Transform rotation = c.gameObject.transform.rotation;

			GameObject panel =  (GameObject)Instantiate(Inkpanel, gameObject.transform.position, c.gameObject.transform.rotation);
			panel.transform.Rotate (0,Random.Range(0,300),0);
		}
		if (c.gameObject.tag == "Ink") {
			GameObject panel = (GameObject)Instantiate(Inkpanel, gameObject.transform.position, c.gameObject.transform.rotation);
			panel.transform.Translate(0,-0.5f,0);
			panel.transform.Rotate (0,Random.Range(0,300),0);
		}

		Destroy(gameObject);
		*/

	}


}
