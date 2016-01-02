using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour {
	public Texture2D crosshairTexture;
	private Rect position;   

	private float range = 30.0f;
	public AudioClip pistolShot;
	public ParticleSystem Shot;
	private RaycastHit hit;
	private Vector3 fwd;
	public AudioSource ble;

	public Vector3 forwardShot;

	public Animator anim;
	public bool rifle;
		public bool gun;

	public int maxAmmo = 200;
	public int currentAmmo = 40;
	public int clipSize = 20;
	private int currentClip;

	public AudioClip reloadSound;
	public float reloadTime = 2.0f;
	private bool isReloading = false;
	private float timer = 0.0f;



	//public GUIText ammoText;
	// Use this for initialization
	void Start () {
		currentClip = clipSize;
		position = new Rect((Screen.width - crosshairTexture.width) / 2,
		                    (Screen.height - crosshairTexture.height) /2,
		                    crosshairTexture.width,
		                    crosshairTexture.height);

		Cursor.visible = false;
		//pistolShot = GetComponent <AudioClip> ();

		 ble = GetComponent <AudioSource>();
		ble.clip = pistolShot;

		//Shot = GetComponent <ParticleSystem> ();
		anim = GetComponent <Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (gun == true) {
			forwardShot = transform.TransformDirection(Vector3.left);
		}

		if (rifle == true) {
			forwardShot = transform.TransformDirection(Vector3.back);
		}

	if (Input.GetButtonDown ("Fire1") && currentClip >0 && !isReloading) {
			currentClip --;
			ble.Play ();
			Shot.Play ();
			anim.Rebind();
			anim.SetBool ("isFire", true);

			if (Physics.Raycast(transform.position,forwardShot , out hit)) {
				if(hit.transform.tag == "Enemy" && hit.distance < range) {
					Debug.Log ("Trafiony przeciwnik");
				} else if(hit.distance < range) {
					Debug.Log ("Trafiona Sciana");
				}
				}
			}

		 else {

			anim.SetBool ("isFire", false);

		}

		if(((Input.GetButtonDown("Fire1") && currentClip == 0) || Input.GetButtonDown("Reload")) && currentClip < clipSize) {
			if(currentAmmo > 0) {
				ble.clip = reloadSound;
				ble.Play();
				anim.SetBool ("isreloat", true);
				isReloading = true;
			}


		}
		if(isReloading) {
			timer += Time.deltaTime;
			if(timer >= reloadTime) {
				int needAmmo = clipSize - currentClip;
				
				if(currentAmmo >= needAmmo) {
					currentClip = clipSize;
					currentAmmo -= needAmmo;
				} else {
					currentClip += currentAmmo;
					currentAmmo = 0;
				}
				
				ble.clip = pistolShot;
				isReloading = false;
				timer = 0.0f;
				anim.SetBool ("isreloat", false);
			}
		}

	}

	public bool canGetAmmo()
	{
		if(currentAmmo == maxAmmo) {
			return false;
		}
		return true;
	}

	void addAmmo(Vector2 data) 
	{
		int ammoToAdd = (int)data.x;
		
		if(maxAmmo - currentAmmo >= ammoToAdd) {
			currentAmmo += ammoToAdd;
		} else {
			currentAmmo = maxAmmo;
		}
	}
	void OnGUI()
	{
		GUI.DrawTexture(position, crosshairTexture);
		GUI.Label (new Rect (10, 10, 100, 20), currentClip + " / " + currentAmmo);
		 

		//new Vector2(-Screen.width/2 +100, -Screen.height /2 +30);
	}

}
