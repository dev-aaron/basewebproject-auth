﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWebProject.Identity.Dapper;

namespace BaseWebProject.Dal
{
    public class ApplicationDbContext : DbManager
    {
        public ApplicationDbContext(string connectionName)
            : base(connectionName)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
