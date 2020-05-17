using System;

namespace QFramework
{
    public class DeprecatedConfig
    {
        public const bool FORCE_DEPRECATED = true;
    }

    public partial class AudioManager
    {

        [Obsolete("AudioManager.IsSoundOn is depreacated,Please use AudioKit.Settings.IsSoundOn.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static bool IsSoundOn
        {
            get { return AudioKit.Settings.IsSoundOn.Value; }
            set { AudioKit.Settings.IsSoundOn.Value = value; }
        }

        [Obsolete("AudioManager.IsMusicOn is depreacated,Please use AudioKit.Settings.IsMusicOn.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static bool IsMusicOn
        {
            get { return AudioKit.Settings.IsMusicOn.Value; }
            set { AudioKit.Settings.IsMusicOn.Value = value; }
        }

        [Obsolete("AudioManager.IsVoiceOn is depreacated,Please use AudioKit.Settings.IsVoiceOn.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static bool IsVoiceOn
        {
            get { return AudioKit.Settings.IsVoiceOn.Value; }
            set { AudioKit.Settings.IsVoiceOn.Value = value; }
        }

        [Obsolete("AudioManager.SoundVolume is depreacated,Please use AudioKit.Settings.SoundVolume.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static float SoundVolume
        {
            get { return AudioKit.Settings.SoundVolume.Value; }
            set { AudioKit.Settings.SoundVolume.Value = value; }
        }

        [Obsolete("AudioManager.SoundVolume is depreacated,Please use AudioKit.Settings.MusicVolume.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static float MusicVolume
        {
            get { return AudioKit.Settings.MusicVolume.Value; }
            set { AudioKit.Settings.MusicVolume.Value = value; }
        }

        [Obsolete("AudioManager.VoiceVolume is depreacated,Please use AudioKit.Settings.VoiceVolume.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static float VoiceVolume
        {
            get { return AudioKit.Settings.VoiceVolume.Value; }
            set { AudioKit.Settings.VoiceVolume.Value = value; }
        }


        [Obsolete(
            "AudioManager.SetSoundOn() is depreacated,Please use AudioKit.Settings.IsSoundOn.Value = true instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void SetSoundOn()
        {
            IsSoundOn = true;
        }

        [Obsolete(
            "AudioManager.SetSoundOff() is depreacated,Please use AudioKit.Settings.IsSoundOn.Value = false instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void SetSoundOff()
        {
            IsSoundOn = false;
        }

        [Obsolete(
            "AudioManager.SetVoiceOn() is depreacated,Please use AudioKit.Settings.IsVoiceOn.Value = true instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void SetVoiceOn()
        {
            IsVoiceOn = true;
        }

        [Obsolete(
            "AudioManager.SetVoiceOff() is depreacated,Please use AudioKit.Settings.IsVoiceOn.Value = false instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void SetVoiceOff()
        {
            IsVoiceOn = false;
        }

        [Obsolete("AudioManager.IsOn is depreacated,Please use AudioKit.Settings.IsOn.Value instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static bool IsOn
        {
            get
            {
                return AudioKit.Settings.IsSoundOn.Value && AudioKit.Settings.IsMusicOn.Value &&
                       AudioKit.Settings.IsVoiceOn.Value;
            }
        }

        [Obsolete("AudioManager.On() is depreacated,Please use AudioKit.Settings.IsOnValue = true instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void On()
        {
            AudioKit.Settings.IsSoundOn.Value = true;
            AudioKit.Settings.IsMusicOn.Value = true;
            AudioKit.Settings.IsVoiceOn.Value = true;
        }

        [Obsolete("AudioManager.On() is depreacated,Please use AudioKit.Settings.IsOnValue = false instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void Off()
        {
            AudioKit.Settings.IsSoundOn.Value = false;
            AudioKit.Settings.IsMusicOn.Value = false;
            AudioKit.Settings.IsVoiceOn.Value = false;
        }


        [Obsolete(
            "AudioManager.SetMusicOn() is depreacated,Please use AudioKit.Settings.IsMusicOn.Value = true instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void SetMusicOn()
        {
            AudioKit.Settings.IsMusicOn.Value = true;

            var self = Instance;

            if (self.CurrentMusicName.IsNotNullAndEmpty())
            {
                self.SendMsg(new AudioMusicMsg(self.CurrentMusicName, true));
            }
        }

        [Obsolete(
            "AudioManager.SetMusicOff() is depreacated,Please use AudioKit.Settings.IsMusicOn.Value = false instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void SetMusicOff()
        {
            IsMusicOn = false;
            StopMusic();
        }

        [Obsolete("AudioManager.PlayMusic() is depreacated,Please use AudioKit.PlayMusic() instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void PlayMusic(string musicName)
        {
            AudioKit.PlayMusic(musicName);
        }

        [Obsolete("AudioManager.PlayMusic() is depreacated,Please use AudioKit.PlayMusic() instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void PlayMusic(string musicName, bool loop = true, Action onBeganCallback = null,
            Action onEndCallback = null, bool allowMusicOff = true, float volume = -1f)
        {
            AudioKit.PlayMusic(musicName, loop, onBeganCallback, onEndCallback, allowMusicOff, volume);
        }

        [Obsolete("AudioManager.StopMusic() is depreacated,Please use AudioKit.StopMusic() instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        /// <summary>
        /// 停止播放音乐
        /// </summary>
        public static void StopMusic()
        {
            AudioKit.StopMusic();
        }

        [Obsolete("AudioManager.PauseMusic() is depreacated,Please use AudioKit.PauseMusic() instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void PauseMusic()
        {
            AudioKit.PauseMusic();
        }

        [Obsolete("AudioManager.ResumeMusic() is depreacated,Please use AudioKit.ResumeMusic() instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void ResumeMusic()
        {
            AudioKit.ResumeMusic();
        }
        
        [Obsolete("AudioManager.PlayVoice() is depreacated,Please use AudioKit.PlayVoice() instead",
            DeprecatedConfig.FORCE_DEPRECATED)]
        public static void PlayVoice(string voiceName, bool loop = false,Action onBeganCallback = null,Action onEndedCallback = null)
        {
            AudioKit.PlayVoice(voiceName, loop, onBeganCallback, onEndedCallback);
        }
        
        [Obsolete("AudioManager.StopVoice() is depreacated,Please use AudioKit.StopVoice() instead", DeprecatedConfig.FORCE_DEPRECATED)]
        public static void StopVoice()
        {
            AudioKit.StopVoice();
        }

        [Obsolete("AudioManager.PlaySound() is depreacated,Please use AudioKit.PlaySound() instead", DeprecatedConfig.FORCE_DEPRECATED)]
        public static void PlaySound(string soundName, bool loop = false, Action<AudioPlayer> callBack = null,
            int customEventId = -1)
        {
            AudioKit.PlaySound(soundName,loop,callBack,customEventId);
        }
    }

    [Obsolete("AudioUnit is deprecated,Please use AudioPlayer instead", DeprecatedConfig.FORCE_DEPRECATED)]
    public class AudioUnit : AudioPlayer
    {

    }
}