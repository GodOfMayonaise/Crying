using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public void Update() {
        movementUpdate();
        healthUpdate();
    }

    // Movement
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;

    public float getJumpVelocity() {
        return Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight);
    }

    public float getMovementSpeed() {
        return movementSpeed;
    }

    private void movementUpdate() {

    }

    // Health
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthPerSecond;
    private float health;
    private bool isDead = false;

    public float getMaxHealth() {
        return maxHealth;
    }

    public float getHealth() {
        return health;
    }

    public float getHealthPerSecond() {
        return healthPerSecond;
    }

    public void changeHealth(float ammount) {
        health += ammount;

        if(health <= 0) {
            deathEffect();
        }

        if(health >= getMaxHealth()) {
            health = getMaxHealth();
        }
    }
    
    public void deathEffect() {
        isDead = true;
    }

    public bool getIsDead() {
        return isDead;
    }

    public void healthUpdate() {
        health += healthPerSecond * Time.deltaTime;
    }
}
