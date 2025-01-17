using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Nova
{
    using MusicUnlockInfo = SerializableHashSet<string>;

    public enum MusicListMode
    {
        Sequential,
        SingleLoop,
        Random
    }

    public class MusicGalleryController : ViewControllerBase
    {
        public const string MusicUnlockStatusKey = "music_unlock_status";

        public MusicGalleryPlayer musicPlayer;
        public MusicEntryList musicList;
        public Transform musicListScrollContent;
        public MusicGalleryEntry musicEntryPrefab;
        public GameObject lockedMusicEntryPrefab;
        public List<AudioController> audioControllersToDisable;

        private CheckpointManager checkpointManager;

        // The indices of entries in allMusics are their indices in unlockedMusics
        // The indices of locked entries are -1
        private const int LockedIndex = -1;

        private List<MusicListEntry> allMusics;
        private List<MusicListEntry> unlockedMusics;

        private MusicListMode _mode = MusicListMode.Sequential;

        public MusicListMode mode
        {
            get => _mode;
            set
            {
                if (_mode == value) return;
                _mode = value;
                RefreshMusicPlayerList();
            }
        }

        private IMusicList GetMusicList(MusicListEntry currentMusic)
        {
            Assert.IsNotNull(currentMusic);
            if (unlockedMusics.Count == 0)
            {
                return null;
            }

            switch (mode)
            {
                case MusicListMode.Sequential:
                    return new SequentialMusicList(unlockedMusics, currentMusic.index);
                case MusicListMode.SingleLoop:
                    return new SingleLoopMusicList(unlockedMusics, currentMusic.index);
                case MusicListMode.Random:
                    return new RandomMusicList(unlockedMusics, currentMusic.index);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Play(MusicListEntry music)
        {
            musicPlayer.musicList = GetMusicList(music);
            musicPlayer.Play();
        }

        private void PauseOtherAudios()
        {
            foreach (var ac in audioControllersToDisable)
            {
                ac.Pause();
            }
        }

        private void RestoreOtherAudios()
        {
            foreach (var ac in audioControllersToDisable)
            {
                ac.UnPause();
            }
        }

        protected override void Awake()
        {
            base.Awake();

            checkpointManager = Utils.FindNovaGameController().CheckpointManager;
            allMusics = musicList.entries.Select(entry => new MusicListEntry(LockedIndex, entry)).ToList();
        }

        protected override void Start()
        {
            base.Start();

            checkpointManager.Init();
        }

        public override void Show(Action onFinish)
        {
            PauseOtherAudios();
            RefreshContent();

            base.Show(onFinish);
        }

        public override void Hide(Action onFinish)
        {
            RestoreOtherAudios();
            musicPlayer.Pause();

            base.Hide(onFinish);
        }

        private void RefreshContent()
        {
            UpdateUnlockedMusics();
            RefreshMusicListView();
            RefreshMusicPlayerList();
        }

        private static bool IsUnlocked(MusicListEntry entry)
        {
            return entry.index != LockedIndex;
        }

        private static bool IsUnlocked(ICollection<string> unlockInfo, MusicEntry entry)
        {
            return unlockInfo.Contains(Utils.ConvertPathSeparator(entry.resourcePath));
        }

        private void UpdateUnlockedMusics()
        {
            var unlockInfo = checkpointManager.Get(MusicUnlockStatusKey, new MusicUnlockInfo());
            unlockedMusics = new List<MusicListEntry>();
            foreach (var music in allMusics)
            {
                if (IsUnlocked(unlockInfo, music.entry))
                {
                    music.index = unlockedMusics.Count;
                    unlockedMusics.Add(music);
                }
                else
                {
                    music.index = LockedIndex;
                }
            }
        }

        private void ClearMusicListView()
        {
            var children = new List<GameObject>();
            foreach (Transform child in musicListScrollContent)
            {
                children.Add(child.gameObject);
            }

            foreach (var child in children)
            {
                Destroy(child);
            }
        }

        private void RefreshMusicListView()
        {
            ClearMusicListView();
            foreach (var music in allMusics)
            {
                if (IsUnlocked(music))
                {
                    var entry = Instantiate(musicEntryPrefab, musicListScrollContent, false);
                    entry.Init(music, Play);
                }
                else
                {
                    Instantiate(lockedMusicEntryPrefab, musicListScrollContent, false);
                }
            }
        }

        private void RefreshMusicPlayerList()
        {
            if (musicPlayer == null || musicPlayer.musicList == null) return;
            musicPlayer.musicList = GetMusicList(musicPlayer.musicList.Current());
        }

        #region For debug

        private void UnlockAllMusics()
        {
            var unlockInfo = checkpointManager.Get(MusicUnlockStatusKey, new MusicUnlockInfo());
            foreach (var music in allMusics)
            {
                unlockInfo.Add(Utils.ConvertPathSeparator(music.entry.resourcePath));
            }

            checkpointManager.Set(MusicUnlockStatusKey, unlockInfo);
            RefreshContent();
        }

        protected override void OnActivatedUpdate()
        {
            base.OnActivatedUpdate();

            if (Utils.GetKeyDownInEditor(KeyCode.LeftShift))
            {
                UnlockAllMusics();
            }
        }

        #endregion
    }
}