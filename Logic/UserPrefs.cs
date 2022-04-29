using UnityEngine;
namespace App.Settings.Prefrences
{
    /// <summary>
    /// This manages all PlayerPrefs
    /// Saves and gets playerprefs 
    /// </summary>
    public class UserPrefs
    {
        #region "Canvas prefs"
        /// <summary>
        /// Sets the submit canvas player pref to the passed value
        /// </summary>
        /// <param name="value">value to set</param>
        public static void SetPreferredCanvas(string value)
        {
            PlayerPrefs.SetString("Submit Canvas", value);
        }
        /// <summary>
        /// gets the submit canvas player pref and returns it
        /// </summary>
        /// <returns></returns>
        public static string GetPreferredCanvas()
        {
            return PlayerPrefs.GetString("Submit Canvas");
        }
        #endregion
        #region "login prefs"
        /// <summary>
        /// sets the Loggedin pref to the passed value
        /// </summary>
        /// <param name="value"></param>
        public static void SetLoginSuccessful(string value)
        {
            PlayerPrefs.SetString("Loggedin", value);
        }
        /// <summary>
        /// returns the "Loggedin" player pref string
        /// </summary>
        /// <returns></returns>
        public static string GetLoginSuccessful()
        {
            return PlayerPrefs.GetString("Loggedin");
        }
        /// <summary>
        /// sets the "Completed Log in" pref to the passed value
        /// </summary>
        /// <param name="value"></param>
        public static void SetLoginComplete(string value)
        {
            PlayerPrefs.SetString("Completed Log in", value);
        }
        /// <summary>
        /// returns the "Completed Log in" player pref string
        /// </summary>
        /// <returns></returns>
        public static string GetLoginComplete()
        {
            return PlayerPrefs.GetString("Completed Log in");
        }
        #endregion
        #region "sign up prefs"
        /// <summary>
        /// sets the "Signed in" pref to the passed value
        /// </summary>
        /// <param name="value"></param>
        public static void SetSignUpSuccessful(string value)
        {
            PlayerPrefs.SetString("Signed in", value);
        }
        /// <summary>
        /// returns the "Signed in" player pref string
        /// </summary>
        /// <returns></returns>
        public static string GetSignUpSuccessful()
        {
            return PlayerPrefs.GetString("Signed in");
        }
        /// <summary>
        /// sets the "Completed Sign up" pref to the passed value
        /// </summary>
        /// <param name="value"></param>
        public static void SetSignUpComplete(string value)
        {
            PlayerPrefs.SetString("Completed Sign up", value);
        }
        /// <summary>
        /// returns the "Completed Sign up" player oref string
        /// </summary>
        /// <returns></returns>
        public static string GetSignupComplete()
        {
            return PlayerPrefs.GetString("Completed Sign up");
        }
        #endregion
    }
}