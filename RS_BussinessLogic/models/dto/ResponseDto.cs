using System;
using System.Collections.Generic;
using System.Text;

namespace RS_BussinessLogic.models.dto
{
    public class ReponseDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }
}
