using Lnu_web.Dbo.User.Student;
using Lnu_web.Dbo.User.Teacher;
using Lnu_web.Dbo.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lnu_web.Dbo.Schedule;
using Lnu_web.Dbo.Departament;
using Lnu_web.Dbo.Chat;
using Lnu_web.Dbo.Labs;

namespace Lnu_web.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<StudentDbo> Students { get; set; }
        public DbSet<TeacherDbo> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TeacherSchedule> teacherSchedules { get; set; }
        public DbSet<DepartamentDbo> departaments { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<PrivateChatsDbo> PrivateChats { get; set; }
        public DbSet<GroupDbo> ChatGroups { get; set; }
        public DbSet<GroupUsersDbo> GroupUsers { get; set; }
        public DbSet<GroupMessageDbo> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentDbo>()
                .HasOne(s => s.AcademicalStudent)
                .WithOne() 
                .HasForeignKey<AcademicalStudentDbo>(a => a.Id);  

            modelBuilder.Entity<StudentDbo>()
                .HasOne(s => s.CoreStudent)
                .WithOne() 
                .HasForeignKey<CoreStudentDbo>(c => c.Id);

            modelBuilder.Entity<TeacherDbo>()
                .HasOne(s => s.AcademicDetails)
                .WithOne()
                .HasForeignKey<AcademicalTeacherDbo>(a => a.Id);

            modelBuilder.Entity<TeacherDbo>()
                .HasOne(s => s.CoreInfo)
                .WithOne()
                .HasForeignKey<CoreTeacherDbo>(c => c.Id);

            modelBuilder.Entity<TeacherDbo>()
                .HasOne(s => s.AdditionalInfo)
                .WithOne()
                .HasForeignKey<AdditionalTeacherDbo>(c => c.Id);

            modelBuilder.Entity<GroupUsersDbo>()
                .HasKey(gu => new { gu.GroupId, gu.UserId });

            modelBuilder.Entity<GroupUsersDbo>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUsers)  
                .HasForeignKey(gu => gu.GroupId) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
