using RetrospectiveApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi
{
    public static class SeedDataBase
    {

        public static void EnsureDatabaseSeed(this ProjectDBContext context)
        {
            if (context.RetropsectiveProjects.Any())
            {
                return;
            }

            var retrospectiveProject = new List<RetropsectiveProject>()
            {
                 new RetropsectiveProject()
                 {
                     Name="First Project",
                     Summary="Create an application that tracks all project commemnts and outcomes",
                     Date=DateTime.Now,
                     Participants="Tim Test",
                     Feedbacks = new List<Feedback>()
                     {
                         new Feedback
                         {
                            Name="Bright Tan",
                            Body="Impressive project and outcome was brilliant.",
                            Types="Postive."
                         },
                         new Feedback
                         {
                            Name="Jame Can",
                            Body="I was impressed by project and outcome it was brilliant.",
                            Types="Postive."
                         },
                         new Feedback
                         {
                            Name="Friday Ram",
                            Body="I was impressed by project and outcome it was brilliant, we can improve more to take it to the next level.",
                            Types="Postive."
                         },
                     }
                 },
                 new RetropsectiveProject()
                 {
                     Name="Second Project",
                     Summary="Create an application Produce daily report",
                     Date=DateTime.Now.AddDays(2),
                     Participants="Tim Test, Harry Test",
                     Feedbacks = new List<Feedback>()
                     {
                         new Feedback
                         {
                            Name="Bright Tan",
                            Body="Impressive project and outcome was brilliant.",
                            Types="Postive."
                         },
                         new Feedback
                         {
                            Name="Jame Can",
                            Body="I was impressed by project and outcome it was brilliant.",
                            Types="Postive."
                         },
                         new Feedback
                         {
                            Name="Friday Ram",
                            Body="I was impressed by project and outcome it was brilliant, we can improve more to take it to the next level.",
                            Types="Postive."
                         },
                     }
                 },
                 new RetropsectiveProject()
                 {
                     Name="Third Project",
                     Summary="Create an application that tracks all Parts and Inventory",
                     Date=DateTime.Now.AddDays(4),
                     Participants="Tim Ogbe, Test Obi",
                     Feedbacks = new List<Feedback>()
                     {
                         new Feedback
                         {
                            Name="Bright Tan",
                            Body="Impressive project and outcome was brilliant.",
                            Types="Postive."
                         },
                         new Feedback
                         {
                            Name="Jame Can",
                            Body="I was impressed by project and outcome it was brilliant.",
                            Types="Postive."
                         },
                         new Feedback
                         {
                            Name="Friday Ram",
                            Body="I was impressed by project and outcome it was brilliant, we can improve more to take it to the next level.",
                            Types="Postive."
                         },
                     }
                 },
                  new RetropsectiveProject()
                 {
                     Name="Fourth Project",
                     Summary="Create an application that tracks all Parts and Inventory",
                     Date=DateTime.Now.AddDays(4),
                     Participants="Tim Test, Harry Chan",
                     Feedbacks = new List<Feedback>()
                 }
            };
            context.RetropsectiveProjects.AddRange(retrospectiveProject);
            context.SaveChangesAsync();
        }
        
    }
}
