using System;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class MemoryReportTable
    {
        public int p_id { get; set; }
        public int p_code { get; set; }
        public string p_name { get; set; }
        public Int16 p_lvl { get; set; }
        public DateTime p_startdate { get; set; }
        public DateTime p_finishdate { get; set; }
        public string p_state { get; set; }
        public int t_id { get; set; }
        public string t_name { get; set; }
        public string t_description { get; set; }
        public Int16 t_lvl { get; set; }
        public DateTime t_startdate { get; set; }
        public DateTime t_finishdate { get; set; }
        public string t_state { get; set; }
    }
}



