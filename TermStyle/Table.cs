using System;
using System.Collections.Generic;
using System.Linq;

namespace TermStyle
{
    public class Table
    {
        public List<string> Header { get; set; }
        public List<string[]> Data { get; set; }

        public Table()
        {
            Header = new List<string>();
            Data = new List<string[]>();
        }

        public void SetHeader(params string[] header)
        {
            Header.AddRange(header);
        }

        public void Show()
        {
            ShowRow(Header);

            Data.ForEach(x => ShowRow(x.ToList()));
        }

        private void ShowRow(List<string> rows)
        {
            var str = "";
            rows.ForEach(x =>
            {
                str += $"{x}\t\t\t\t";
            });
            str = str.Substring(0, str.Length - 2);
            Console.WriteLine(str);
        }

        public void SetData(List<string[]> datas)
        {
            Data.AddRange(datas);
        }
    }
}