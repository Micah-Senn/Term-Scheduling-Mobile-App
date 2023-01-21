using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public string InstName { get; set; }
        public string InstPhone { get; set; }
        public string InstEmail { get; set; }
        public string Notes { get; set; }
        public bool StartNotification { get; set; }
    }
}
