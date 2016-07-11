using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TaskManagerProject.Domain.Entities;

namespace TaskManagerProjectApp.Models
{
        public class RegisterViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginViewModel
        {
            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class ForgotPasswordViewModel
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name ="Email")]
            public string Email { get; set; }
        }

        public class ResetPasswordViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
    }

    public class EditUserViewModel
    {
        public int ID { get; set; }
        public List<int> TaskIds { get; set; }
    }

    public class ChangeStatusViewModel
    {
        public StatusOfTask newStatus { get; set; }
        public int TaskId { get; set; }
    }

    public class PaginationUserViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public static PaginationUserViewModel CreateFromModel(Project project)
        {
            return new PaginationUserViewModel
            {
                ID = project.ID,
                CustomerName = project.Customer.Name,
                Name = project.Name,
                IsActive = project.IsActive,
                DateCreated = project.DateCreated
            };
        }
    }


}