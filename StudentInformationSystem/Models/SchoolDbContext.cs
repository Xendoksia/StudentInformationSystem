using Microsoft.EntityFrameworkCore;

namespace StudentInformationSystem.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.IdentityNumber)
                .IsRequired()
                .HasMaxLength(50);

            // ---------- SEED DATA BAŞLANGICI ----------

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "12345678901", Password = "password123", Role = "admin", IdentityNumber = "12345678901" },
                new User { Id = 2, Username = "10987654321", Password = "password456", Role = "student", IdentityNumber = "10987654321" },
                new User { Id = 3, Username = "11223344556", Password = "password789", Role = "teacher", IdentityNumber = "11223344556" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 2,
                    IdentityNumber = "10987654321",
                    Name = "Alice",
                    Surname = "Johnson",
                    Gender = "Female",
                    BirthDate = new DateTime(2000, 5, 15),
                    PhoneNumber = "+905551234567",
                    Email = "alice.johnson@example.com",
                    Address = "123 Main Street",
                    StudentNumber = "S1234567"
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id =3, 
                    IdentityNumber = "11223344556",
                    Name = "John",
                    Surname = "Doe",
                    Email = "john.doe@example.com",
                    Office = "B12"
                }
            );

            modelBuilder.Entity<Lesson>().HasData(
                new Lesson { Id = 6, Code = "CS101", Name = "Introduction to Computer Science", Semester = "1", Credit = 4 },
                new Lesson { Id = 7, Code = "MATH102", Name = "Calculus I", Semester = "1", Credit = 5 }
            );

            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = 8, StudentNumber = "S1234567", Code = "CS101", StudentId = 4, LessonId = 6, GradeValue = "A" }
            );

            modelBuilder.Entity<Announcement>().HasData(
                new Announcement { Id = 9, Title = "Welcome to the New Semester!", Content = "Classes start on September 1st. Good luck!", Date = DateTime.Now },
                new Announcement { Id = 10, Title = "Midterm Exams", Content = "Midterm exams will be held between November 10-15.", Date = DateTime.Now }
            );

            // ---------- SEED DATA BİTİŞİ ----------
        }
    }
}
