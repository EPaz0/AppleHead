using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Health")]

	public int maxHealth = 3;
	public int currentHealth;

	public HealthBar healthBar;
	public float transparentVal;
	
	public GameManagerScript gameManager;
	private bool isDead;

	[Header("iFrames")]
	[SerializeField]private float iFramesDuration;
	[SerializeField]private int numberOfFlashes;
	private SpriteRenderer spriteRend;
	private Material currentMat;

	public ChangeImage changeImage;
	public AudioSource Damage;

	private void Awake(){
		// a function from video for iframes: https://www.youtube.com/watch?v=YSzmCf_L2cE
		spriteRend = GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
    void Start()
    {
		currentMat = gameObject.GetComponent<Renderer>().material; // used for transparency in iFrames
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

	void SetTransparency(float alphaVal){
		// used for transparency in iFrames
		Color oldColor = currentMat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
		currentMat.SetColor("_Color", newColor);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
		changeImage.NextSprite(damage);
		Damage.Play();

		if (currentHealth > 0){
			//set animation in future here
			StartCoroutine(Invulnerability());
		}
		
		
		if (currentHealth <= 0 && !isDead)
		{
			isDead = true;
			gameObject.SetActive(false);
			gameManager.gameOver(); // to load game over screen
			Destroy(gameObject);
			Debug.Log("YOU DIED");
		}
	}


	private IEnumerator Invulnerability(){
		// 6 = Player Layer, 8 = Enemy Layer
		Physics2D.IgnoreLayerCollision(6, 8, true);
		for (int i = 0; i < numberOfFlashes; i++){ // flash transparency for # of flashes
			// will flash transparent red
			// spriteRend.color = new Color (1, 0, 0, 0.5f);
			SetTransparency(transparentVal);

			yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 4));
			// spriteRend.color = Color.white; // white is used to remove red
			SetTransparency(1); // set to original
			yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 4));
		}
		Physics2D.IgnoreLayerCollision(6, 8, false);

	}
}
