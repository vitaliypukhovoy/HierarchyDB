using System;

namespace PMS.WebAPI.Model
{
    public class TaskData
    {
        public int p_id { get; set; }        
        public string t_name { get; set; }        
        public int t_mgrid { get; set; }
        public string t_startdate { get; set; }
        public string t_finishdate { get; set; }
        public int t_state { get; set; }
    }
}
