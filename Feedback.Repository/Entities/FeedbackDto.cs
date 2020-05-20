using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback.Repository.Entities
{

    [Table("Feedback")]
    public class FeedbackDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }


        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string HomePostcode { get; set; }

        [Required]
        [MaxLength(300)]
        public string HomeAddress { get; set; }
        [Required]
        public bool HappyWithLevelOfLighting { get; set; }
         
        [Required]
        public int LevelOfBrightness { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
