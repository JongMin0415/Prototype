using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public int maxLives = 3;
    private int currentLives;

    public float invincibleTime = 1f;
    private bool isInvincible = false;
    public float blinkInterval = 0.1f;

    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;

    private int playerLayer;
    private int enemyBulletLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentLives = maxLives;

        playerLayer = LayerMask.NameToLayer("Player");
        enemyBulletLayer = LayerMask.NameToLayer("EnemyBullet");
    }

    void Update()
    {
        if (isDashing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("MoveX", movement.x);
        anim.SetFloat("MoveY", movement.y);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("LastMoveX", movement.x);
            anim.SetFloat("LastMoveY", movement.y);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDashing)
                StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        rb.velocity = movement.normalized * speed;
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 dir = (mousePos - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentLives -= damage;
        Debug.Log("Player Lives: " + currentLives);

        if (currentLives <= 0)
        {
            Die();
        }

        StartCoroutine(Invincible());
    }

    IEnumerator Invincible()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < invincibleTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        sr.enabled = true;
        isInvincible = false;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        isInvincible = true;

        Physics2D.IgnoreLayerCollision(playerLayer, enemyBulletLayer, true);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 dashDir = (mousePos - transform.position).normalized;

        float timer = 0f;

        while (timer < dashDuration)
        {
            rb.velocity = dashDir * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;

        Physics2D.IgnoreLayerCollision(playerLayer, enemyBulletLayer, false);

        isDashing = false;
        isInvincible = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}