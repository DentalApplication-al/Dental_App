﻿
using DentalDomain.Files;

namespace DentalApplication.User.ClientController.DTO
{
    public class FileResponse
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? link { get; set; }
        public int? size { get; set; }
        public string? unit { get; set; }
        public string? uploaded_date { get; set; }

    }
}
