using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetrospectiveApi.Entities;
using RetrospectiveApi.Models;
using RetrospectiveApi.Repositories;

namespace RetrospectiveApi.Services
{
    public class RetrospectiveService : IRetrospectiveService
    {
        private readonly IRetrospectiveRepository _context;

        public RetrospectiveService(IRetrospectiveRepository context)
        {
            _context = context;
        }
        public void AddFeedback(int projectId, FeedbackDto feedbacks)
        {
            var map = AutoMapper.Mapper.Map<Feedback>(feedbacks);
            _context.AddFeedback(projectId, map);
        }

        public void AddProject(ProjectDto project)
        {
            var map = AutoMapper.Mapper.Map<RetropsectiveProject>(project);
            _context.AddProject(map);
        }

        public ProjectDto GetProject(int id, bool IncludeFeedback)
        {
            var result = _context.GetProject(id,IncludeFeedback);
            return AutoMapper.Mapper.Map<ProjectDto>(result);
        }

        public IEnumerable<ProjectDto> GetProjectByDate(DateTime date)
        {
            var result = _context.GetProjectByDate(date);
            return AutoMapper.Mapper.Map<IEnumerable<ProjectDto>>(result);
        }

        public IEnumerable<ProjectDto> GetProjects()
        {
            var result = _context.GetProjects();
            return AutoMapper.Mapper.Map<IEnumerable<ProjectDto>>(result);
        }

        public bool ProjectExist(int id)
        {
            return _context.ProjectExist(id);
        }

        public bool ProjectNameExist(string name)
        {
            return _context.ProjectNameExist(name);
        }

        public bool Save()
        {
            return _context.Save();
        }
    }
}
