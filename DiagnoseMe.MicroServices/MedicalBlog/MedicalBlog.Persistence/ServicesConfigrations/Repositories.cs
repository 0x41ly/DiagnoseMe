using MedicalBlog.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalBlog.Persistence.ServicesConfigrations;

public static class Repositories
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
        {
            services.AddScoped<IAnswerAgreementRepository, AnswerAgreementRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICommentAgreementRepository, CommentAgreementRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IPostRatingRepository, PostRatingRepository>();
            services.AddScoped<IPostViewRepository, PostViewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSubscribedUserRepository, UserSubscribedUserRepository>();
            return services;
        }
}