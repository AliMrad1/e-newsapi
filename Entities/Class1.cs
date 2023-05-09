using System.ComponentModel.DataAnnotations;

namespace Entities
{

    //all classes

    public class Category
    {
        public int id { set; get; }
        public string name { set; get; }
        public string img_url { set; get; }
    }

    public class News
    { 
        public int id { set; get; }
        public string title { set; get; }
        public string img_url { set; get;}
        public string short_desc { set; get; }
        public string long_desc { set; get;}
        public Category category { set; get; }
        public string created_at { set; get; }
        public string author { set; get;}
    }

    public class Role
    {
        public int id { set; get; }
        public string role_name { set; get; }
    }

    public class User
    {
        public int id { set; get; }
        public string fullname { set; get; }
        public string dateofbirth { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public Role role { set; get; }
    }

    // all records

    public record NewsRequest(string title, string short_desc, string long_desc, string? img_url, CategoryRequest category, string author) { }

    public record CategoryRequest(int id) { }

    public record AddNewsResponse(string response) { }

    public record RegistrationBodyResponse(string res_message) { }

    public record RegistrationBodyRequest(

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 characters",ErrorMessageResourceName =null,ErrorMessageResourceType =null, MinimumLength =2)]
        string fullname,

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        string dateofbirth,

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email must be between 11 and 50 characters",ErrorMessageResourceName =null,ErrorMessageResourceType =null, MinimumLength =10 )]
        string email,

        [Required(ErrorMessage = "password is required")]
        [StringLength(50, ErrorMessage = "Password must be between 8 and 50 characters",ErrorMessageResourceName =null,ErrorMessageResourceType =null, MinimumLength =8 )]
        string password,

        [Required(ErrorMessage = "role is required")]
        Role role)
    { }
}