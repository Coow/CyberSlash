using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class DataManager
{
    #region Fields

    public static string ApplicationPath;
    public static string DataPath;
    public static string ConfigurationPath;

    #endregion

    #region Methods

    private static string AssembleConfigurationPathFromDefault()
    {
        return ApplicationPath + "/" + ConfigurationPath + "/";
    }

    private static string AssembleDataPathFromDefault()
    {
        return ApplicationPath + "/" + DataPath + "/";
    }

    #endregion
    
    #region Serialize

    public static void SerializeToFile(string file, object data, bool defaultConfPath = true, bool isConfiguration = false)
    {
        try
        {
            if (defaultConfPath)
            {
                var folder = isConfiguration ? AssembleConfigurationPathFromDefault() : AssembleDataPathFromDefault();

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                file = folder + file;
            }

            using (StreamWriter sw = File.CreateText(file))
            {
                JsonSerializer js = new JsonSerializer();
                js.Serialize(sw, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught at " + nameof(SerializeToFile) + " : " + e.Message);
        }
    }

    public static void SerializeConfigurationToFile(string file, object data, bool defaultConfPath = true)
    {
        SerializeToFile(file, data, defaultConfPath, true);
    }

    public static void SerializeDataToFile(string file, object data, bool defaultConfPath = true)
    {
        SerializeToFile(file, data, defaultConfPath, false);
    }

    #endregion

    #region Deserialize

    public static T DeserializeFromFile<T>(string file, bool defaultConfPath = true, bool isConfiguration = false)
    {
        try
        {
            if (defaultConfPath)
            {
                file = (isConfiguration ? AssembleConfigurationPathFromDefault() : AssembleDataPathFromDefault()) + file;
            }

            using (StreamReader sr = File.OpenText(file))
            {
                JsonSerializer js = new JsonSerializer();
                return (T)js.Deserialize(sr, typeof(T));
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught at " + nameof(DeserializeFromFile) + " : " + e.Message);
        }

        return default(T);
    }

    public static T DeserializeConfigurationFromFile<T>(string file, bool defaultConfPath = true)
    {
        return DeserializeFromFile<T>(file, defaultConfPath, true);
    }

    public static T DeserializeDataFromFile<T>(string file, bool defaultConfPath = true)
    {
        return DeserializeFromFile<T>(file, defaultConfPath, false);
    }

    #endregion
}
