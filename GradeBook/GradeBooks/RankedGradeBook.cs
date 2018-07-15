using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        string errorMessage = "Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.";

        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            int threshold = (int)Math.Ceiling(Students.Count * 0.20);           

            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();

            if (grades[threshold - 1] <= averageGrade)
                return 'A';
            else if (grades[(threshold * 2) - 1] <= averageGrade)
                return 'B';
            else if (grades[(threshold * 3) - 1] <= averageGrade)
                return 'C';
            else if (grades[(threshold * 4) - 1] <= averageGrade)
                return 'D';
            else
                return 'F';

            //Same thing without Using Linq?
            //TO DO: How to make List.Sort() order Descending??
            //List<double> studentGrades = new List<double>();

            //foreach (Student student in Students)
            //{
            //    studentGrades.Add(student.AverageGrade);
            //}
            //studentGrades.Sort();            
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine(errorMessage);
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine(errorMessage);
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
