using System;
using System.ComponentModel.DataAnnotations;

namespace authorizationcw.Models
{
    public class GameModel
    {
        [Key]
        public int id {get; set;}

        [Required(ErrorMessage = "Game Title is a required field of 100 characters or less.")]
        [StringLength(100)]
        [Display(Name = "Game Title")]
        public string Title {get; set;}

        [Required(ErrorMessage = "Game Description is a required field.")]
        [Display(Name = "Game Description")]
        public string Description {get; set;}

        [Required(ErrorMessage = "Game Publisher is a required field.")]
        [Display(Name = "Game Publisher")]
        public string Publisher {get; set;}

        [Range(1,5,ErrorMessage = "Game Rating requires a number between 1 and 5.")]
        [Display(Name = "Game Rating")]
        public int Rating {get; set;}
    }
}