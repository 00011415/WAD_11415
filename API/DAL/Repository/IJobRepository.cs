using API.Model;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IJobRepository
    {
        void InsertJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(int id);
        Job GetJobById(int Id);
        IEnumerable<Job> GetJobs();
    }
}
