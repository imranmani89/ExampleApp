using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.WebApp.Repositories
{
    public class BaseRepository
    {
        protected readonly IConfiguration _config;
        public string _connectionString { get; set; }

        public BaseRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("NorthwindDb");
        }

        public string GetStringData()
        {
            return _config.GetSection("NorthwindSettings:firstMessage").Value.ToString();
        }

    }
}
