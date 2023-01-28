using SQLite;
using System;

namespace C971.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        
        public DateTime AssessStart { get; set; }
        public DateTime AssessEnd { get; set; }
        public bool StartNotification { get; set; }
    }
}
