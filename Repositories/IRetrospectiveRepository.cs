using RetrospectiveApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.Repositories
{
    public interface IRetrospectiveRepository
    {
        bool ProjectNameExist(string name);
        bool ProjectExist(int id);
        RetropsectiveProject GetProject(int id, bool IncludeFeedback);
        IEnumerable<RetropsectiveProject> GetProjects();
        IEnumerable<RetropsectiveProject> GetProjectByDate(DateTime date);
        void AddProject(RetropsectiveProject project);
        //void AddProjectWithoutFeedback(RetrospectiveProjectWithOutFeedback projectWithoutFeedback);
        void AddFeedback(int projectId, Feedback feedbacks);
        bool Save();
    }
}
