using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalApplication.Errors
{
    public class BadRequestException : Exception
    {
        public List<string> Errors { get; set; } = new();
        public BadRequestException(string error)
        {
            Errors.Add(error);
        }
    }
}
