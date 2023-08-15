using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 0f; // Health represents the current health of the player displayed by the health bar
	[SerializeField] private int lives = 5;  // Lives represent the Hearts we have remaining.
    [SerializeField] private float maxHealth = 40f;
	[SerializeField] private int maxLives = 5;
	[SerializeField] private PlayerMovement player;
	[SerializeField] private Slider healthBar;
	[SerializeField] public bool trackHealth;
	[SerializeField] private AudioManager audioManager;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		health = maxHealth; 
		lives = maxLives;
		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		trackHealth = true;
        StartCoroutine(RegenerateHealth());
	}

    private void FixedUpdate()
    {
		healthBar.value = (health / maxHealth);
    }

    public void UpdateHealth (float mod)
	{
		if (health > 0 && trackHealth)
		{
			audioManager.PlayAt(2);
			health += mod;

			if (health > maxHealth)
			{
				health = maxHealth;
			}
			else if (health <= 0f)
			{
				health = 0f;
				lives -= 1;
				player.PlayerDies();
				Debug.Log("Player died");
			}

			if (lives > maxLives)
			{
				lives = maxLives;
			}
			else if (lives <= 0)
			{
				Debug.Log("Player lost all lives");
			}
		}
	}

	public float GetHealth() {
		return health;
	}

	public void ResetHealth()
	{
		health = maxHealth;
	}

	public void StopTrackingHealth()
    {
		trackHealth = false;
    }

    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (health < maxHealth)
            {
                health += 2.5f;
            }
            yield return new WaitForSeconds(20);
        }
    }
}
