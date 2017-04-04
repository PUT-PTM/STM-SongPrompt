using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;


namespace SongPrompt
{
    public partial class SongPrompt : Form
    {
        private readonly SpotifyLocalAPI _spotify;
        private Track _currentTrack;
        public SongPrompt()
        {
            InitializeComponent();
            _spotify = new SpotifyLocalAPI();

            _spotify.OnPlayStateChange += _spotify_OnPlayStateChange;
            _spotify.OnTrackChange += _spotify_OnTrackChange;
            _spotify.OnTrackTimeChange += _spotify_OnTrackTimeChange;

            titleSetLbl.Click += (sender, args) => Process.Start(titleSetLbl.Tag.ToString());
            authorSetLbl.Click += (sender, args) => Process.Start(authorSetLbl.Tag.ToString());
        }

        public void Connect()
        {
            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                MessageBox.Show(@"Spotify nie jest uruchomione");
                return;
            }
            bool successful = _spotify.Connect();
            if (successful)
            {
                connectionStatusLbl.Text = @"OK";
                UpdateInfos();
                _spotify.ListenForEvents = true;
            }
            else
            {
                DialogResult res = MessageBox.Show(@"Nie można było połączyć się do Spotify, ponowić próbę?", @"Spotify",
                    MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                    Connect();
            }
        }

        public void UpdateInfos()
        {
            StatusResponse status = _spotify.GetStatus();
            if (status == null)
                return;
            if (status.Track != null)
                UpdateTrack(status.Track);
        }

        public async void UpdateTrack(Track track)
        {
            _currentTrack = track;

            if (track.IsAd())
                return;

            titleSetLbl.Text = track.TrackResource.Name;
            titleSetLbl.Tag = track.TrackResource.Uri;

            authorSetLbl.Text = track.ArtistResource.Name;
            authorSetLbl.Tag = track.ArtistResource.Uri;
        }

        private void checkConnBtn_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private static String FormatTime(double sec)
        {
            TimeSpan span = TimeSpan.FromSeconds(sec);
            String secs = span.Seconds.ToString(), mins = span.Minutes.ToString();
            if (secs.Length < 2)
                secs = "0" + secs;
            return mins + ":" + secs;
        }

        private void _spotify_OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => _spotify_OnTrackTimeChange(sender, e)));
                return;
            }
            timeLabel.Text = $@"{FormatTime(e.TrackTime)}/{FormatTime(_currentTrack.Length)}";
        }

        private void _spotify_OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => _spotify_OnTrackChange(sender, e)));
                return;
            }
            UpdateTrack(e.NewTrack);
        }

        private void _spotify_OnPlayStateChange(object sender, PlayStateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => _spotify_OnPlayStateChange(sender, e)));
                return;
            }
        }
    }
}
