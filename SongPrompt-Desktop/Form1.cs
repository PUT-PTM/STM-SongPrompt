using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;


namespace SongPrompt
{
    public struct TrackInfo
    {
        public string _author;
        public string _title;
        public string _time;
    }
    public partial class SongPrompt : Form
    {
        private readonly SpotifyLocalAPI _spotify;
        private Track _currentTrack;
        private TrackInfo _trackInfo;
        private SerialPort mySerialPort;
        public string chosenPort;

        /**
         * Constructor
         * Creates USer Interface, detects changes
         */
        public SongPrompt()
        {
            InitializeComponent();

            Hide();
            
            _trackInfo = new TrackInfo();
            mySerialPort = new SerialPort();

            titleSetLbl.Text = "";
            authorSetLbl.Text = "";
            timeLabel.Text = "0:00 / 0:00";
            try
            {
                fillComboBoxWithPorts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _spotify = new SpotifyLocalAPI();

            _spotify.OnPlayStateChange += _spotify_OnPlayStateChange;
            _spotify.OnTrackChange += _spotify_OnTrackChange;
            _spotify.OnTrackTimeChange += _spotify_OnTrackTimeChange;

            titleSetLbl.Click += (sender, args) => Process.Start(titleSetLbl.Tag.ToString());
            authorSetLbl.Click += (sender, args) => Process.Start(authorSetLbl.Tag.ToString());
            
            notifyIcon1.Visible = true;
            
        }

        /**
         * Filling ComboBox with all Ports available in system.
         * 
         */
        void fillComboBoxWithPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                portComComboBox.Items.Add(port);
            }
        }

        /**
         * Connection to Spotify
         * Try to connect to Local Spotify App
         */
        public void Connect()
        {
            // Check whether the app is running

           if (_spotify.Connect())
            {
                // If the connection to Spotify is OK, set Label.
                connectionStatusLbl.Text = @"OK";
                UpdateInfos();
                _spotify.ListenForEvents = true;
                if (mySerialPort.IsOpen)
                {
                    mySerialPort.Write(_trackInfo._author + ";" + _trackInfo._title + ";" + _trackInfo._time + "^");
                }
                else
                {
                    MessageBox.Show(@"Brak połączenia z portem COM", "Brak połączenia z portem COM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                DialogResult res = MessageBox.Show(@"Nie można było połączyć się do Spotify, ponowić próbę?", @"Połączenei z Spotify",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                    Connect();
            }
            
        }

        /**
         * Gets status from Spotify and updates it
         */
        public void UpdateInfos()
        {
            StatusResponse status = _spotify.GetStatus();
            if (status == null)
                return;
            if (status.Track != null)
                UpdateTrack(status.Track);
        }

        /**
         * Update Track info
         */
        public async void UpdateTrack(Track track)
        {
            _currentTrack = track;

            if (track.IsAd())
                return;

            titleSetLbl.Text = track.TrackResource.Name;
            titleSetLbl.Tag = track.TrackResource.Uri;
            _trackInfo._title = track.TrackResource.Name;

            authorSetLbl.Text = track.ArtistResource.Name;
            authorSetLbl.Tag = track.ArtistResource.Uri;
            _trackInfo._author = track.ArtistResource.Name;
            Console.WriteLine(_trackInfo._author + ";" + _trackInfo._title + ";" + _trackInfo._time + "^");
            Console.WriteLine(_trackInfo._author + ";" + _trackInfo._title + ";0");

            if (mySerialPort.IsOpen)
            {
                
                mySerialPort.Write("Spotify ON 0");
                mySerialPort.Write(_trackInfo._author + ";" + _trackInfo._title + ";" + _trackInfo._time + "^");
                
            }
        }

        
        /**
         * Method that converts time to format: mm:ss
         */
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
            timeLabel.Text = $@"{FormatTime(e.TrackTime)} / {FormatTime(_currentTrack.Length)}";
            _trackInfo._time = timeLabel.Text;
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Write(_trackInfo._author + ";" + _trackInfo._title + ";" + _trackInfo._time + "^");
            }
            mySerialPort.Write(_trackInfo._author + ";" + _trackInfo._title + ";" + _trackInfo._time + "^");
            
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

        /**
         * Event for check connetion with Spotify button.
         * 
         */
        private void checkConnBtn_Click(object sender, EventArgs e)
        {
            Connect();
        }

        /**
         * Event for COM port connection button.
         * Tries to connect to given COM port.
         */
        private void comPortConnect_Click(object sender, EventArgs e)
        {
            try
            {
                chosenPort = portComComboBox.SelectedItem.ToString();
                mySerialPort.PortName = chosenPort;
                mySerialPort.BaudRate = 9600;
                mySerialPort.Parity = Parity.None;
                mySerialPort.StopBits = StopBits.One;
                mySerialPort.DataBits = 8;
                mySerialPort.Handshake = Handshake.None;
                mySerialPort.RtsEnable = true;

                mySerialPort.Open();

                if (mySerialPort.IsOpen)
                {
                    comConnectionLbl.Text = "OK";
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Proszę wybrać port COM i spróbować ponownie.", "Nie wybrano portu COM",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void settingsMenuStrip_Click(object sender, EventArgs e)
        {
            Opacity = 1.0;
            ShowInTaskbar = true;
            ShowIcon = true;
            Visible = true;
            WindowState = FormWindowState.Normal;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.Visible = false;
        }

        private void exitMenuStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
