using UnityEngine;
/// <summary>
/// This manages all PlayerPrefs
/// Saves and gets playerprefs 
/// </summary>
public class UserPrefs
{
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
}
