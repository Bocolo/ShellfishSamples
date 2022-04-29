using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine;
namespace App.Authentication
{
    /// <summary>
    /// This script is responsible for the firebase authentication logic
    /// </summary>
    public class AuthenticationLogic
    {
        public bool IsSuccessfulLogin { get; private set; } = false;
        public bool IsSuccessfulSignUp { get; private set; } = false;


        /// <summary>
        /// Attempts to set up a firebase user auth account using the email, password and name params
        /// sets the _isSuccessfulSignUp bool based on the sign up attempts success
        /// sets the new account display name if successful
        /// </summary>
        /// <param name="email">represents the login email</param>
        /// <param name="password">representd the login password</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task SetUpAuthentication(FirebaseAuth auth, string email, string password, string name)
        {
            await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    IsSuccessfulSignUp = false;
                    return;
                }
                if (task.IsFaulted)
                {
                    IsSuccessfulSignUp = false;
                    return;
                }
                auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
                {
                    DisplayName = name,
                });
                Debug.Log("Sign Up Succeeded. Current User: " + auth.CurrentUser.DisplayName);
                IsSuccessfulSignUp = true;
                return;
            });
        }
        /// <summary>
        /// Attempts to validate a firebase user login using the email and password params
        /// sets the _isSuccessfulLogin bool based on the sign attempts success
        /// </summary>
        /// <param name="email">represents the login email</param>
        /// <param name="password">representd the login password</param>
        /// <returns></returns>
        public async Task ValidateAuthentication(FirebaseAuth auth, string email, string password)
        {
            await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    IsSuccessfulLogin = false;
                    return;
                }
                if (task.IsFaulted)
                {
                    IsSuccessfulLogin = false;
                    return;
                }
                IsSuccessfulLogin = true;
            });
        }
    }
}