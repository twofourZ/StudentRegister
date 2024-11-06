using Microsoft.EntityFrameworkCore;

namespace StudentRegister
{
    internal class UserInterface
    {
        private StudentRegisterDbContext context;
        public UserInterface(StudentRegisterDbContext context)
        {
            this.context = context;
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    "Press 1 - New student\n" +
                    "Press 2 - Edit student\n" +
                    "Press 3 - View students\n");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        NewStudent();
                        break;
                    case '2':
                        EditStudent();
                        break;
                    case '3':
                        ViewStudents();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Error");
                        System.Threading.Thread.Sleep(500);
                        break;
                }
            }
        }

        public void ViewStudents()
        {
            Console.Clear();

            var students = context.Students
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine("Name: " + student.FirstName + " " + student.LastName);
                Console.WriteLine("City: " + student.City);
                foreach (var course in student.Courses)
                {
                    Console.WriteLine("CourseID: " + course.CourseId + " - Teacher: " + course.Teacher.FirstName + " " + course.Teacher.LastName + " - Subject: " + course.Subject + ", ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }

        public void EditStudent()
        {
            while (true)
            {
                Console.Clear();

                var students = context.Students
                    .Include(x => x.Courses)
                    .ToList();

                foreach (var s in students)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("StudentID: " + s.StudentId);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Name: " + s.FirstName + " " + s.LastName);
                    Console.WriteLine("City: " + s.City);
                    Console.Write("Courses: ");
                    foreach (var course in s.Courses)
                    {
                        Console.Write(course.Subject + ", ");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }

                Console.WriteLine("Enter StudentID to edit student");
                Console.ForegroundColor = ConsoleColor.Blue;
                var input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                var student = context.Students
                    .Include(x => x.Courses)
                    .FirstOrDefault(x => x.StudentId.ToString() == input);

                if (student != null)
                {
                    Console.Clear();
                    Console.WriteLine("First name: ");
                    student.FirstName = Console.ReadLine();
                    Console.WriteLine("Last name: ");
                    student.LastName = Console.ReadLine();
                    Console.WriteLine("City: ");
                    student.City = Console.ReadLine();

                    var availableCourses = context.Courses
                        .Include(x => x.Teacher)
                        .ToList();

                    ApplyToCourses(student);

                    try
                    {
                        context.Students.Update(student);
                        context.SaveChanges();
                        Console.WriteLine("Edit successful!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error: " + ex.Message);
                    }
                }

            }
        }

        public void NewStudent()
        {
            Console.Clear();

            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = Console.ReadLine();
            Console.Write("City: ");
            string city = Console.ReadLine();

            var student = new Student() { FirstName = firstName, LastName = lastName, City = city };

            ApplyToCourses(student);

            try
            {
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine(
                    "New student created\n" +
                    "Welcome " + student.FirstName + "!");
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error: " + ex.Message);
            }
        }

        private void ApplyToCourses(Student student)
        {
            var availableCourses = context.Courses
                .Include(x => x.Teacher)
                .ToList();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Your courses: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                for (int i = 0; i < student.Courses.Count; i++)
                {
                    Console.WriteLine("CourseID: " + student.Courses[i].CourseId.ToString());
                    Console.WriteLine("Subject: " + student.Courses[i].Subject);
                    Console.WriteLine("Teacher: " + student.Courses[i].Teacher.FirstName + " " + student.Courses[i].Teacher.LastName);
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Available courses: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                for (int i = 0; i < availableCourses.Count; i++)
                {
                    Console.WriteLine("Teacher: " + availableCourses[i].Teacher.FirstName + " " + availableCourses[i].Teacher.LastName);
                    Console.WriteLine("CourseID: " + availableCourses[i].CourseId.ToString());
                    Console.WriteLine("Subject: " + availableCourses[i].Subject);
                    Console.WriteLine();
                }

                Console.WriteLine(
                    "Enter 0 to accept\n" +
                    "Enter CourseID and '+' to apply for course\n" +
                    "Enter CourseID and '-' to remove from your courses");

                var input = Console.ReadLine();

                if (input.EndsWith('+'))
                {
                    input = input.Substring(0, input.Length - 1);
                    var course = availableCourses.FirstOrDefault(x => x.CourseId.ToString() == input);

                    if (course != null)
                    {
                        if (student.Courses.Contains(course))
                        {
                            Console.WriteLine("You are already in this course");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            student.Courses.Add(course);
                            Console.WriteLine("Course added!");
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                    if (course == null)
                    {
                        Console.WriteLine("CourseID " + input + " not found");
                        System.Threading.Thread.Sleep(500);
                    }
                }

                if (input.EndsWith('-'))
                {
                    input = input.Substring(0, input.Length - 1);
                    var course = availableCourses.FirstOrDefault(x => x.CourseId.ToString() == input);

                    if (course != null)
                    {
                        if (!student.Courses.Contains(course))
                        {
                            Console.WriteLine("You are not in this course");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            student.Courses.Remove(course);
                            Console.WriteLine("Course removed!");
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                    if (course == null)
                    {
                        Console.WriteLine("CourseID " + input + " not found");
                        System.Threading.Thread.Sleep(500);
                    }
                }

                if (input == "0")
                {
                    break;
                }
            }
        }
    }
}