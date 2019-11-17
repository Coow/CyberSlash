using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "CyberSlash/Weapon")]
public class Weapon : ScriptableObject
{   
    public string Name,Description;
    public WeaponEnum weaponType;
    public Spell[] spells;
    public Attacks[] attacks;
    public bool RightHand = true;
    public DamageEnum damageType;
    public ItemRareness itemRareness;
    public int damageAmount;
    public int durabilityPercentage, currentDurabilityPercentage;
    public float attackRange, attackSpeed;
    public GameObject weaponObject;

    public Item inventoryItem;
    
    public Vector3 RightHandPrefabQuaternion;
    public Vector3 LeftHandPrefabQuaternion;
    
    void Awake()
    {
        
        if (weaponType == WeaponEnum.None){
            Debug.LogError("Please select a Weapon Type for this weapon");
        }

    }
}
