using Microsoft.SqlServer.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class Tasks
    {
        [Required]
        public int t_id { get; set; }
        public int p_id { get; set; }
        public int t_mgrid { get; set; }
        [Required]
        public string t_name { get; set; }
        public string t_description { get; set; }
        public SqlHierarchyId t_hid { get; set; }
        public Int16 t_lvl { get; set; }
        [Required]
        public DateTime t_startdate { get; set; }
        [Required]
        public DateTime t_finishdate { get; set; }
        public string t_state { get; set; }
    }
}


