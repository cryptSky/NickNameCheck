namespace NickNameCheck
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [MinLength(1)]
        [Required]
        public String Name { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(255)]
        [MinLength(1)]
        [Required]
        public String NickName { get; set; }

    }

    public class UserDB : DbContext
    {
        public UserDB()
            : base("name=UserDB")
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class NickNameAvailableBindingModel
    {
        public string NickName { get; set; }
    }

}

