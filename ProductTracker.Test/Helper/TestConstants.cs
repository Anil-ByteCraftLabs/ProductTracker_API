﻿using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace ProductTracker.Test.Helper
{
    public static class TestConstants
    {
        public static SqlException GetSqlException()
        {
            var sqlException = FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;

            return sqlException;
        }

        public static Exception GetGeneralException()
        {
            return new Exception("Test Exception");
        }

        public static class ContactTest
        {
            public static string FirstName = "Sandeep";
            public static string LastName = "Kumar";
            public static string Email = "stest@gmail.com";
            public static string PhoneNumber = "9845623117";
            public static string NewEmail = "stest01@gmail.com";
        }
    }
}
