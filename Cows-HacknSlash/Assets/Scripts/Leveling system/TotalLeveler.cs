using System;
using UnityEngine;

[Serializable]
public class TotalLeveler : ObservableProperties, ILeveler
{
    #region Fields

    [SerializeField]
    private ulong _totalXP;
    [SerializeField]
    private ulong _currentLevelXP;
    [SerializeField]
    private ulong _nextLevelXP;
    [SerializeField]
    private int _currentLevel;

    #endregion

    #region Properties

    /// <summary>
    /// The total accumulated xp
    /// </summary>
    public ulong TotalXP
    {
        get { return _totalXP; }
        protected set
        {
            SetField(ref _totalXP, value);
        }
    }

    /// <summary>
    /// The xp accumulated for the current level
    /// </summary>
    public ulong CurrentLevelXP
    {
        get { return _currentLevelXP; }
        protected set
        {
            SetField(ref _currentLevelXP, value);
        }
    }

    /// <summary>
    /// The current level
    /// </summary>
    public int CurrentLevel
    {
        get { return _currentLevel; }
        protected set
        {
            SetField(ref _currentLevel, value);
        }
    }

    /// <summary>
    /// The xp needed for the next level
    /// </summary>
    public ulong NextLevelXP
    {
        get { return _nextLevelXP; }
        protected set
        {
            SetField(ref _nextLevelXP, value);
        }
    }

    #endregion

    /// <summary>
    /// There is a need to calculate the current level at initialization
    /// </summary>
    public TotalLeveler()
    {
        CalculateCurrentLevelInfo();
    }

    #region Private

    /// <summary>
    /// Update level information based on total accumulated xp
    /// </summary>
    private void CalculateCurrentLevelInfo()
    {
        ulong totalNeeded = 0;
        ulong diff;
        int currentLevel = 1;

        diff = NeededXpForLevel(currentLevel);

        while ((totalNeeded + diff) <= _totalXP)
        {
            currentLevel++;
            totalNeeded += diff;
            diff = NeededXpForLevel(currentLevel);
        }

        CurrentLevel = currentLevel;
        CurrentLevelXP = _totalXP - totalNeeded;
        NextLevelXP = diff;

    }

    /// <summary>
    /// Calculates the xp needed for the next level
    /// </summary>
    /// <param name="level">The current level</param>
    /// <returns></returns>
    private ulong NeededXpForLevel(int level)
    {
        return (ulong)(level + 300 * Mathf.Pow(2, level / 7.0f));
    }

    #endregion

    #region Public

    /// <summary>
    /// Adds an amount of xp
    /// </summary>
    /// <param name="amount">The amoun to add</param>
    public void Add(ulong amount)
    {
        TotalXP += amount;
        CalculateCurrentLevelInfo();
    }

    /// <summary>
    /// Remove an amount of xp
    /// </summary>
    /// <param name="amount">The amount to remove</param>
    public void Remove(ulong amount)
    {
        TotalXP = TotalXP <= amount ? 0 : TotalXP - amount;
        CalculateCurrentLevelInfo();
    }
    
    #endregion
}