using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caravan : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float coolDown = 1f;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite damageSprite, heavyDamageSprite;
    [SerializeField]
    private ParticleSystem dustParticles;
    [SerializeField]
    private Slider healthSlider;

    public float health;
    float timer;
    Vector3 collisionPoint;
    bool hittable;

    private float impulseForce, maxImpulse;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > coolDown)
        {
            hittable = true;
            timer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (hittable && collision.gameObject.tag != "Player")
        {
            CalculateImpulse(collision);
            hittable = false;
        }



    }

    void CalculateImpulse(Collision2D collision)
    {
        impulseForce = 0f;
        maxImpulse = 0f;

        for (int i = 0; i < collision.contactCount; i++)
        {
            impulseForce = Vector3.Dot(collision.GetContact(i).point - (Vector2)collision.gameObject.transform.position, collision.GetContact(i).relativeVelocity);

            if (impulseForce > maxImpulse) maxImpulse = impulseForce;
        }

        Debug.Log(maxImpulse);

        health -= maxImpulse;

        dustParticles.Play();

        healthSlider.value = health / (float)maxHealth;

        if(health < 0f)
        {
            GameManager.EndGame();
        }

        else if (health < maxHealth / 5f)
        {
            spriteRenderer.sprite = heavyDamageSprite;
        }
        else if (health < maxHealth / 2f)
        {
            spriteRenderer.sprite = damageSprite;
        }


    }
}
