using UnityEngine;
using System.Collections;

public class AmmoPack : MonoBehaviour {
	public float ammunition = 25.0f;
	public float gunType = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 1, 0));
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag.Equals("Player")) {
			Transform gun = other.transform.Find("FirstPersonCharacter/SciFiGun_Diffuse");
			if(gun.GetComponent<Shooting>().canGetAmmo()) {
				gun.SendMessage("addAmmo", new Vector2(ammunition, gunType));
				Destroy(gameObject);
			}
		}
	}
}
