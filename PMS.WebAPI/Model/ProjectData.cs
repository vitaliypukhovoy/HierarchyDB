using System;

namespace PMS.WebAPI.Model
{
    public class ProjectData
    {
        public int p_code { get; set; }
        public int p_mgrid { get; set; }
        public string p_name { get; set; }
        public string p_startdate { get; set; }
        public string p_finishdate { get; set; }
    }
}
