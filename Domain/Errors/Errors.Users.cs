using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateUsername = Error.Validation(code: "User Already Exist", description: "User Already Exist");
        }
    }
}
