﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{   
    Weapon _weapon;
    Editor _gameObjectEditor;
    bool _meleeSelected, _staffSelected = false;

    public override void OnInspectorGUI(){

        serializedObject.Update();

        Weapon _weapon = (Weapon)target; 

        #region Setup

        SerializedProperty _Name = serializedObject.FindProperty("Name");
        SerializedProperty _Description = serializedObject.FindProperty("Description");
        SerializedProperty _weaponType = serializedObject.FindProperty("weaponType");

        SerializedProperty _weaponSpells = serializedObject.FindProperty("spells");
        SerializedProperty _weaponAttacks = serializedObject.FindProperty("attacks");

        SerializedProperty _TwoHanded = serializedObject.FindProperty("TwoHanded");
        SerializedProperty _RightHand = serializedObject.FindProperty("RightHand");
        SerializedProperty _damageType = serializedObject.FindProperty("damageType");
        SerializedProperty _itemRareness = serializedObject.FindProperty("itemRareness");
        SerializedProperty _damageAmount = serializedObject.FindProperty("damageAmount");
        SerializedProperty _durabilityPercentage = serializedObject.FindProperty("durabilityPercentage");
        SerializedProperty _currentDurabilityPercentage = serializedObject.FindProperty("currentDurabilityPercentage");
        SerializedProperty _weaponObject = serializedObject.FindProperty("weaponObject");

        SerializedProperty _inventoryItem = serializedObject.FindProperty("inventoryItem");

        #endregion

        serializedObject.Update();

        GUILayout.Label("Weapon", EditorStyles.boldLabel);

        _weapon.Name = EditorGUILayout.TextField("Name: ", _weapon.Name);
        _weapon.Description = EditorGUILayout.TextField("Description: ", _weapon.Description);

        GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type of Weapon: ");
        _weapon.weaponType = (WeaponEnum)EditorGUILayout.EnumPopup(_weapon.weaponType);
        EditorGUILayout.EndHorizontal();

        /// <summary>
        /// Stuff for hiding/showing Dropdown for Arrays for Spells and Attacks avaliabe to the Weapon
        /// This needs to be inbeetween the stuff it is right now, to make sure it looks nice
        /// </summary>
        #region ShowLists
        if(_weapon.weaponType == WeaponEnum.Staff){
            _staffSelected = true;
            _meleeSelected = false;
            //Debug.Log("<b>GUI:</b> Staff selected");
        }

        if(_weapon.weaponType == WeaponEnum.Melee){
            _staffSelected = false;
            _meleeSelected = true;
            //Debug.Log("<b>GUI:</b> Melee selected");
        }

        if(_weapon.weaponType == WeaponEnum.None){
            _staffSelected = false;
            _meleeSelected = false;
            //Debug.Log("<b>GUI:</b> Nothing selected");
        }

        //_staffSelected = GUILayout.Toggle(_staffSelected, "Spells");
        if(_staffSelected){
            EditorGUILayout.PropertyField(_weaponSpells, true);
        }

        //_meleeSelected = GUILayout.Toggle(_meleeSelected, "Attacks");
        if(_meleeSelected){
            EditorGUILayout.PropertyField(_weaponAttacks, true);
        }

        #endregion
        
        GUILayout.Space(10f);

        _weapon.RightHand = GUILayout.Toggle(_weapon.RightHand, "Right Handed Weapon");
        if(_weapon.RightHand) {
            _weapon.RightHandPrefabQuaternion = EditorGUILayout.Vector3Field("Right Hand Quaternion:",_weapon.RightHandPrefabQuaternion);
        } else {
            _weapon.LeftHandPrefabQuaternion = EditorGUILayout.Vector3Field("Left Hand Quaternion:",_weapon.LeftHandPrefabQuaternion);
        }

        GUILayout.Space(4f);

        _weapon.damageType = (DamageEnum)EditorGUILayout.EnumPopup("Damage Type:",_weapon.damageType);
        _weapon.itemRareness = (ItemRareness)EditorGUILayout.EnumPopup("Item Rareness:",_weapon.itemRareness);
        _weapon.damageAmount = EditorGUILayout.IntField("Damage Amount:",_weapon.damageAmount);

        _weapon.currentDurabilityPercentage = EditorGUILayout.IntSlider("Durability Left:",_weapon.currentDurabilityPercentage, 0, 100);
        // TODO Make a Custom GameObject preview for the Weapon Object
        // https://answers.unity.com/questions/448086/is-it-possible-to-create-a-custom-model-preview-in.html
        
        GUILayout.Space(4f);

        _weapon.weaponObject = (GameObject)EditorGUILayout.ObjectField("Weapon Prefab:",_weapon.weaponObject, typeof(GameObject), true);

        GUILayout.Space(4f);

        _weapon.inventoryItem = (Item)EditorGUILayout.ObjectField("Inventory Item:",_weapon.inventoryItem, typeof(Item), true);

    }
}
