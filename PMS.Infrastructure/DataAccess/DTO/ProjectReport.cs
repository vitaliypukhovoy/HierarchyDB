using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class ProjectReport
    {
        [BindRequired]
        public string startDate { get; set; }

        [BindRequired]
        public string finishDate { get; set; }
    }
}
