using Microsoft.SqlServer.Types;
using System;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class Tasks
    {
        public int t_id { get; set; }
        public int p_id { get; set; }
        public string t_name { get; set; }
        public SqlHierarchyId t_hid { get; set; }
        public Int16 t_lvl { get; set; }
        public DateTime t_startdate { get; set; }
        public DateTime t_finishdate { get; set; }
        public int t_state { get; set; }
    }
}


