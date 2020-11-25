using AutoMapper;
using Quizzish.DTO_s;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Quizzish.Data.ApiCaller;

namespace Quizzish.Profiles
{
    public class CustomMapper
    {
        private IMapper _mapper;

        public CustomMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Question> MapApiResultsIntoQuestions(IEnumerable<Result> results)
        {
            return results.Select(result => new Question 
            { 
                Text = result.question,
                Answers = result.incorrect_answers
                    .Select(ia => new Answer { Text = ia, Result = false })
                    .Append(new Answer { Text = result.correct_answer, Result = true })
                    .Append(new Answer { Text = "DummyAnswer", Result = false })
                    .OrderBy(a => Guid.NewGuid())
                    .ToList()
            }).ToList();
        }

        public IEnumerable<ChallengeOutputDto> MapChallengesIntoDtos(IEnumerable<Challenge> challenges)
        {
            return challenges.Select(ch => {
                var challengeDto = new ChallengeOutputDto
                {
                    Id = ch.Id,
                    Questions = ch.Questions.Select(q => 
                        new QuestionOutputDto { Id = q.Id, 
                            Text = q.Text, 
                            Answers = MapAnswersIntoDto(q.Answers), 
                            Difficulty = ch.Difficulty, 
                            Category = ch.Category,
                            Challenge = ch
                        }),
                    PlayerSections = ch.PlayerSections.Select(ps => 
                        new PlayerSectionOutputDto 
                        {
                            Id = ps.Id,
                            Player = new PlayerOutputDto 
                            { 
                                Id = ps.Player.Id, 
                                UserName = ps.Player.UserName, 
                                IdentityId = ps.Player.IdentityId, 
                                Scores = ps.Player.Scores.Select(sc => new ScoreOutputDto { Id = sc.Id, Amount= sc.Amount, Category = sc.Category}),
                                PlayerSections = new List<PlayerSectionOutputDto>()
                                
                            },
                            Counter = ps.Counter,
                            Points = ps.Points,
                            Answers = ps.Answers,
                            Category = ch.Category
                        }),
                    Difficulty = ch.Difficulty,
                    Category = ch.Category
                };

                foreach (var ps in challengeDto.PlayerSections)
                {
                    ps.Player.PlayerSections.Append(ps);

                    foreach (var sc in ps.Player.Scores)
                    {
                        sc.Player = ps.Player;
                    }
                }

                return challengeDto;
            }).ToList();
        }


        public IEnumerable<AnswerOutputDto> MapAnswersIntoDto(IEnumerable<Answer> answers)
        {
            return answers.Select(a => new AnswerOutputDto { Id = a.Id, Result = a.Result, Text = a.Text}).ToList();
        }
        public PlayerOutputDto MapPlayerIntoDto(Player p)
            => new PlayerOutputDto
            {
                Id = p.Id,
                UserName = p.UserName,
                IdentityId = p.IdentityId,
                Scores = p.Scores.Select(sc => new ScoreOutputDto { Id = sc.Id, Amount = sc.Amount, Category = sc.Category }),
                PlayerSections = p.PlayerSections.Select(ps => new PlayerSectionOutputDto { Id = ps.Id, Points = ps.Points, Counter = ps.Counter, Category = ps.Category, Answers = ps.Answers })
            };

        public IEnumerable<PlayerOutputDto> MapPlayersIntoDto(IEnumerable<Player> players)
            => players.Select(p => new PlayerOutputDto
            {
                Id = p.Id,
                UserName = p.UserName,
                IdentityId = p.IdentityId,
                Scores = p.Scores.Select(sc => new ScoreOutputDto { Id = sc.Id, Amount = sc.Amount, Category = sc.Category }),
                PlayerSections = p.PlayerSections.Select(ps => new PlayerSectionOutputDto { Id = ps.Id, Points = ps.Points, Counter = ps.Counter, Category = ps.Category, Answers = ps.Answers  })
            });
    }
}
