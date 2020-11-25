using AutoMapper;
using Quizzish.DTO_s;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Profiles
{
    public class QuizzishProfile : Profile
    {
        public QuizzishProfile()
        {
            CreateMap<Challenge, ChallengeOutputDto>();
            CreateMap<ChallengeInputDto, Challenge>();
            CreateMap<Challenge, ChallengeInputDto>();

            CreateMap<Question, QuestionOutputDto>();
            CreateMap<QuestionInputDto, Question>();
            CreateMap<Question, QuestionInputDto>();

            CreateMap<Answer, AnswerOutputDto>();
            CreateMap<AnswerInputDto, Answer>();
            CreateMap<Answer, AnswerInputDto>();

            CreateMap<Player, PlayerOutputDto>();
            CreateMap<PlayerInputDto, Player>();
            CreateMap<Player, PlayerInputDto>();

            CreateMap<PlayerSection, PlayerSectionOutputDto>();
            CreateMap<PlayerSectionInputDto, PlayerSection>();
            CreateMap<PlayerSection, PlayerSectionInputDto>();

            CreateMap<Score, ScoreOutputDto>();
            CreateMap<ScoreInputDto, Score>();
            CreateMap<Score, ScoreInputDto>();
        }
    }
}
