using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Quizzish.Models
{
    public class Challenge : IHaveId
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<PlayerSection> PlayerSections { get; set; }
        public string Difficulty { get; set; }
        public Category Category { get; set; }


        public bool IsNotATie() => PlayerSections.Select(ps => ps.Points).Distinct().Count() == 2;

        public void DistributeEndGamePoints()
        {
            PlayerSections = PlayerSections.OrderByDescending(ps => ps.Points).ToList();

            var difference = PlayerSections.FirstOrDefault().Points
                - PlayerSections.LastOrDefault().Points;

            var difficulty = PlayerSections.FirstOrDefault().Challenge.Difficulty;

            var bonusPoints = difficulty == "Easy" ? 1m : difficulty == "Medium" ? 1.07m : 1.15m;

            int points = (int)Math.Round((decimal)PlayerSections.FirstOrDefault().Player.Scores.FirstOrDefault(sc => sc.Category == Category).Amount / 100 * difference * bonusPoints);

            PlayerSections.First().Player.Scores.FirstOrDefault(sc => sc.Category == this.Category).Amount += points;
            PlayerSections.LastOrDefault().Player.Scores.FirstOrDefault(sc => sc.Category == Category).Amount -= points;
        }

        public bool GameOverOnCurrentUserSide(int playerCounter, int answerId) => playerCounter >= 10 
            || Questions.Select(q => q.Id).ElementAtOrDefault(FindQuestionId(answerId)) > playerCounter;
        public int FindQuestionId(int answerId) => Questions.FirstOrDefault(q => q.Answers.Select(a => a.Id).Contains(answerId)).Id;
        public bool GameOverOnBothSides() => PlayerSections.Select(ps => ps.Counter).All(n => n >= 10);

        public void ProcessUserAnswer(PlayerSection playerSection, Answer answer)
        {
            playerSection.ProcessUserAnswer(answer);

            if (GameOverOnBothSides() && IsNotATie())
            {
                DistributeEndGamePoints();
            }
        }
    }
}
