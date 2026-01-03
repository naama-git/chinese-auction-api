using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.DTO
{
    public class UserDTO
    {
        public class SignInDTO
        {
                [MaxLength(100)]
                public string FirstName { get; set; }

                [MaxLength(100)]
                public string LastName { get; set; }

                [EmailAddress]
                public string Email { get; set; }
                public string Password { get; set; }

                [Phone]
                public string PhoneNumber { get; set; }
        }
        public class LogInDTO
        {
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class ResponseUserDTO
        {
            public int Id { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            public string Name { get; set; } = string.Empty;

             public string Token { get; set; }
        }

        public class ReadUserDTO
        {
            public int Id { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            public string Name { get; set; } = string.Empty;

           
        }

    }
}
