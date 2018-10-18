using UnityEngine;
using TMPro;

public class LevelingTestManager : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The GameObject containing the amount of xp to add/remove
    /// </summary>
    public TMP_InputField XPAmount;
    
    /// <summary>
    /// The GameObject displaying the total xp
    /// </summary>
    public TMP_Text TotalXP;

    /// <summary>
    /// The GameObject displaying the current level xp
    /// </summary>
    public TMP_Text CurrentLevelXP;

    /// <summary>
    /// The GameObject displaying the current level
    /// </summary>
    public TMP_Text CurrentLevel;

    /// <summary>
    /// The GameObject displaying the next level xp
    /// </summary>
    public TMP_Text NextLevelXP;

    /// <summary>
    /// The leveler class to test
    /// </summary>
    public Leveler leveler;

    #endregion

    private void Start()
    {
        leveler = new Leveler();
        leveler.PropertyChanged += Leveler_PropertyChanged;
        UpdateVisuals();
    }

    #region Actions

    /// <summary>
    /// Action for the add xp button
    /// </summary>
    public void Add()
    {
        leveler.Add(ulong.Parse(XPAmount.text));
    }

    /// <summary>
    /// Action for the remove xp button
    /// </summary>
    public void Remove()
    {
        leveler.Remove(ulong.Parse(XPAmount.text));
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
        TotalXP.text = $"Total XP : {leveler.TotalXP}";
    }

    /// <summary>
    /// Properly updates the display of current level xp
    /// </summary>
    private void UpdateCurrentLevelXP()
    {
        CurrentLevelXP.text = $"Current level XP : {leveler.CurrentLevelXP}";
    }

    /// <summary>
    /// Properly updates the display of current level
    /// </summary>
    private void UpdateCurrentLevel()
    {
        CurrentLevel.text = $"Current level : {leveler.CurrentLevel}";
    }

    /// <summary>
    /// Properly updates the display of nex level xp
    /// </summary>
    private void UpdateNextLevelXP()
    {
        NextLevelXP.text = $"Next level XP : {leveler.NextLevelXP}";
    }
    
    #endregion
}