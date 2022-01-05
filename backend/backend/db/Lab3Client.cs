using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.db
{
    public class Lab3Client
    {
        private ConfigurationBuilder _builder { get; set; }
        private string _configPath = "D:\\Studies\\Web-programming\\lab3\\backend\\backend\\db\\";
        private IConfiguration _config { get; set; }
        private string _connectionString { get; set; }
        private DbContextOptionsBuilder<Lab3Context> _optionsBuilder { get; set; }
        private DbContextOptions<Lab3Context> _options { get; set; }
        protected Lab3Context context { get; set; }

        public Lab3Client()
        {
            _builder = new ConfigurationBuilder();
            _builder.SetBasePath(_configPath);
            _builder.AddJsonFile("db-config.json");

            _config = _builder.Build();

            _connectionString = _config.GetConnectionString("DefaultConnection");

            _optionsBuilder = new DbContextOptionsBuilder<Lab3Context>();

            _options = _optionsBuilder.UseSqlServer(_connectionString).Options;

            context = new Lab3Context(_options);
        }
    }
}
