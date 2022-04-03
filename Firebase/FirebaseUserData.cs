using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

public class FirebaseUserData : MonoBehaviour
{
    public void AddUser(UserData userDetails)
    {
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Collection("Users").Document(userDetails.Email).SetAsync(userDetails);
        Debug.Log("Successfully added user details to database");

    }
    public void UpdateUser(UserData userDetails)
    {

    }
}
