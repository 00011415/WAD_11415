using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        // GET: api/<JobController>
        [HttpGet]
        public IActionResult Get()
        {
            var jobs = _jobRepository.GetJobs();
            return new OkObjectResult(jobs);
        }

        // GET api/<JobController>/5
        [HttpGet("{id}", Name = "GetJob")]
        public IActionResult Get(int id)
        {
            var job = _jobRepository.GetJobById(id);
            return new OkObjectResult(job);
        }

        // POST api/<JobController>
        [HttpPost]
        public IActionResult Post([FromBody] Job job)
        {
            if (job == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                _jobRepository.InsertJob(job);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = job.Id }, job);
            }
        }

        // PUT api/<JobController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Job job)
        {
            if (job == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                _jobRepository.UpdateJob(job);
                scope.Complete();
                return new OkResult();
            }
        }

        // DELETE api/<JobController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _jobRepository.DeleteJob(id);
            return new OkResult();
        }
    }
}
