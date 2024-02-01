using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggessiveWeaponData", menuName = "Data/Weapon Data/ Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData {
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    private void OnEnable() {
        amountOfAttack = attackDetails.Length;

        movementSpeed = new float[amountOfAttack];

        for (int i = 0; i < amountOfAttack; i++) {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
