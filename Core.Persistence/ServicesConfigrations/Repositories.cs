using Core.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Persistence.ServicesConfigrations;

public static class Repositories
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
        {
            services.AddScoped<IAnswerAgreementRepository, AnswerAgreementRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<ICheckRepository, CheckRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IClinicPhoneNumberRepository, ClinicPhoneNumberRepository>();
            services.AddScoped<ICommentAgreementRepository, CommentAgreementRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IDoctorRateRepository, DoctorRateRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IPatientDoctorRepository, PatientDoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IPostStateSuggestionRepository, PostStateSuggestionRepository>();

            return services;
        }
}