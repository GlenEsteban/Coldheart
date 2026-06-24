using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon")]
public class CharacterSO : ScriptableObject {
    public string characterName;
    public Sprite sprite;
    public Animator animator;
    public int statStrength;
    public int statIntelligence;
    public int statDefense;
    public int statAgility;
    public int statVitality;
    public int statEndurance;
}