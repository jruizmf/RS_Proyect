
using System;

namespace RS_BussinessLogic.models.dto
{
    public class TokenResultDto
    {
        public Guid Id { get; set; }
        public string Usuario { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }

    }
}
