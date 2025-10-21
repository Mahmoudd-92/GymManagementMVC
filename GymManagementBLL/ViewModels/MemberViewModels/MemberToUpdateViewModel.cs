﻿using System.ComponentModel.DataAnnotations;

namespace GymManagementBLL.ViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!;

        public string? Photo { get; set; }

        [Required(ErrorMessage = "Email Is Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Number Is Required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [RegularExpression(@"^01[0125]\d{8}", ErrorMessage = "Phone Number Must Be Valid Egyptian Phone Number.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Building Number Is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Building Number must be greater than 0")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City Is Required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Street Is Required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 150 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street can only contain letters, numbers and spaces.")]
        public string Street { get; set; } = null!;
    }
}
