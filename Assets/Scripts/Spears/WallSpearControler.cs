using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpearController : MonoBehaviour
{
    private SpearType spearType;
    private float lifeTime;
    private Rigidbody2D rb;
    private Player player;
    private SpriteRenderer sr;
    private Animator anim;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetUpWallSpear(SpearType spearType, Sprite sprite, float lifeTime, Player player)
    {
        this.spearType = spearType;
        this.lifeTime = lifeTime;
        this.player = player;

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.enabled = false;
        sr.sprite = sprite;

        if (spearType == SpearType.RegularLightBlue)
        {
            sr.color = new Color(1, 1, 1, 0.5f); 
        }
    }

    private void Start()
    {
        rb.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
        transform.right = rb.linearVelocity.normalized;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            TriggerDestroyAnimation();
        }
    }

    public void TriggerDestroyAnimation()
    {
        if (anim != null)
        {
            anim.enabled = true;
            anim.Play(spearType == SpearType.Regular ? "RegularSpearAnim" : "BlueSpearAnim");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (spearType == SpearType.RegularLightBlue)
        {
            HandleLightBlueSpearCollision(collision);
        }
        else
        {
            Player player = collision.GetComponent<Player>() ?? collision.GetComponentInParent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
    }

    private void HandleLightBlueSpearCollision(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity.magnitude > Mathf.Epsilon)
        {
            collision.GetComponent<Player>()?.Damage();
        }
    }

    public void ZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}

