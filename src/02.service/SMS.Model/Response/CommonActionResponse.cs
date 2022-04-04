using System;

namespace SMS.Model.Response
{
    public class CommonActionResponse
    {
        public CommonActionResponse(Guid id, ActionStatusEnum status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; set; }

        public ActionStatusEnum Status { get; set; }
    }
}