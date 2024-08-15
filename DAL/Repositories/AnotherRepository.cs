using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class AnotherRepository: IAnotherRepository
    {

        private readonly IConfiguration _config;
        public AnotherRepository(IConfiguration config)
        {
            _config = config;
        }


        public string GetConnectionString()
        {
            return _config.GetConnectionString("NorthWindDb")!.ToString();
        }
    }
}
