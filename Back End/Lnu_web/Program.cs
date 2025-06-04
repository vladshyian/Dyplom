using Lnu_web.Data;
using Lnu_web.Dbo.User;
using Lnu_web.Interfaces.Reposotories.IChatRepository;
using Lnu_web.Interfaces.Reposotories.IDepartamentRepository;
using Lnu_web.Interfaces.Reposotories.ILogin;
using Lnu_web.Interfaces.Reposotories.IScheduleRepository;
using Lnu_web.Interfaces.Reposotories.IUserRepository;
using Lnu_web.Reposotories.ChatRepository;
using Lnu_web.Reposotories.DepartamentRepository;
using Lnu_web.Reposotories.LoginRepository;
using Lnu_web.Reposotories.ScheduleRepository;
using Lnu_web.Reposotories.UserRepository;
using Lnu_web.Services.ChatService;
using Lnu_web.Services.DepartamentService;
using Lnu_web.Services.LoginService;
using Lnu_web.Services.ScheduleService;
using Lnu_web.Services.UserService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//data
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
<<<<<<< HEAD
        policy => policy.WithOrigins("https://lviv-univeristy.vercel.app")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
=======
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        );
>>>>>>> 875b81d (Initial commit to main)
});

//mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//repos
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ScheduleRepository>();
builder.Services.AddScoped<DepartamentRepository>();
builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILogin, Login>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

//services
builder.Services.AddScoped<DepartamentService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ChatService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");


app.UseCors("AllowLocalhost");

app.MapControllers();

app.Run();
