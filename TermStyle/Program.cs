using System;
using System.Collections.Generic;
using System.Linq;
using Yandex.Music.Api;
using Yandex.Music.Api.Models;

namespace TermStyle
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new YandexMusicApi();

            var login = new LoginPanel(api);
            login.Login();
            
            Console.WriteLine("Extracting your favorites...");
            var tracks = api.GetListFavorites();
            var index = 1;
            var tableData = new List<string[]>();
            var border = 9;
            
            tracks.ForEach(x =>
            {
                var list = new string[4];

                var title = x.Title;
                if (title.Length >= border)
                    title = $"{title.Substring(0, border)}...";
                
                var artist = x.Artists.First().Name;
                if (artist.Length >= border)
                    artist = $"{artist.Substring(0, border)}...";
                var size = x.FileSize / 1024;
                
                list[0] = index.ToString();
                list[1] = title;
                list[2] = artist;
                list[3] = size.ToString();
                
                tableData.Add(list);
                
                index++;
            });

            Console.Clear();
            var table = new Table();
            table.SetHeader("#", "TITLE", "ARTIST", "SIZE");
            table.SetData(tableData);
            table.Show();

            var audio = new AudioPlayer();
            audio.SetTrack("Can not sleep", new TimeSpan(0, 0, 2, 34));
            audio.SetCurrentPosition(new TimeSpan(0, 0, 0, 34));
            audio.Show();
            Console.WriteLine();
            
            Console.Write(":: Send command: ");
            Console.ReadLine();
        }
    }
}