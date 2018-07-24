﻿namespace PhotoShare.Models
{
    using PhotoShare.Models.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Validation;

    public class User
    {
        public User()
        {
            this.FriendsAdded = new HashSet<Friendship>();
            this.AddedAsFriendBy = new HashSet<Friendship>();
            this.AlbumRoles = new HashSet<AlbumRole>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        [Modify]
        [Password(6, 50, ContainsDigit = true, ContainsLowercase = true, ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        [Email]
        [Modify]
        public string Email { get; set; }

        [Modify]
        public int? ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }

        [Modify]
        public string FirstName { get; set; }

        [Modify]
        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public int? BornTownId { get; set; }
        [Modify]
        public Town BornTown { get; set; }

        public int? CurrentTownId { get; set; }
        [Modify]
        public Town CurrentTown { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Age]
        [Modify]
        public int? Age { get; set; }

        [Modify]
        public bool? IsDeleted { get; set; }

        public ICollection<Friendship> FriendsAdded { get; set; }

        public ICollection<Friendship> AddedAsFriendBy { get; set; }

        public ICollection<AlbumRole> AlbumRoles { get; set; }

        public override string ToString()
        {
            return $"{this.Username} {this.Email} {this.Age} {this.FullName}";
        }
    }
}
