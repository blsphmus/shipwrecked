using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to instantiate
    public float shootInterval = 1f; // Time between shots in seconds
    public int maxHealth = 100; // Maximum player health
    public Image healthBar; // Reference to the UI health bar
    public GameObject gameOverMenu; // Reference to the game over menu UI
    private int currentHealth; // Current player health
    private ScoreManager scoreManager; // Reference to ScoreManager

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Find ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();

        // Start the shooting coroutine
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            GameObject nearestEnemy = FindNearestRectangularEnemy();
            if (nearestEnemy != null)
            {
                ShootAtEnemy(nearestEnemy);
                Debug.Log("Player shooting at enemy at position: " + nearestEnemy.transform.position);
            }
            else
            {
                Debug.Log("No rectangular enemies found to shoot at.");
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }

    GameObject FindNearestRectangularEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ship");
        GameObject nearest = null;
        float minDistance = float.MaxValue;
        Vector2 playerPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(playerPos, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }

    void ShootAtEnemy(GameObject enemy)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (enemy.transform.position - transform.position).normalized;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction, true); // true = пуля от игрока
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void TriggerGameOver()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true); // Show game over menu
            if (scoreManager != null)
            {
                scoreManager.StopScoring(); // Stop accumulating points
                gameOverMenu.GetComponent<GameOverMenu>().SetScore(scoreManager.GetFinalScore());
            }
            Time.timeScale = 0f; // Pause the game
        }
        gameObject.SetActive(false); // Hide player instead of destroying
    }
}