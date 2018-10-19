using UnityEngine;

public class InventoryTestManager : MonoBehaviour
{
    #region Fields

    //Any folder inside the application path
    public string ConfigurationFolder = "Data/Configuration";
    public string DataFolder = "Data/Data";
    public string ConfigurationSettingsFile = "mainConf.json";
    public ConfigurationSettings Configuration;

    public int InventorySize = 12;
    public InventoryUI Bank;
    public InventoryUI Inventory;
    public InventoryUI Equipment;

    #endregion

    #region Private

    private void Start () {
        InitConfiguration();

        InitTestBank();
        InitTestInventory();
        InitTestEquipment();
    }

    /// <summary>
    /// Gets the configuration of the scene
    /// </summary>
    private void InitConfiguration()
    {
        //Set up the data manager
        DataManager.ApplicationPath = Application.dataPath;
        DataManager.ConfigurationPath = ConfigurationFolder;
        DataManager.DataPath = DataFolder;

        //Get the settings
        SceneConfiguration.Settings = DataManager.DeserializeConfigurationFromFile<ConfigurationSettings>(ConfigurationSettingsFile);
        
        //Initialize the resources
        ResourceManager.Initialize();
    }

    /// <summary>
    /// Initialize the bank inventory for the test scene
    /// </summary>
    private void InitTestBank()
    {
        PlayerBag inv = new PlayerBag(InventorySize);

        //Do some basic operations to check for errors
        inv.Add(0, 2);
        inv.Add(0, 1);
        inv.Add(1, 1);
        inv.Add(0, 5);
        inv.Add(1, 2);

        inv.Remove(1, 2);
        inv.Remove(0, 6);

        inv.Add(2, 1);
        inv.Add(3, 1);
        inv.Add(4, 1);
        inv.Add(5, 1);
        inv.Add(6, 1);
        inv.Add(7, 1);

        //Display some basic info to check for errors
        Debug.Log($"Amount Apple : { inv.GetAmount(0)}");
        Debug.Log($"Amount Bread : { inv.GetAmount(1)}");
        Debug.Log($"Capacity Apple : { inv.GetFreeSpace(0)}");
        Debug.Log($"Capacity Bread : { inv.GetFreeSpace(1)}");

        Bank.Initialize(inv);
    }

    /// <summary>
    /// Initialize the player inventory for the test scene
    /// </summary>
    private void InitTestInventory()
    {
        PlayerBag inv = new PlayerBag(InventorySize);

        inv.Add(0, 2);
        inv.Add(4, 1);

        Inventory.Initialize(inv);
    }

    /// <summary>
    /// Initialize the equipment inventory for the test scene
    /// </summary>
    private void InitTestEquipment()
    {
        var eq = new PlayerEquipment();

        Equipment.Initialize(eq);
    }

    #endregion
}
