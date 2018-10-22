using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelingTestManager : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The GameObject containing the amount of xp to add/remove
    /// </summary>
    public TMP_InputField XPAmount;

    /// <summary>
    /// The GameObject containing the percent of xp to add/remove
    /// </summary>
    public TMP_InputField PercentAmount;

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
    /// The GameObject displaying the levelers available
    /// </summary>
    public Dropdown Choices;

    /// <summary>
    /// The leveler class to test
    /// </summary>
    public ILeveler Leveler;

    /// <summary>
    /// The list of all levelers detected
    /// </summary>
    public IEnumerable<Type> Levelers;

    #endregion

    private void Start()
    {
        Levelers = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ILeveler).IsAssignableFrom(t) && t != typeof(ILeveler));

        Choices.AddOptions(Levelers.Select(l => l.Name).ToList());
        LevelerChoiceChanged();
    }

    #region Actions

    /// <summary>
    /// Action for the add xp button
    /// </summary>
    public void Add()
    {
        Leveler.Add(ulong.Parse(XPAmount.text));
    }

    /// <summary>
    /// Action for the remove xp button
    /// </summary>
    public void Remove()
    {
        Leveler.Remove(ulong.Parse(XPAmount.text));
    }

    /// <summary>
    /// Action for the add percentage button
    /// Percentage [0, 100]
    /// </summary>
    public void AddPercent()
    {
        Leveler.Add((ulong)(Leveler.NextLevelXP * float.Parse(PercentAmount.text) / 100.0f));
    }

    /// <summary>
    /// Action for the remove percentage button
    /// Percentage [0, 100]
    /// </summary>
    public void RemovePercent()
    {
        Leveler.Remove((ulong)(Leveler.NextLevelXP * float.Parse(PercentAmount.text) / 100.0f));
    }

    /// <summary>
    /// Action for changing the leveler
    /// </summary>
    public void LevelerChoiceChanged()
    {
        var chosen = Choices.options[Choices.value].text;
        var type = Levelers.First(l => l.Name == chosen);

        Set((ILeveler)Activator.CreateInstance(type));
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
    /// Properly sets up the leveler
    /// </summary>
    /// <param name="leveler">The leveler to set</param>
    private void Set(ILeveler leveler)
    {
        Debug.Log("Leveler chosen");
        if (Leveler != null)
        {
            Leveler.PropertyChanged -= Leveler_PropertyChanged;
        }

        Leveler = leveler;
        Leveler.PropertyChanged += Leveler_PropertyChanged;
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
        TotalXP.text = $"Total XP : {Leveler.TotalXP}";
    }

    /// <summary>
    /// Properly updates the display of current level xp
    /// </summary>
    private void UpdateCurrentLevelXP()
    {
        CurrentLevelXP.text = $"Current level XP : {Leveler.CurrentLevelXP}";
    }

    /// <summary>
    /// Properly updates the display of current level
    /// </summary>
    private void UpdateCurrentLevel()
    {
        CurrentLevel.text = $"Current level : {Leveler.CurrentLevel}";
    }

    /// <summary>
    /// Properly updates the display of nex level xp
    /// </summary>
    private void UpdateNextLevelXP()
    {
        NextLevelXP.text = $"Next level XP : {Leveler.NextLevelXP}";
    }
    
    #endregion
}