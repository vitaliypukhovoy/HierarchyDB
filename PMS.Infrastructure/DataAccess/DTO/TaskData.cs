using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class TaskData
    {
        [Required]
        public int p_id { get; set; }        
        public string t_name { get; set; }        
        public int t_mgrid { get; set; }
        [Required]
        public string t_startdate { get; set; }
        [Required]
        public string t_finishdate { get; set; }
        public int t_state { get; set; }
    }
}
