using System;
using System.Collections;
using System.Collections.Generic;

namespace PMS.Infrastructure.DataAccess.Model
{
    public class ResponseModel<T>
    {
        public bool ReturnStatus { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable Errors { get; set; }

        public ResponseModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new Hashtable();
        }
    }
}
