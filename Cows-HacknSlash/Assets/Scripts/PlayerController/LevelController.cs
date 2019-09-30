using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour {
	//GameObject displaying XP
	//Attack Level
	public TMP_Text AttackCurrentLevelXP;
	public TMP_Text AttackCurrentLevel;
	[Space(10f)]

	//Spell Level
	public TMP_Text SpellCurrentLevelXP;
	public TMP_Text SpellCurrentLevel;
	[Space(10f)]

	//Defense Level
	public TMP_Text DefenseCurrentLevelXP;
	public TMP_Text DefenseCurrentLevel;
	[Space(10f)]

	//Agility Level
	public TMP_Text AgilityCurrentLevelXP;
	public TMP_Text AgilityCurrentLevel;
	[Space(10f)]

	public ILeveler attackLevel;
	public ILeveler spellLevel;
	public ILeveler defenseLevel;
	public ILeveler agilityLevel;


	void Start () {
		attackLevel = new TotalLeveler();
		attackLevel.PropertyChanged += Leveler_PropertyChanged;

		spellLevel = new TotalLeveler();
		spellLevel.PropertyChanged += Leveler_PropertyChanged;

		defenseLevel = new TotalLeveler();
		defenseLevel.PropertyChanged += Leveler_PropertyChanged;

		agilityLevel = new TotalLeveler();
		agilityLevel.PropertyChanged += Leveler_PropertyChanged;

		UpdateVisuals();
	}
	
	#region Actions

	public void Add(string skill) {
		if(skill == "attackLevel") {
			attackLevel.Add(ulong.Parse("25"));
		}
		if(skill == "spellLevel") {
			spellLevel.Add(ulong.Parse("25"));
		}
		if(skill == "defenseLevel") {
			defenseLevel.Add(ulong.Parse("25"));
		}
		if(skill == "agilityLevel") {
			agilityLevel.Add(ulong.Parse("2500"));
		}
		else {
			Debug.Log(skill + " is not a valid skill!");
		}
	}

	#endregion

    #region Private

    /// <summary>
    /// Reacts to changes with leveler properties
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The event data</param>

	private void Leveler_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateVisuals();
    }

    /// <summary>
    /// Properly updates the display of the leveler
    /// </summary>
    private void UpdateVisuals()
    {
        UpdateTotalXP();
        UpdateCurrentLevelXP();
        UpdateCurrentLevel();
        UpdateNextLevelXP();
    }

    /// <summary>
    /// Properly updates the display of total xp
    /// </summary>
    private void UpdateTotalXP()
    {
        //TotalXP.text = $"Total XP : {leveler.TotalXP}";
    }

    /// <summary>
    /// Properly updates the display of current level xp
    /// </summary>
    private void UpdateCurrentLevelXP()
    {
        AttackCurrentLevelXP.text = $"Current XP : {attackLevel.CurrentLevelXP}";
		DefenseCurrentLevelXP.text = $"Current XP : {defenseLevel.CurrentLevelXP}";
		AgilityCurrentLevelXP.text = $"Current XP : {agilityLevel.CurrentLevelXP}";

    }

    /// <summary>
    /// Properly updates the display of current level
    /// </summary>
    private void UpdateCurrentLevel()
    {
        AttackCurrentLevel.text = $"{attackLevel.CurrentLevel}";
		DefenseCurrentLevel.text = $"{defenseLevel.CurrentLevel}";
		AgilityCurrentLevel.text = $"{agilityLevel.CurrentLevel}";

		this.GetComponent<CharController>().CalculatePlayerSpeed(AgilityCurrentLevel.text);
		this.GetComponent<CharController>().CalculatePlayerMaxHealth(DefenseCurrentLevel.text);
    }

    /// <summary>
    /// Properly updates the display of nex level xp
    /// </summary>
    private void UpdateNextLevelXP()
    {
        //NextLevelXP.text = $"Next level XP : {leveler.NextLevelXP}";
    }
    
    #endregion
	
	
}
