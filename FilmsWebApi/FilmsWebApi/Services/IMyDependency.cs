using FilmsWebApi.Models;

namespace FilmsWebApi.Services
{
    public interface IMyDependency
    {
        public FilmsDbContext GetContext();
    }
}
