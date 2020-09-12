﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DogKeepers.Shared.DTOs
{
    public class UserDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }

    }
}
