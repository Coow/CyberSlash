using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class DataManager
{
    #region Fields

    /// <summary>
    /// The main path containing everything else
    /// </summary>
    public static string ApplicationPath;

    /// <summary>
    /// The configuration related path
    /// </summary>
    public static string ConfigurationPath;
    
    /// <summary>
    /// The data related path
    /// </summary>
    public static string DataPath;

    /// <summary>
    /// The instance of the serializer
    /// </summary>
    private static JsonSerializer _serializer;

    #endregion

    #region Methods

    static DataManager()
    {
        InitializeSerializer();
    }

    /// <summary>
    /// Utility method to get the full configuration path
    /// </summary>
    /// <returns></returns>
    private static string AssembleConfigurationPathFromDefault()
    {
        return ApplicationPath + "/" + ConfigurationPath + "/";
    }

    /// <summary>
    /// Utility method to get the full data path
    /// </summary>
    /// <returns></returns>
    private static string AssembleDataPathFromDefault()
    {
        return ApplicationPath + "/" + DataPath + "/";
    }

    /// <summary>
    /// Utility method to get a configured serializer
    /// </summary>
    /// <returns></returns>
    private static void InitializeSerializer()
    {
        _serializer = new JsonSerializer();
        _serializer.Formatting = Formatting.Indented;
        _serializer.PreserveReferencesHandling = PreserveReferencesHandling.All;
        _serializer.TypeNameHandling = TypeNameHandling.Auto;
    }

    #endregion
    
    #region Serialize

    /// <summary>
    /// The basic serialize method
    /// </summary>
    /// <param name="file">The file to serialize to</param>
    /// <param name="data">The data to serialize</param>
    /// <param name="defaultConfPath">Do we use the paths from properties or is a path included inside the file name</param>
    /// <param name="isConfiguration">Is this a configuration or data file</param>
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
                _serializer.Serialize(sw, data);
            }
            Debug.Log($"Data saved in : {file}");
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught at " + nameof(SerializeToFile) + " : " + e.Message);
        }
    }

    /// <summary>
    /// This is a convenience serialize method that uses the configuration path as default path
    /// </summary>
    /// <param name="file">The file to serialize to</param>
    /// <param name="data">The data to serialize</param>
    /// <param name="defaultConfPath">Do we use the paths from properties or is a path included inside the file name</param>
    public static void SerializeConfigurationToFile(string file, object data, bool defaultConfPath = true)
    {
        SerializeToFile(file, data, defaultConfPath, true);
    }

    /// <summary>
    /// This is a convenience serialize method that uses the data path as default path
    /// </summary>
    /// <param name="file">The file to serialize to</param>
    /// <param name="data">The data to serialize</param>
    /// <param name="defaultConfPath">Do we use the paths from properties or is a path included inside the file name</param>
    public static void SerializeDataToFile(string file, object data, bool defaultConfPath = true)
    {
        SerializeToFile(file, data, defaultConfPath, false);
    }

    #endregion

    #region Deserialize

    /// <summary>
    /// The basic deserialize method
    /// </summary>
    /// <param name="file">The file to deserialize from</param>
    /// <param name="defaultConfPath">Do we use the paths from properties or is a path included inside the file name</param>
    /// <param name="isConfiguration">Is this a configuration or data file</param>
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
                object result = _serializer.Deserialize(sr, typeof(T));
                Debug.Log($"Data loaded from : {file}");
                return (T)result;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught at " + nameof(DeserializeFromFile) + " : " + e.Message);
        }

        return default(T);
    }


    /// <summary>
    /// This is a convenience deserialize method that uses the configuration path as default path
    /// </summary>
    /// <param name="file">The file to deserialize from</param>
    /// <param name="defaultConfPath">Do we use the paths from properties or is a path included inside the file name</param>
    public static T DeserializeConfigurationFromFile<T>(string file, bool defaultConfPath = true)
    {
        return DeserializeFromFile<T>(file, defaultConfPath, true);
    }

    /// <summary>
    /// This is a convenience deserialize method that uses the data path as default path
    /// </summary>
    /// <param name="file">The file to deserialize from</param>
    /// <param name="defaultConfPath">Do we use the paths from properties or is a path included inside the file name</param>
    public static T DeserializeDataFromFile<T>(string file, bool defaultConfPath = true)
    {
        return DeserializeFromFile<T>(file, defaultConfPath, false);
    }

    #endregion
}
