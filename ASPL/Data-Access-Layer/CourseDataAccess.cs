﻿using System.Data;
using Business_Logic_Layer.Interfaces;

namespace Data_Access_Layer
{
    public class CourseDataAccess: ICourseDataAccess
    {
        private DataAccess dataAccess;

        public CourseDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public DataTable GetCourses()
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM courses");
            return data;
        }

        public DataTable GetChapters(int course)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM chapters WHERE course_id = {course}");
            return data;
        }
    }
}