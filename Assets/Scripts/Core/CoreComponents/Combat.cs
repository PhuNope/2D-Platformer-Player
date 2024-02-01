using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, Iknockbackable {

    [SerializeField] private GameObject damageParticles;

    protected Movement Movement { get => movement ??= core.GetComponent<Movement>(); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetComponent<CollisionSenses>(); }
    private CollisionSenses collisionSenses;

    protected Stats Stats { get => stats ??= core.GetComponent<Stats>(); }
    private Stats stats;

    private ParticleManager ParticleManager => particleManager ??= core.GetComponent<ParticleManager>();
    private ParticleManager particleManager;

    [SerializeField] private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void LogicUpdate() {
        CheckKnockback();
    }

    public void Damage(float amount) {
        Stats?.DecreaseHealth(amount);
        ParticleManager?.StartParticleWithRandomRotation(damageParticles, core.transform.parent.position);
    }

    public void Knockback(Vector2 angle, float strength, int direction) {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback() {
        if (isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground) || Time.time >= knockbackStartTime + maxKnockbackTime)) {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
