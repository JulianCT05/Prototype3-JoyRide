using UnityEngine;
using UnityEngine.UI; // Needed for displaying UI

public class WeakPoint : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Color Thresholds")]
    public Color defaultColor = Color.red;
    public Color midHealthColor = new Color(1f, 0.5f, 0f); // Orange
    public Color lowHealthColor = Color.yellow;

    private SpriteRenderer sr;

    // Static tracking variables
    private static int totalWeakPoints = 0;
    private static int destroyedWeakPoints = 0;

    void Awake()
    {
        // Count this weak point on creation
        totalWeakPoints++;
    }

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = defaultColor;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(15);
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateColor();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            destroyedWeakPoints++;

            if (destroyedWeakPoints >= totalWeakPoints)
            {
                YouWin();
            }
        }
    }

    void UpdateColor()
    {
        if (sr != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;

            if (healthPercent <= 0.25f)
                sr.color = lowHealthColor;
            else if (healthPercent <= 0.5f)
                sr.color = midHealthColor;
            else
                sr.color = defaultColor;
        }
    }

    void YouWin()
    {
        Debug.Log("You Win!");

        // Optional: Show a message on screen
        // You can also load a win screen, pause game, etc.
        // Time.timeScale = 0f; // freeze game if desired
    }

    void OnDestroy()
    {
        // Clean up in case the object is manually removed
        totalWeakPoints = Mathf.Max(totalWeakPoints - 1, 0);
    }
}
