using API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    // Job repository with all CRUD logic
    public class JobRepository : IJobRepository
    {
        private JobDbContext _dbContext;

        public JobRepository(JobDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteJob(int jobId)
        {
            var job = _dbContext.Jobs.Find(jobId);
            _dbContext.Jobs.Remove(job);
            Save();
        }

        public Job GetJobById(int jobId)
        {
            var job = _dbContext.Jobs.Find(jobId);
            _dbContext.Entry(job).Reference(s => s.Category).Load();
            return job;
        }

        public IEnumerable<Job> GetJobs()
        {
            return _dbContext.Jobs.Include(s => s.Category).ToList();
        }

        public void InsertJob(Job job)
        {
            job.Category = _dbContext.Categories.Find(job.Category.Id);
            _dbContext.Jobs.Add(job);
            Save();
        }

        public void UpdateJob(Job job)
        {
            job.Category = _dbContext.Categories.Find(job.Category.Id);
            _dbContext.Entry(job).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
