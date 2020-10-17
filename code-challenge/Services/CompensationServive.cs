using challenge.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;

namespace challenge.Services
{
    public class CompensationServive : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationServive> _logger;

        public CompensationServive(ILogger<CompensationServive> logger, ICompensationRepository employeeRepository)
        {
            _compensationRepository = employeeRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Create(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public List<Compensation> GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id).ToList();
            }

            return null;
        }

    }
}
