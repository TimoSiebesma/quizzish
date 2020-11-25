using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Models
{


    public enum Category 
    {
        [Display(Name = "General Knowledge")] GeneralKnowledge = 9,
        [Display(Name = "Entertainment: Books")] EntertainmentBooks,
        [Display(Name = "Entertainment: Film")] EntertainmentFilm,
        [Display(Name = "Entertainment: Music")] EntertainmentMusic,
        [Display(Name = "Entertainment: Musicals & Theatres")] EntertainmentMusicalsTheatres,
        [Display(Name = "Entertainment: Television")] EntertainmentTelevision,
        [Display(Name = "Entertainment: Video Games")] EntertainmentVideoGames,
        [Display(Name = "Entertainment: Board Games")] EntertainmentBoardGames,
        [Display(Name = "Science & Nature")] ScienceNature,
        [Display(Name = "Science: Computers")] ScienceComputers,
        [Display(Name = "Science: Mathematics")] ScienceMathematics,
        [Display(Name = "Mythology")] Mythology,
        [Display(Name = "Sports")] Sports,
        [Display(Name = "Geography")] Geography,
        [Display(Name = "History")] History,
        [Display(Name = "Politics")] Politics,
        [Display(Name = "Art")] Art,
        [Display(Name = "Celebrities")] Celebrities,
        [Display(Name = "Animals")] Animals,
        [Display(Name = "Vehicles")] Vehicles,
        [Display(Name = "Entertainment: Comics")] EntertainmentComics,
        [Display(Name = "Science: Gadgets")] ScienceGadgets,
        [Display(Name = "Entertainment: Japanese Anime & Manga")] EntertainmentJapaneseAnimeManga,
        [Display(Name = "Entertainment: Cartoon & Animations")] EntertainmentCartoonAnimations
    }

    public class Question : IHaveId
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ChallengeId { get; set; }
        [ForeignKey("ChallengeId")]
        public virtual Challenge Challenge { get; set; }

        public string Text { get; set; }
        public virtual IEnumerable<Answer> Answers { get; set; }
        

       
       
    }
}
