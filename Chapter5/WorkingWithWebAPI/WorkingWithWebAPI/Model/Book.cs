using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWithWebAPI.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
    }
}
