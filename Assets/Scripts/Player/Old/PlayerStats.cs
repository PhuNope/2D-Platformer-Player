using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject deathChunkParticle, deathBloodParticle;

    private float currenthealth;

    private GameManager GM;

    private void Start() {
        currenthealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DecreaseHealth(float amount) {
        currenthealth -= amount;

        if (currenthealth <= 0.0f) {
            Die();
        }
    }

    private void Die() {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
