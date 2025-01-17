﻿using StiveBack.Models;
using StiveBack.Ressources.Core;
using System.ComponentModel.DataAnnotations;

namespace StiveBack.Ressources
{
    public class UserUpdateRessource
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public bool? IsAdmin { get; set; }
        public string? Password { get; set; }
        public List<int> RoleIds { get; set; }

        public UserUpdateRessource()
        {
            RoleIds = new List<int>();
        }
    }
}
