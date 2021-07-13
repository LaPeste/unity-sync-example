using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using System;

public class AddCustomUserData: MonoBehaviour
{
    [SerializeField] private Button _addBtn;

    private PlayerStats _playerStats;
    private GameManager _gameManager;
    private Realm _realm;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _addBtn.onClick.AddListener(OnClickAdd);
    }

    async void OnClickAdd()
    {
        if (_gameManager.CustomUserData)
        {
            var cud = _gameManager.User.GetCustomData<CustomUserData>();
            if (cud == null)
            {
                var mongoClient = _gameManager.User.GetMongoClient("mongodb-atlas");
                var dbTracker = mongoClient.GetDatabase("UnityDevDB");
                var cudCollection = dbTracker.GetCollection<CustomUserData>("UserPrefs");
                var cudToInstert = new CustomUserData("player1")
                {
                    Development = "noIdeaWhatThisFieldIs",
                    Age = 20,
                    Gender = "Male",
                    Language = "English",
                };
                var foundObj = await cudCollection.FindOneAsync(cudToInstert);
                if (foundObj == null)
                {
                    var insertResult = await cudCollection.InsertOneAsync(cudToInstert);
                    Debug.Log($"InsertedIDs: {insertResult.InsertedId}");
                }
                else
                {
                    Debug.Log("Obj was already added");
                }


            }
        }
    }
}