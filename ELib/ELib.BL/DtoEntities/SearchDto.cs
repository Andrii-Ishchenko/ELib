﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.DtoEntities
{
    public class SearchDto
    {
        public string Query { get; private set; }
        public string Title { get; private set; }
        public string AuthorName { get; private set; }
        public string Genre { get; private set; }
        public string Subgenre { get; private set; }
        public string Publisher { get; private set; }
        public int Year { get; private set; }

        public SearchDto(string query, string authorName, string title,
                         string publisher, string genre, string subgenre, int year) {
            Query = query;
            Title = title;
            AuthorName = authorName;
            Genre = genre;
            Subgenre = subgenre;
            Publisher = publisher;
            Year = year;
        }
    }
}