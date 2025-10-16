using System.Collections.Generic;
using System.Linq;
using Models.Dto.Ticket;
using Models.Dto.Ticket.Models.Dto.Tickets;
using Models.Dto.Dashboard;

namespace Services.IssueManagement
{
    public interface ITicketReferenceService
    {
        List<TicketDto> GetTickets();
        List<DepartmentDto> GetDepartments();
        List<TicketTypeDto> GetTicketTypes();
        List<RootCauseDto> GetRootCauses();
        List<RelocationDto> GetRelocations();
        List<CustomerDto> GetCustomers();
        List<ProjectDto> GetProjects();
        List<UserDto> GetUsers();
        List<SubformDto> GetSubforms(int ticketTypeId);
    }
    public class TicketReferenceService : ITicketReferenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketSeed(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<TicketDto> GetTickets()
        {
            return _unitOfWork.TicketRepository.GetAll()
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    // Map other properties as needed
                })
                .ToList();
        }

        public List<DepartmentDto> GetDepartments()
        {
            return _unitOfWork.DepartmentRepository.GetAll()
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();
        }

        public List<TicketTypeDto> GetTicketTypes()
        {
            return _unitOfWork.TicketTypeRepository.GetAll()
                .Select(tt => new TicketTypeDto
                {
                    Id = tt.Id,
                    Name = tt.Name
                })
                .ToList();
        }

        public List<RootCauseDto> GetRootCauses()
        {
            return _unitOfWork.RootCauseRepository.GetAll()
                .Select(rc => new RootCauseDto
                {
                    Id = rc.Id,
                    Name = rc.Name
                })
                .ToList();
        }

        public List<RelocationDto> GetRelocations()
        {
            return _unitOfWork.RelocationRepository.GetAll()
                .Select(r => new RelocationDto
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();
        }

        public List<CustomerDto> GetCustomers()
        {
            return _unitOfWork.CustomerRepository.GetAll()
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public List<ProjectDto> GetProjects()
        {
            return _unitOfWork.ProjectRepository.GetAll()
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }

        public List<UserDto> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll()
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name
                })
                .ToList();
        }

        public List<SubformDto> GetSubforms(int ticketTypeId)
        {
            return _unitOfWork.SubformRepository.GetByTicketTypeId(ticketTypeId)
                .Select(s => new SubformDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();
        }
    }
}