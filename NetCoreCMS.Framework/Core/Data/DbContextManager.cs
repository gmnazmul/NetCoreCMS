﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NetCoreCMS.Framework.Core.Mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetCoreCMS.Framework.Core.Data
{
    public class DbContextManager 
    {
        private static string _sqLiteConString = "Data Source={0}\\NetCoreCMS.Web.db;Version=3;";
        private static string _sqlLocalDb = "Server=(localdb)\\mssqllocaldb;Database=NetCoreCMS.Web.db;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static string _mySqlConString = "server={0};port={1};database={2};userid={3};pwd={4};sslmode=none;";
        private static string _msSqlConString = "Data Source={0}; Initial Catalog={1}; User Id = {2}; Password = {3}; MultipleActiveResultSets=true";
        private static string _pgSqlConString = "Host={0}; Port={1}; Database={2}; User ID={3}; Password={4}; Pooling=true;";

        public static string GetConnectionString(IHostingEnvironment env, DatabaseEngine engine, string server, string port, string database, string username, string password)
        {
            switch (engine)
            {
                case DatabaseEngine.MsSql:
                    if (string.IsNullOrEmpty(port))
                        return string.Format(_msSqlConString, server, database, username, password);
                    else
                        return string.Format(_msSqlConString, server+","+port, database, username, password);

                case DatabaseEngine.MsSqlLocalStorage:
                    return _sqlLocalDb;

                case DatabaseEngine.MySql:
                    if (string.IsNullOrEmpty(port))
                        return string.Format(_mySqlConString, server, "3306", database, username, password);
                    else
                        return string.Format(_mySqlConString, server, port, database, username, password);

                case DatabaseEngine.PgSql:
                    if (string.IsNullOrEmpty(port))
                        return string.Format(_mySqlConString, server, "5432", database, username, password);
                    else
                        return string.Format(_mySqlConString, server, port, database, username, password);
                case DatabaseEngine.SqLite:
                    var path = env.ContentRootPath;
                    return string.Format(_sqLiteConString, Path.Combine(path,"Data"));
                default:
                    return "";

            }
        }

        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityTypes = typeToRegisters.Where(x => x.GetTypeInfo().IsSubclassOf(typeof(BaseModel)) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }
    }
}
