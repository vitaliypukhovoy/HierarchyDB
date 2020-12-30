using Microsoft.SqlServer.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class Projects
    {
        [Required]
        public int p_id { get; set; }
        public int p_code { get; set; }
        public int p_mgrid { get; set; }
        public SqlHierarchyId p_hid { get; set; }
        [Required]
        public string p_name { get; set; }
        public Int16 p_lvl { get; set; }
        [Required]
        public DateTime p_startdate { get; set; }
        [Required]
        public DateTime p_finishdate { get; set; }
    }
}


