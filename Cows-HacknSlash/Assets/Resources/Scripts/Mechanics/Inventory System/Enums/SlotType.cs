using Newtonsoft.Json;
using System;

/// <summary>
/// Used to match item types to slot types
/// </summary>
[Flags]
//This is necessary for easier conversion of the value from and to json
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum SlotType
{
    None = 0,
    Standard = 1 << 0,
    Helmet = 1 << 1,
    Chestplate = 1 << 2,
    Ring = 1 << 3,
    Leggings = 1 << 4,
    Boots = 1 << 5,
    Weapon = 1 << 6,
    Skill = 1 << 7,

    All = ~None,
    NonSkills = All ^ Skill
}