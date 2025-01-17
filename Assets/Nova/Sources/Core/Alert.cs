﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Nova
{
    [Serializable]
    public class AlertParameters
    {
        public readonly string title;
        public readonly string bodyContent;
        public readonly Action onConfirm;
        public readonly Action onCancel;
        public readonly string ignoreKey;
        public readonly bool lite;

        public AlertParameters(string title, string bodyContent, Action onConfirm, Action onCancel, string ignoreKey)
        {
            this.title = title;
            this.bodyContent = bodyContent;
            this.onConfirm = onConfirm;
            this.onCancel = onCancel;
            this.ignoreKey = ignoreKey;
            lite = false;
        }

        public AlertParameters(string bodyContent, Action onCancel)
        {
            this.bodyContent = bodyContent;
            this.onCancel = onCancel;
            lite = true;
        }
    }

    [Serializable]
    public class AlertEvent : UnityEvent<AlertParameters> { }

    /// <summary>
    /// A delegate for alert function to be used across the engine core and the game.
    /// </summary>
    [ExportCustomType]
    public class Alert : MonoBehaviour
    {
        public const string AlertKeyPrefix = ConfigManager.TrackedKeyPrefix + "Alert";

        public AlertEvent alertFunction;

        private static AlertEvent _alert;

        private void Awake()
        {
            if (alertFunction == null)
            {
                Debug.LogError("Nova: AlertFunction not set on Alert component.");
                Utils.Quit();
            }

            _alert = alertFunction;
        }

        private static void AssertAlertFunction()
        {
            if (_alert == null)
            {
                if (StackTraceUtility.ExtractStackTrace().Contains("Awake"))
                    Debug.LogError("Nova: Alert() should not be called in Awake().");
                else
                    Debug.LogError("Nova: Missing Alert component in initial scene.");
                Utils.Quit();
            }
        }

        /// <summary>
        /// Show a game-provided alert box. Leave the two actions null to hide cancel button.
        /// </summary>
        /// <param name="title">Title of the alert box. Null to hide.</param>
        /// <param name="bodyContent">Body of the alert box. Null to hide.</param>
        /// <param name="onClickConfirm">Action executed when positive button is clicked or the alert box has been ignored.</param>
        /// <param name="onClickCancel">Action executed when negative button is clicked.</param>
        /// <param name="ignoreKey">A key to identify the alert box when prompting user to ignore it. "" to disable this feature.</param>
        public static void Show(string title, string bodyContent,
            Action onClickConfirm = null, Action onClickCancel = null,
            string ignoreKey = "")
        {
            AssertAlertFunction();
            _alert.Invoke(new AlertParameters(title, bodyContent, onClickConfirm, onClickCancel,
                ignoreKey == "" ? "" : AlertKeyPrefix + ignoreKey));
        }

        /// <summary>
        /// Show a simple notification which will fade out itself.
        /// </summary>
        public static void Show(string content, Action onFinish = null)
        {
            AssertAlertFunction();
            _alert.Invoke(new AlertParameters(content, onFinish));
        }
    }
}