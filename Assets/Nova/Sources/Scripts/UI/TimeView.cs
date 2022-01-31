using System;
using UnityEngine;
using UnityEngine.UI;

namespace Nova
{
    public class TimeView : MonoBehaviour
    {
        private const int UpdateInterval = 60;
        private Text timeText;
        private DateTime nextTimeUpdate;
        private static DateTime curTime => DateTime.Now;

        private void Awake()
        {
            timeText = GetComponent<Text>();
            UpdateTime();
        }

        void Update()
        {
            var timeSeconds = (curTime - nextTimeUpdate).Seconds;
            if (timeSeconds <= UpdateInterval)
            {
                return;
            }

            UpdateTime();
        }

        private void UpdateTime()
        {
            timeText.text = curTime.ToShortTimeString();
            nextTimeUpdate = curTime;
        }
    }
}
