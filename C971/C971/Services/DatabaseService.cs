using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using C971.Models;
using Xamarin.Essentials;

namespace C971.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbConnection;
        static async Task Init()
        {
            if (_db != null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Terms.db");

            _db = new SQLiteAsyncConnection(databasePath);
            _dbConnection = new SQLiteConnection(databasePath);

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Assessment>();


        }
        #region Term Methods

        public static async Task AddTerm(string title, DateTime termStart, DateTime termEnd)
        {
            await Init();
            var term = new Term()
            {
                Title = title,
                TermStart = termStart,
                TermEnd = termEnd,
            };

            await _db.InsertAsync(term);
            var id = term.Id;
        }
        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();

            var terms = await _db.Table<Term>().ToListAsync();
            return terms;
        }
        public static async Task RemoveTerm(int id)
        {
            await Init();

            await _db.DeleteAsync<Term>(id);
        }
        public static async Task UpdateTerm(int id, string title, DateTime termStart, DateTime termEnd)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.Title = title;
                termQuery.TermStart = termStart;
                termQuery.TermEnd = termEnd;

                await _db.UpdateAsync(termQuery);
            }
        }
        #endregion

        #region Course Methods
        public static async Task AddCourse(int termId, string name, string status, bool startNotification, DateTime courseStart, DateTime courseEnd, string notes, 
            string instName,  string instPhone, string instEmail)
        {
            await Init();
            var course = new Course
            {
                TermId = termId,
                Name = name,
                Status = status,
                StartNotification = startNotification,
                CourseStart = courseStart,
                CourseEnd = courseEnd,
                Notes = notes,
                InstName = instName,
                InstPhone = instPhone,
                InstEmail = instEmail
            };

            await _db.InsertAsync(course);

            var id = course.Id;
        }
        public static async Task RemoveCourse(int id)
        {
            await Init();

            await _db.DeleteAsync<Course>(id);
        }
        public static async Task<IEnumerable<Course>> GetCourses(int termId)
        {
            var courses = await _db.Table<Course>().Where(i => i.TermId == termId).ToListAsync();

            return courses;
        }
        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await _db.Table<Course>().ToListAsync();

            return courses;
        }
        public static async Task UpdateCourse(int id, int termId, string name, string status, bool startNotification, DateTime courseStart, DateTime courseEnd, string notes,
            string instName, string instPhone, string instEmail)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.TermId = termId;
                courseQuery.Name = name;
                courseQuery.Status = status;
                courseQuery.StartNotification = startNotification;
                courseQuery.CourseStart = courseStart;
                courseQuery.CourseEnd = courseEnd;
                courseQuery.Notes = notes;
                courseQuery.InstName = instName;
                courseQuery.InstPhone = instPhone;
                courseQuery.InstEmail = instEmail;

                await _db.UpdateAsync(courseQuery);
            }
        }
        #endregion

        #region Assessment Methods

        public static async Task AddAssessment(int courseId, string name, string type, bool startNotification, DateTime assessStart, DateTime assessEnd)
        {
            await Init();
            var assessment = new Assessment
            {
               CourseId = courseId,
               Name = name,
               Type = type,
               StartNotification = startNotification,
               AssessStart = assessStart,
               AssessEnd = assessEnd
            };

            await _db.InsertAsync(assessment);

            var id = assessment.Id;
        }
        public static async Task RemoveAssessment(int id)
        {
            await Init();

            await _db.DeleteAsync<Assessment>(id);
        }
        public static async Task<IEnumerable<Assessment>> GetAssessments(int courseId)
        {
            var assessments = await _db.Table<Assessment>().Where(i => i.CourseId == courseId).ToListAsync();

            return assessments;
        }

        public static async Task<IEnumerable<Assessment>> GetAssessment()
        {
            await Init();

            var assessments = await _db.Table<Assessment>().ToListAsync();

            return assessments;
        }

        public static async Task UpdateAssessment(int id, int courseId, string name, string type, bool startNotification, DateTime assessStart, DateTime assessEnd)
        {
            await Init();

            var assessmentQuery = await _db.Table<Assessment>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (assessmentQuery != null)
            {
                assessmentQuery.CourseId = courseId;
                assessmentQuery.Name = name;
                assessmentQuery.Type = type;
                assessmentQuery.StartNotification = startNotification;
                assessmentQuery.AssessStart = assessStart;
                assessmentQuery.AssessEnd = assessEnd;

                await _db.UpdateAsync(assessmentQuery);
            }
        }

        #endregion

        #region SampleData
        public static async void LoadSampleData()
        {
            await Init();

            Term term = new Term
            {
                Title = "Term 1",
                TermStart = DateTime.Today.Date,
                TermEnd = DateTime.Today.Date
            };
            await _db.InsertAsync(term);
            Course course = new Course
            {
                TermId = term.Id,
                Name = "Mobile App Development",
                Status = "Active",
                StartNotification = true,
                CourseStart = DateTime.Today.Date,
                CourseEnd = DateTime.Today.Date,
                InstName = "Micah Senn",
                InstEmail = "msenn3@wgu.edu",
                InstPhone = "334-733-3343"
            };
            await _db.InsertAsync(course);
            Assessment assessment = new Assessment
            {
                CourseId = course.Id,
                Name = "Task 1",
                Type = "Objective Assessment",
                StartNotification = true,
                AssessStart = DateTime.Today.Date,
                AssessEnd = DateTime.Today.Date
            };
            await _db.InsertAsync(assessment);
            Assessment assessment2 = new Assessment
            {
                CourseId = course.Id,
                Name = "Task 2",
                Type = "Performance Assessment",
                StartNotification = true,
                AssessStart = DateTime.Today.Date,
                AssessEnd = DateTime.Today.Date
            };
            await _db.InsertAsync(assessment2);
        }
        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Term>();
            await _db.DropTableAsync<Course>();
            await _db.DropTableAsync<Assessment>();
            _db = null;
            _dbConnection = null;
        }
        #endregion

        #region Count Determinations
        public static async Task<int> GetCourseCountAsync(int selectedTermId)
        {
            int courseCount = await _db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Course WHERE TermId = ?", selectedTermId);

            return courseCount;
        }

        public static async Task<int> GetAssessmentCountAsync(int selectedCourseId)
        {
            int assessmentCount = await _db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Assessment WHERE CourseId = ?", selectedCourseId);

            return assessmentCount;
        }
        #endregion

        #region Looping through table records
        public static async void LoopingTermTable()
        {
            await Init();
            var allTermRecords = _dbConnection.Query<Term>("SELECT * FROM Term");

            foreach (var termRecord in allTermRecords)
            {
                Console.WriteLine("Name " + termRecord.Title);
            }
        }

        public static async Task<IEnumerable<Term>> GetNotificationTerms()
        {
            await Init();
            var allTermRecords = _dbConnection.Query<Term>("SELECT * FROM Term");

            return allTermRecords;
        }

        #endregion
    }
}
       