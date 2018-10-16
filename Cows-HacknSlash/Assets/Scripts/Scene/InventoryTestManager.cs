using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTestManager : MonoBehaviour {

    //Any folder inside the application path
    public string ConfigurationFolder = "Data/Configuration";
    public string DataFolder = "Data/Data";
    public string ConfigurationSettingsFile = "mainConf.json";
    public ConfigurationSettings Configuration;

    public int InventorySize = 10;
    public InventoryUI Bank;
    public InventoryUI Inventory;

    void Start () {
        InitConfiguration();
        InitTestBank();
        InitTestInventory();
	}

    private void InitConfiguration()
    {
        DataManager.ApplicationPath = Application.dataPath;
        DataManager.ConfigurationPath = ConfigurationFolder;
        DataManager.DataPath = DataFolder;

        SceneConfiguration.Settings = DataManager.DeserializeConfigurationFromFile<ConfigurationSettings>(ConfigurationSettingsFile);
        ResourceManager.Initialize();
    }

    private void InitTestInventory()
    {
        PlayerBag inv = new PlayerBag(InventorySize);
        inv.Add(0, 2);
        Inventory.Initialize(inv);
    }

    private void InitTestBank()
    {
        PlayerBag inv = new PlayerBag(InventorySize);
        inv.Add(0, 1);
        inv.Add(0, 1);
        inv.Add(1, 1);
        inv.Add(0, 5);
        inv.Add(1, 2);

        //inv.Remove(1, 2);
        //inv.Remove(0, 6);
        //Debug.Log(inv.GetAmount(0));
        //Debug.Log(inv.GetAmount(1));

        Bank.Initialize(inv);
    }
}
