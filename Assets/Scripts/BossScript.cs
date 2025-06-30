using UnityEngine;

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
            Destroy(collision.gameObject); // ? This destroys the projectile
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateColor();

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy weak point when health reaches 0
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
}
