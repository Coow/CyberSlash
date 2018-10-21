using System;
using UnityEngine;

[Serializable]
public class Leveler : ObservableProperties, ILeveler
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
    /// Since the default value for ulong is 0 
    /// we set it to 1 in the constructor so we begin at level 1
    /// </summary>
    public Leveler()
    {
        _currentLevel = 1;
        UpdateLevelInfo();
    }

    #region Private

    /// <summary>
    /// Properly levels up
    /// </summary>
    private void LevelUp()
    {
        CurrentLevel++;
        UpdateLevelInfo();
    }

    /// <summary>
    /// Properly levels down
    /// </summary>
    private void LevelDown()
    {
        CurrentLevel--;
        UpdateLevelInfo(false);
    }

    /// <summary>
    /// Updates the current level informations based on if we're leveling up or down
    /// </summary>
    /// <param name="up">Are we leveling up</param>
    private void UpdateLevelInfo(bool up = true)
    {
        NextLevelXP = NeededXpForLevel(_currentLevel);
        CurrentLevelXP = up ? 0 : _nextLevelXP;
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
        ulong xpToNextLevel;
        ulong addXp;

        //Add xp per level so we can level up accordingly
        while (amount > 0)
        {
            xpToNextLevel = _nextLevelXP - _currentLevelXP;
            addXp = xpToNextLevel >= amount ? amount : xpToNextLevel;

            CurrentLevelXP += addXp;
            TotalXP += addXp;
            amount -= addXp;

            if (_currentLevelXP == _nextLevelXP)
            {
                LevelUp();
            }
        }
    }

    /// <summary>
    /// Remove an amount of xp
    /// </summary>
    /// <param name="amount">The amount to remove</param>
    public void Remove(ulong amount)
    {
        ulong delXp;

        //Remove xp per level so we can level down accordingly
        while (amount > 0)
        {
            delXp = _currentLevelXP >= amount ? amount : _currentLevelXP;
            CurrentLevelXP -= delXp;
            amount -= delXp;
            TotalXP -= delXp;

            if (_currentLevelXP == 0 && amount > 0)
            {
                LevelDown();
            }
        }
    }
    
    #endregion
}