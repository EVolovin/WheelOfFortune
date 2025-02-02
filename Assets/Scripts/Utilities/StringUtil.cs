using System;
using System.Globalization;

namespace WheelOfFortune.Utilities
{
    public static class StringUtil
    {
        public const string SCENE_MAIN_NAME  = "MainScene";
        public const string UI_CONTAINER_TAG = "UIContainer";
        public const string USER_DATA_FILE_NAME = "UserData.dat";
        
        
        // extension method to remove the (Clone) label in the name of a freshly spawned gameObject
        public static string RemoveCloneLabel(this string str)
        {
            return str.Replace("(Clone)", string.Empty);
        }
        
        public static string GetFormattedNumber(double value, bool shortenValue = true)
        {
            if (!shortenValue)
                return value.ToString("N0", CultureInfo.InvariantCulture);
            
            return value switch
            {
                > 999999999 or < -999999999 => 
                    value.ToString("0,,,.###B", CultureInfo.InvariantCulture),
                > 999999 or < -999999 => 
                    value.ToString("0,,.##M", CultureInfo.InvariantCulture),
                > 999 or < -999 => 
                    value.ToString("0,.#K", CultureInfo.InvariantCulture),
                _ => value.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
