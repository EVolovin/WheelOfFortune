using System;
using System.IO;
using UnityEngine;
using WheelOfFortune.Game;

namespace WheelOfFortune.Utilities
{
    public static class SaveDataManager
    {
        public static void SaveJsonData(ISaveable obj)
        {
            var json = obj.ToJson();
            
            if (!WriteToFile(StringUtil.USER_DATA_FILE_NAME, json))
                Debug.LogError("Failed to save user data!");
        }
        
        public static bool LoadJsonData(ISaveable obj)
        {
            if (!ReadFromFile(StringUtil.USER_DATA_FILE_NAME, out var jsonString))
            {
                Debug.LogError("Failed to load user data!");
                return false;
            }
            
            obj.LoadFromJson(jsonString);
            return true;
        }
        
        static bool WriteToFile(string fileName, string jsonString)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
            
            try
            {
                File.WriteAllText(fullPath, jsonString);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {fullPath} with exception {e}");
                return false;
            }
        }
        
        static bool ReadFromFile(string fileName, out string jsonString)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                jsonString = File.ReadAllText(fullPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {fullPath} with exception {e}");
                jsonString = string.Empty;
                return false;
            }
        }
    }
}
