using System.ComponentModel;

public interface ILeveler : INotifyPropertyChanged
{
    #region Fields

    /// <summary>
    /// The current level
    /// </summary>
    int CurrentLevel { get; }

    /// <summary>
    /// The xp accumulated for the current level
    /// </summary>
    ulong CurrentLevelXP { get; }
    
    /// <summary>
    /// The xp needed for the next level
    /// </summary>
    ulong NextLevelXP { get; }

    /// <summary>
    /// The total accumulated xp
    /// </summary>
    ulong TotalXP { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Adds an amount of xp
    /// </summary>
    /// <param name="amount"></param>
    void Add(ulong amount);

    /// <summary>
    /// Removes an amount of xp
    /// </summary>
    /// <param name="amount"></param>
    void Remove(ulong amount);
    
    #endregion
}