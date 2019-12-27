using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetrospectiveApi.Entities;

namespace RetrospectiveApi.Repositories
{
    public class RetrospectiveRepository : IRetrospectiveRepository
    {
        private readonly ProjectDBContext _context;

        public RetrospectiveRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public void AddFeedback(int projectId, Feedback feedbacks)
        {
            var project = GetProject(projectId, false);
            project.Feedbacks.Add(feedbacks);
        }

        public void AddProject(RetropsectiveProject project)
        {
            _context.RetropsectiveProjects.Add(project);
        }

        public RetropsectiveProject GetProject(int projectId, bool IncludeFeedback)
        {
            if (IncludeFeedback)
            {
                return _context.RetropsectiveProjects
                    .Include(p => p.Feedbacks).FirstOrDefault(p => p.Id == projectId);
            }

            return _context.RetropsectiveProjects.FirstOrDefault(p => p.Id == projectId);
        }

        public IEnumerable<RetropsectiveProject> GetProjectByDate(DateTime date)
        {
            return _context.RetropsectiveProjects
                .Include( f=>f.Feedbacks)
                .Where(p => p.Date.Date == date.Date)
                .ToList();
        }

        public IEnumerable<RetropsectiveProject> GetProjects()
        {
            return _context.RetropsectiveProjects.Include(f => f.Feedbacks)
                .OrderBy(p => p.Id).ToList();
        }

        public bool ProjectExist(int id)
        {
            return _context.RetropsectiveProjects.Any(p => p.Id == id);
        }

        public bool ProjectNameExist(string name)
        {
            return _context.RetropsectiveProjects.Any(p => p.Name == name);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
