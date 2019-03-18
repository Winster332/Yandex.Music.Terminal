using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using Yandex.Music.Api;
using Yandex.Music.Api.Models;

namespace TermStyle
{
    public class AudioPlayer
    {
        private string _title;
        private TimeSpan _time;
        private int TotalPosition = 0;
        private YandexApi _api;
        private List<YandexTrack> _tracks;
        private int _activeTrack;
        private TimeSpan _currentTime;
        
        public AudioPlayer(YandexApi api, List<YandexTrack> tracks)
        {
            _api = api;
            _tracks = tracks;
            _activeTrack = 0;
            _currentTime = new TimeSpan();
        }

        public void Play()
        {
            var track = _tracks[_activeTrack];
            
            Console.WriteLine("Extracting track...");
            _api.ExtractTrackToFile(track, "music");
            
            Console.Clear();
            Task.Factory.StartNew(() =>
            {
                using (var audioFile = new AudioFileReader($"music/{track.Title}.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    
                    SetTrack(track.Title, audioFile.TotalTime);

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Console.Clear();
                        SetCurrentPosition(audioFile.CurrentTime);
                        Show();
                        Thread.Sleep(1000);
                    }
                    
                    Play();
                }
            });
            
            
            _activeTrack++;
        }

        public void SetTrack(string title, TimeSpan time)
        {
            _title = title;
            _time = time;

            if (_title.Length >= 12)
                _title = $"{_title.Substring(0, 12)}...";
        }

        public void SetCurrentPosition(TimeSpan time)
        {
            _currentTime = time;
            
            var current = time.TotalSeconds;
            var total = 58 / _time.TotalSeconds;
            
            //TotalPosition = (int)((_time.TotalSeconds / 100) * time.TotalSeconds);
            TotalPosition = (int)(total * current);
        }
        
        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine($":.\t{_title}\t{new string(' ', 30)}\t\t{_currentTime.Minutes}:{_currentTime.Seconds} | {_time.Minutes}:{_time.Seconds}");
            Console.WriteLine($":.\t|{new string('-', TotalPosition)}>{new string('.', 58 - TotalPosition)}|");
        }
    }
}