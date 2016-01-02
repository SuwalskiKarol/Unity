using UnityEngine;
using System.Collections;

public class PlayerStat : MonoBehaviour {
	
	private float maxHealth = 100;
	private float currentHealth = 100;
	private float maxArmour = 100;
	private float currentArmour = 100;

	
	public Texture2D healthTexture;
	public Texture2D armourTexture;


	private float barWidth;
	private float barHeight;

	private float canHeal = 0.0f;

	void Awake()
	{
		barHeight = Screen.height * 0.02f;
		barWidth = barHeight * 10.0f;

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)) {
			takeHit(30);
		}

		if(canHeal > 0.0f) {
			canHeal -= Time.deltaTime;
		}
		if(canHeal <= 0.0f && currentHealth < maxHealth) {
			regenerate(ref currentHealth, maxHealth);
		}
	}

		
		void regenerate(ref float currentStat, float maxStat)
	{
		currentStat += maxStat * 0.005f;
		Mathf.Clamp(currentStat, 0, maxStat);
	}


	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
		                         Screen.height - barHeight - 10,
		                         currentHealth * barWidth / maxHealth,
		                         barHeight),
		                healthTexture);
		GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
		                         Screen.height - barHeight * 2 - 20,
		                         currentArmour * barWidth / maxArmour,
		                         barHeight),
		                armourTexture);

	}

	void takeHit(float damage) 
	{
		if(currentArmour > 0) {
			currentArmour -= damage;
			if(currentArmour < 0) {
				currentHealth += currentArmour;
				currentArmour = 0;
			}
		} else {
			currentHealth -= damage;
		}

		if(currentHealth < maxHealth) {  // Tego ifa!
			canHeal = 5.0f;
		}

		currentArmour = Mathf.Clamp(currentArmour, 0, maxArmour);
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}
}
