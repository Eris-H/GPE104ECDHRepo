using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Image healthImg;

    AudioSource audioSource;
    public AudioClip destroyedSound;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = currentHealth/maxHealth;
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        audioSource.PlayOneShot(destroyedSound, 0.7f);


        //test if working
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die(source);
        }
    }

    public void Heal(float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;

        //test if working
        Debug.Log(source.name + " did " + amount + " healing to " + gameObject.name);

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    public void Die(Pawn source)
    {
        Destroy(gameObject);
        if (source.controller != null)
        {
            source.controller.AddToScore(20);
        }
    }
}
