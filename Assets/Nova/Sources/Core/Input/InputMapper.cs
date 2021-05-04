using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Nova
{
    using KeyStatus = Dictionary<AbstractKey, bool>;
    using AbstractKeyGroups = Dictionary<AbstractKey, AbstractKeyGroup>;
    using AbstractKeyTags = Dictionary<AbstractKey, bool>;

    public class InputMapper : MonoBehaviour
    {
        public static string InputFilesDirectory => Path.Combine(Application.persistentDataPath, "Input");

        private static string KeyBoardMappingFilePath => Path.Combine(InputFilesDirectory, "keyboard.json");

        public TextAsset defaultKeyboardMapping;

        private KeyStatus keyStatus = new KeyStatus();
        private KeyStatus keyStatusLastFrame = new KeyStatus();
        private KeyStatus keyEnabled = new KeyStatus();
        private readonly KeyStatus keyTriggered = new KeyStatus();

        public readonly AbstractKeyboard keyboard = new AbstractKeyboard();
        public readonly AbstractKeyGroups keyGroups = new AbstractKeyGroups();
        public readonly AbstractKeyTags keyIsEditor = new AbstractKeyTags();

        private readonly AbstractKeyboard defaultKeyboard = new AbstractKeyboard();

        #region Enable

        public void SetEnable(AbstractKey key, bool value)
        {
            keyEnabled[key] = value;
        }

        public bool IsEnabled(AbstractKey key)
        {
#if !UNITY_EDITOR
            if (keyIsEditor[key])
            {
                return false;
            }
#endif

            return keyEnabled[key];
        }

        public void SetEnableGroup(AbstractKeyGroup group)
        {
            foreach (AbstractKey key in Enum.GetValues(typeof(AbstractKey)))
            {
                SetEnable(key, (keyGroups[key] & group) > 0);
            }
        }

        public KeyStatus GetEnabledState()
        {
            return keyEnabled.ToDictionary(x => x.Key, x => x.Value);
        }

        public void SetEnabledState(KeyStatus keyEnabled)
        {
            this.keyEnabled = keyEnabled;
        }

        #endregion

        public AbstractKeyboardData GetDefaultKeyboardData()
        {
            return defaultKeyboard.Data.GetCopy();
        }

        public List<CompoundKey> GetDefaultCompoundKeys(AbstractKey key)
        {
            return defaultKeyboard.Data[key].Select(ck => new CompoundKey(ck)).ToList();
        }

        private IEnumerable<IAbstractKeyDevice> keyDevices
        {
            get { yield return keyboard; }
        }

        #region Save and load

        private void LoadKeyBoard()
        {
            defaultKeyboard.LoadFull(defaultKeyboardMapping.text, keyGroups, keyIsEditor);

            try
            {
                keyboard.Load(File.ReadAllText(KeyBoardMappingFilePath));

                // Use default values to fill missing keys
                foreach (AbstractKey ak in Enum.GetValues(typeof(AbstractKey)))
                {
                    if (!keyboard.Data.ContainsKey(ak))
                    {
                        keyboard.Data[ak] = GetDefaultCompoundKeys(ak);
                    }
                }
            }
            catch
            {
                Debug.LogWarning("Nova: Failed to load input mapping file, use default input mapping.");
                keyboard.Data = GetDefaultKeyboardData();
            }
        }

        private void SaveKeyBoard()
        {
            File.WriteAllText(KeyBoardMappingFilePath, keyboard.Json());
        }

        public void Save()
        {
            if (!Directory.Exists(InputFilesDirectory))
            {
                Directory.CreateDirectory(InputFilesDirectory);
            }

            SaveKeyBoard();
        }

        #endregion

        private void Awake()
        {
            LoadKeyBoard();

            foreach (AbstractKey key in Enum.GetValues(typeof(AbstractKey)))
            {
                keyStatus[key] = false;
                keyStatusLastFrame[key] = false;
                keyEnabled[key] = true;
                keyTriggered[key] = false;
            }
        }

        private void OnDestroy()
        {
            Save();
        }

        // Trigger at most one time in each frame
        public bool GetKey(AbstractKey key)
        {
            if (!IsEnabled(key)) return false;
            if (keyTriggered[key]) return false;
            keyTriggered[key] = true;
            return keyStatus[key];
        }

        public bool GetKeyDown(AbstractKey key)
        {
            if (!IsEnabled(key)) return false;
            if (keyTriggered[key]) return false;
            keyTriggered[key] = true;
            return !keyStatusLastFrame[key] && keyStatus[key];
        }

        public bool GetKeyUp(AbstractKey key)
        {
            if (!IsEnabled(key)) return false;
            if (keyTriggered[key]) return false;
            keyTriggered[key] = true;
            return keyStatusLastFrame[key] && !keyStatus[key];
        }

        private void SwapKeyStatus()
        {
            var tmp = keyStatusLastFrame;
            keyStatusLastFrame = keyStatus;
            keyStatus = tmp;
        }

        private void Update()
        {
            foreach (var device in keyDevices)
            {
                device.Update();
            }

            SwapKeyStatus();

            foreach (AbstractKey key in Enum.GetValues(typeof(AbstractKey)))
            {
                keyStatus[key] = keyDevices.Any(device => device.GetKey(key));
                keyTriggered[key] = false;
            }
        }
    }
}