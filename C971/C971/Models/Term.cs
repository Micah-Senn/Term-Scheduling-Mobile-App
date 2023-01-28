using SQLite;
using System;

namespace C971.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }
    }
}
