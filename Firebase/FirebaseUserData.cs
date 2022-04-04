using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

public class FirebaseUserData : MonoBehaviour
{
    public void AddUser(User user)
    {
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Collection("Users").Document(user.Email).SetAsync(user);
        Debug.Log("Successfully added user details to database");

    }
    public void UpdateUser(User user)
    {

    }
}
