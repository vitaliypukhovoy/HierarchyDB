using Microsoft.SqlServer.Types;
using System;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class Projects
    {
        public int p_id { get; set; }
        public int p_code { get; set; }
        public int p_mgrid { get; set; }
        public SqlHierarchyId p_hid { get; set; }
        public string p_name { get; set; }
        public Int16 p_lvl { get; set; }
        public DateTime p_startdate { get; set; }
        public DateTime p_finishdate { get; set; }
    }
}


