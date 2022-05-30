using ElephantSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Elephant integration

namespace Happy.Analytics
{
    public partial class HappyAnalytics : MonoBehaviour
    {
        private void SendEvent_Elephant(string eventName, int levelNumber)
        {
            Elephant.Event(eventName, levelNumber);
        }

        private void LevelEvent_Elephant(LevelEvents type, int levelNumber)
        {
            switch (type)
            {
                case LevelEvents.LevelStarted:
                    Elephant.LevelStarted(levelNumber);
                    break;
                case LevelEvents.LevelFailed:
                    Elephant.LevelFailed(levelNumber);
                    break;
                case LevelEvents.LevelCompleted:
                    Elephant.LevelCompleted(levelNumber);
                    break;
                default:
                    break;
            }

            Debug.Log($"LevelEvent_Elephant: {type} Level: {levelNumber}");
        }
    } 
}

