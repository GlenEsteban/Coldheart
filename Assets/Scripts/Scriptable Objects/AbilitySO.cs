using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilitySO : ScriptableObject {
    [SerializeField] private string abilityName = "New Ability";
    [SerializeField] private float cooldown;

    public string AbilityName => abilityName;
    public float Cooldown => cooldown;
}
