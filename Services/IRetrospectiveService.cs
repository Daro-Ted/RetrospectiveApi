using RetrospectiveApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.Services
{
    public interface IRetrospectiveService
    {
        bool ProjectNameExist(string name);
        bool ProjectExist(int id);
        ProjectDto GetProject(int id, bool IncludeFeedback);
        IEnumerable<ProjectDto> GetProjects();
        IEnumerable<ProjectDto> GetProjectByDate(DateTime date);
        void AddProject(ProjectDto project);
        void AddFeedback(int projectId, FeedbackDto feedbacks);
        bool Save();
    }
}
