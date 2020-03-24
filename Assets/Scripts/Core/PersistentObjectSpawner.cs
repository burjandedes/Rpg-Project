﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class PersistentObjectSpawner : MonoBehaviour
    {

        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        void Awake()
        {
            if (!hasSpawned)
            {
                SpawnPersistentObjects();

                hasSpawned = true;
            }
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }

}