﻿using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {

        GameObject player;

        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;

            player = GameObject.FindWithTag("Player");
        }

        private void DisableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

    }
}
