using UnityEngine;

namespace StarProject
{
    public class Constants
    {
        public const string LoginPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        
        public const int MinPasswordLength = 6;
    }
}
