using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public bool StartNotification { get; set; }
    }
}
