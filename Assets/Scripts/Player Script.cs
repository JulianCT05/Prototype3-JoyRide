using UnityEngine;

public class PlayerFlyAndShoot : MonoBehaviour
{
    [Header("Flight Settings")]
    public float flySpeed = 5f;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 0.5f;
    public float projectileSpeed = 10f;

    private Rigidbody2D rb;
    private float shootTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Fly up when holding space
        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, flySpeed);
        }

        // Constant shooting
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootProjectile();
            shootTimer = 0f;
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.linearVelocity = Vector2.right * projectileSpeed;
            }
        }
    }
}
