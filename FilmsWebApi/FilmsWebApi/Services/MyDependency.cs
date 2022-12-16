using FilmsWebApi.Models;

namespace FilmsWebApi.Services
{
    public class MyDependency:IMyDependency
    {
        public FilmsDbContext GetContext()
        {
            return new FilmsDbContext();
        }
    }
}
