﻿using System;
using System.IO;

namespace golfclubdistanceorganizer.DAL
{
    public static class DatabaseFilePathRetriever
    {
        public static string GetPath()
        {
            const string filename = "gcdoSQLite.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);
            return path;
        }
    }
}