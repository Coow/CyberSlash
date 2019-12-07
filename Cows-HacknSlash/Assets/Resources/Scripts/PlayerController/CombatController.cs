using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject selectedTarget;
    public GameObject[] avaliableTargets;

    public Weapon selectedWeapon;
    [SerializeField]private WeaponEnum selectedWeaponType;

	private SpellController spellController;
	private MeleeController meleeController;

    [SerializeField] private GameObject LeftHandAttachmentPoint, RightHandAttachmentPoint,EquipedWeapon;

	//TODO Im on a plane and I know there is some function of observing when a property changes, but I cant be bothered RN
	[SerializeField]private bool ChangedWeapon = false;

	[SerializeField]private int targetIndex;

    void Start() {
		spellController = gameObject.GetComponent<SpellController>();
		meleeController = gameObject.GetComponent<MeleeController>();
	}

    void Update()
    {	
		//DEBUG
		if(Input.GetKeyDown(KeyCode.C)){
			ChangedWeapon = true;
		}

		if(ChangedWeapon){
			ChangeWeaponStats();
		}
        if (Input.GetMouseButtonDown(0))
        {
            SelectTarget();
        }
    }

	void FixedUpdate() {
		//GetAvaliabeTargets();
	}

	public void GetAvaliabeTargets() {

	}

    public void SelectTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.tag == "Enemy")
            {
                selectedTarget = hit.transform.gameObject;
            }
        }
    }
	
	private void ChangeWeaponStats() {
		selectedWeaponType = selectedWeapon.weaponType;
		ChangedWeapon = false;
		if(selectedWeaponType == WeaponEnum.Melee){
			Debug.Log("Player selected a weapon type: <b>Melee</b>");
			meleeController.damageToDeal = selectedWeapon.damageAmount;
			meleeController.damageType = selectedWeapon.damageType;
			meleeController.attackRange = selectedWeapon.attackRange;

			InstantiateWeapon();
			
			//Enable Controllers depending on which type of weapon is selected
			meleeController._Enabled = true;
			spellController._Enabled = false;
		}
		if(selectedWeaponType == WeaponEnum.Staff)  {
			Debug.Log("Player selected a weapon type: <b>Spell</b>");
			spellController.damageToDeal = selectedWeapon.damageAmount;
			spellController.damageType = selectedWeapon.damageType;
			
			InstantiateWeapon();

			//Enable Controllers depending on which type of weapon is selected
			meleeController._Enabled = false;
			spellController._Enabled = true;
		}
		if(selectedWeaponType == WeaponEnum.None) {
			meleeController._Enabled = false;
			spellController._Enabled = false;
			
			Destroy(EquipedWeapon);
		}
	}

	private void InstantiateWeapon(){
		//Remove equiped weapon
		Destroy(EquipedWeapon);

		if(selectedWeapon.RightHand){
			//Equip to right hand
			EquipedWeapon = Instantiate(selectedWeapon.weaponObject,RightHandAttachmentPoint.transform.position,Quaternion.identity);
			EquipedWeapon.transform.SetParent(RightHandAttachmentPoint.transform);
			EquipedWeapon.transform.localEulerAngles = selectedWeapon.RightHandPrefabQuaternion;
			Debug.Log("Player equiped weapon: <b>" + selectedWeapon.Name + "</b> to right hand.");
			return;
		} else {
			//Equip to left hand
			EquipedWeapon = Instantiate(selectedWeapon.weaponObject,LeftHandAttachmentPoint.transform.position,Quaternion.identity);
			EquipedWeapon.transform.SetParent(LeftHandAttachmentPoint.transform);
			EquipedWeapon.transform.localEulerAngles = selectedWeapon.LeftHandPrefabQuaternion;
			Debug.Log("Player equiped weapon: <b>" + selectedWeapon.Name + "</b> to left hand.");
			return;
		}
		
	}
}
