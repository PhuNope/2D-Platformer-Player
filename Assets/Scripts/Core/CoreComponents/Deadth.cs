using UnityEngine;

public class Deadth : CoreComponent {

    [SerializeField] private GameObject[] deadParticles;

    protected ParticleManager ParticleManager => particleManager ??= core.GetComponentInChildren<ParticleManager>();
    private ParticleManager particleManager;

    protected Stats Stats => stats ??= core.GetComponent<Stats>();
    private Stats stats;

    public void Die() {
        foreach (var particle in deadParticles) {
            ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable() {
        Stats.OnHealthZero += Die;
    }

    private void OnDisable() {
        Stats.OnHealthZero -= Die;
    }
}
