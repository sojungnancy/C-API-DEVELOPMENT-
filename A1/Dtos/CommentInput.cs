using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Dtos
{
    public class CommentInput
    {
        // for holding information entered by the client
        // 5 columns: Id, UserComment, Name, Time, IP
        // Id: automatically assigned by the database when inserted
        // Time: assigned by the server when comment is written
        // IP: server code obtains the user's IP address
        [Required]
        public string UserComment { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
    }
}