using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Services.Helpers
{
    public class Enums
    {
        public enum StatusCode
        {
            Ok = 200,
            BadRequest = 400,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            InternalServerError = 500
        }
        public enum Role
        {
            Owner = 1,
            Manager = 2,
            AssistantManager = 3,
            Staff = 4
        }
        public enum OrderType
        {
            Wating = 1,
            Payment = 2,
            Reject = 3

        }
        public enum PromotionType
        {
            Baht = 1,
            Percent = 2
        }
    }
}
