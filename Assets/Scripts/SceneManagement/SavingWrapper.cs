﻿using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{

    public class SavingWrapper : MonoBehaviour
    {

        const string defaultSaveFile = "save";

        [SerializeField] float fadeInTime = 1f;

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutImmediate();

            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);

            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }

}