﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.DataModels
{
    public class User : IdentityUser
    {
        [Required]
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // One user can have multiple playlists
        // Ref: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
        public ICollection<Playlist> Playlists { get; } = new List<Playlist>();
    }
}