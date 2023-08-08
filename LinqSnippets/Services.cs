using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using university_api_backend.Models.DataModels;

namespace LinqSnippets
{
    public class Services
    {
        public static IEnumerable<Users> SearchUserByEmail(string email)
        {
            List<Users> user = new ();

            var userByEmail = user.Where(user => user.Email == email).ToList();

            return userByEmail;

        }

        public static IEnumerable<Student> SearchStudentMoreThan18()
        {
            List<Student> student = new ();

            DateTime actualDate = DateTime.Today;
            var studentMoreThan18 = student.Where(student => (actualDate - student.DateOfBirth).TotalDays / 365 >= 18).ToList();

            return studentMoreThan18;
        }

        public static IEnumerable<Student> SearchStudentsWithOneCourseAtLeast()
        {
            List<Student> student = new ();

            var studentWithOneCourseAtLeast = student.Where(course => course.Courses.Any()).ToList();

            return studentWithOneCourseAtLeast;
        }

        public static IEnumerable<Course> SearchCoursesWithOneStudentAtLeast()
        {
            List<Course> course = new();

            var thisCourseHasStudent = course.Where(course => course.Level == Level.Basic && course.Students.Any()).ToList();

            return thisCourseHasStudent;
        }

        public static IEnumerable<Course> SearchCourseWithLevelDeterminated()
        {
            List<Course> course = new();

            var courseWithLevelDeterminated = course.Where(course => course.Level == Level.Medium && course.Categories.Any(cat => cat.Name == "Matematicas")).ToList();

            return courseWithLevelDeterminated;
        }

        public static IEnumerable<Course> SearchCourseWithoutStudents()
        {
            List<Course> course = new();

            var courseWithoutStudent = course.Where(course => !course.Students.Any()).ToList();

            return courseWithoutStudent;
        }


    }
}
