using System;
using Yandex.Music.Api.Models;

namespace TermStyle
{
    public class AudioPlayer
    {
        private string _title;
        private TimeSpan _time;
        private int TotalPosition = 0;
        
        public AudioPlayer()
        {
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
            var current = time.TotalSeconds;
            var total = 58 / _time.TotalSeconds;
            
            //TotalPosition = (int)((_time.TotalSeconds / 100) * time.TotalSeconds);
            TotalPosition = (int)(total * current);
        }
        
        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine($":.\t{_title}\t{new string(' ', 30)}\t\t{_time.Minutes}:{_time.Seconds}");
            Console.WriteLine($":.\t|{new string('-', TotalPosition)}>{new string('.', 58 - TotalPosition)}|");
        }
    }
}