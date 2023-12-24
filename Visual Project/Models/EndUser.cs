using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;

namespace Visual_Project.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class EndUser
    {
        [Key]
        [Required]
        [RegularExpression("[0-9]{14}" ,ErrorMessage = "Please enter 14 digits for Your ID")]
        public string ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,20}$", ErrorMessage = "No numbers allowed in the FirstName.")]
        public string Firstname { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,20}$", ErrorMessage = "No numbers allowed in the LasttName.")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9]+([._-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-.][a-zA-Z0-9]+)*\.[a-z]{2,6}$",
         ErrorMessage="This Email Address not Valid.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z0-9-_.]{3,20}$",
        ErrorMessage = "Numbers are not permitted at the beginning,No space char is permitted ")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_=+{};:'"",.<>/?[\]\\|]).{8,}$", 
        ErrorMessage = "The Password must contains at least one of all of this (lowercase character, uppercase, digit, special character).")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Please enter 11 digits for Your PhoneNumber")]
        public string PhoneNumber { get; set; }
        //[AllowNull] // allows the property to be assigned a null value
        //[Required(AllowEmptyStrings = true)] // ensures that if the value is not null, it cannot be an empty string

        //public string? Image { get; set; }


        //public static int Type(EndUser user)
        //{
        //    using (HotelDbContext dbContext = new HotelDbContext())
        //    {


        //        var is_Guest = dbContext.Guests.Any(guest => guest.Username == user.Username);
        //        var is_Admin = dbContext.Admins.Any(admin => admin.Username == user.Username);
        //        // User is Guest
        //        if (is_Guest)
        //        {
        //            var guest = dbContext.Guests.FirstOrDefault(gst => gst.Username == user.Username);
        //            //if (SecretHasher.HashPassword($"{user.Password}{guest.Salt}") == guest.Password)
        //            if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, guest.Password));
        //            {
        //                return 1;
        //            }
        //        }
        //        // User is Admin
        //        if (is_Admin)
        //        {
        //            var admin = dbContext.Admins.FirstOrDefault(admin => admin.Username == user.Username);
        //            //if (SecretHasher.HashPassword($"{user.Password}{admin.Salt}") == admin.Password)
        //            if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, admin.Password));
        //            {
        //                // Adimn is Receptionist
        //                if (admin.Type == "Receptionist")
        //                    return 2;
        //                else // Admin is Manager
        //                    return 3;
        //            }
        //        }
        //        return 0;
        //    }
        //}
    }
}
