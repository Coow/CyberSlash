using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Contains all the configuration a scene needs
/// </summary>
public static class SceneConfiguration
{
    public static BaseSettings Base = new BaseSettings();

    /// <summary>
    /// Main configuration
    /// </summary>
    public static ConfigurationSettings Settings;

    static SceneConfiguration()
    {
        //Set up the data manager
        DataManager.ApplicationPath = Application.dataPath;
        DataManager.ConfigurationPath = Base.ConfigurationFolder;
        DataManager.DataPath = Base.DataFolder;

        //Get the settings
        Settings = DataManager.DeserializeConfigurationFromFile<ConfigurationSettings>(Base.ConfigurationSettingsFile);

        var data = SceneDataManager.instance;

    }
}
