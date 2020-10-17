using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext compensationContext)
        {
            _employeeContext = compensationContext;
            _logger = logger;
        }

        public List<Compensation> GetById(string id)
        {
            return _employeeContext.Compensation.Include(i => i.employee).Where(w => w.employee.EmployeeId == id).ToList();
        }

        public Compensation Create(Compensation compensation)
        {
            Employee employee = _employeeContext.Employees.Include(e => e.DirectReports).FirstOrDefault(e => e.EmployeeId == compensation.employee.EmployeeId);
            compensation.employee = employee;
            _employeeContext.Compensation.Add(compensation);

            return compensation;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
