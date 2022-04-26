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

    public static void SetLoginSuccessful(string value)
    {
        PlayerPrefs.SetString("Loggedin", value);
    }

    public static string GetLoginSuccessful()
    {
        return PlayerPrefs.GetString("Loggedin");
    }
    public static void SetSignUpSuccessful(string value)
    {
        PlayerPrefs.SetString("Signed in", value);
    }

    public static string GetSignUpSuccessful()
    {
        return PlayerPrefs.GetString("Signed in");
    }
    public static void SetLoginComplete(string value)
    {
        PlayerPrefs.SetString("Completed Log in", value);
    }

    public static string GetLoginComplete()
    {
        return PlayerPrefs.GetString("Completed Log in");
    }
    public static void SetSignUpComplete(string value)
    {
        PlayerPrefs.SetString("Completed Sign up", value);
    }

    public static string GetSignupComplete()
    {
        return PlayerPrefs.GetString("Completed Sign up");
    }
}
