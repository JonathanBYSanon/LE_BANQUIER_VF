using System;
using System.Media;
using System.Windows;
using System.Windows.Media;

namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Service to manage sound effects and background music, there is 3 different players for each group of sounds.
    /// </summary>
    public class SoundManagerService
    {
        private static bool _isMuted = false;
        public static bool IsMuted
        {
            get => _isMuted;
            set
            {
                _isMuted = value;
                if (_isMuted)
                {
                    _instance.StopMusic();
                }
                else
                {
                    _instance.PlayMusic("BackgroundSound.mp3", true);
                }
            }
        }

        private static SoundManagerService _instance = new SoundManagerService();
        // Media players for the background music
        private MediaPlayer _mediaPlayer;
        // Media players for the sound effects
        private MediaPlayer _soundPlayer;
        // Media players for the notifications about new messages
        private MediaPlayer _notifPlayer;

        // Private constructor to prevent instantiation
        private SoundManagerService()
        {
            _mediaPlayer = new MediaPlayer();
            _soundPlayer = new MediaPlayer();
            _notifPlayer = new MediaPlayer();
        }

        // Static instance to access globally
        public static SoundManagerService Instance => _instance;

        // Play MP3 (for background music and longer effects)
        public void PlayMusic(string fileName, bool loop = false)
        {
            if (_isMuted) return;
            try
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource/Sound/", fileName);

                _mediaPlayer.Open(new Uri(filePath, UriKind.Absolute));
                _mediaPlayer.MediaEnded -= MediaPlayer_MediaEnded;

                if (loop)
                {
                    _mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                }

                _mediaPlayer.Volume = 0.1;
                _mediaPlayer.Play();               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to play the music: " + ex.Message);
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            _mediaPlayer.Position = TimeSpan.Zero;
            _mediaPlayer.Play();
        }

        // Stop music playback
        public void StopMusic()
        {
            _mediaPlayer.Stop();
        }

        // Play short sound effects
        public void PlayEffect(string fileName)
        {
            if (_isMuted) return;
            try
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource/Sound/", fileName);

                _soundPlayer.Open(new Uri(filePath, UriKind.Absolute));

                _soundPlayer.Play();
                _notifPlayer.Volume = 0.5;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to play the sound effect: " + ex.Message);
            }
        }

        // Stop sound effect playback
        public void StopEffect()
        {
            _soundPlayer.Stop();
        }

        // Play short sound effects
        public void PlayNotif(string fileName)
        {
            if (_isMuted) return;
            try
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource/Sound/", fileName);

                _notifPlayer.Open(new Uri(filePath, UriKind.Absolute));

                _notifPlayer.Play();
                _notifPlayer.Volume = 0.05;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to play the sound effect: " + ex.Message);
            }
        }

        // Stop sound effect playback
        public void StopNotif()
        {
            _notifPlayer.Stop();
        }

    }

}
