using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class ProjectData
    {
        public int p_code { get; set; }
        public int p_mgrid { get; set; }
        [Required]
        public string p_name { get; set; }
        [Required]       
        public string p_startdate { get; set; }
        [Required]       
        public string p_finishdate { get; set; }
    }
}
