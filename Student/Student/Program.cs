using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public double MathScore { get; set; }
    public double PhysicsScore { get; set; }
    public double ChemistryScore { get; set; }
    public double AverageScore { get; private set; }
    public string AcademicPerformance { get; private set; }

    public Student(int id, string name, string gender, int age, double mathScore, double physicsScore, double chemistryScore)
    {
        Id = id;
        Name = name;
        Gender = gender;
        Age = age;
        MathScore = mathScore;
        PhysicsScore = physicsScore;
        ChemistryScore = chemistryScore;
        CalculateAverageScore();
        DetermineAcademicPerformance();
    }

    public void CalculateAverageScore()
    {
        AverageScore = (MathScore + PhysicsScore + ChemistryScore) / 3;
    }

    public void DetermineAcademicPerformance()
    {
        if (AverageScore >= 8.0)
        {
            AcademicPerformance = "Gioi";
        }
        else if (AverageScore >= 6.5)
        {
            AcademicPerformance = "Kha";
        }
        else if (AverageScore >= 5.0)
        {
            AcademicPerformance = "Trung binh";
        }
        else
        {
            AcademicPerformance = "Yeu";
        }
    }
}

public class StudentManager
{
    private List<Student> students = new List<Student>();
    private int nextId = 1;

    public void AddStudent(string name, string gender, int age, double mathScore, double physicsScore, double chemistryScore)
    {
        Student student = new Student(nextId++, name, gender, age, mathScore, physicsScore, chemistryScore);
        students.Add(student);
    }

    public void UpdateStudent(int id, string name, string gender, int age, double mathScore, double physicsScore, double chemistryScore)
    {
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student != null)
        {
            student.Name = name;
            student.Gender = gender;
            student.Age = age;
            student.MathScore = mathScore;
            student.PhysicsScore = physicsScore;
            student.ChemistryScore = chemistryScore;
            student.CalculateAverageScore();
            student.DetermineAcademicPerformance();
        }
        else
        {
            Console.WriteLine("Khong tim thay sinh vien co ID " + id);
        }
    }

    public void DeleteStudent(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Da xoa sinh vien co ID " + id);
        }
        else
        {
            Console.WriteLine("Khong tim thay sinh vien co ID " + id);
        }
    }

    public void SearchStudent(string name)
    {
        var foundStudents = students.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();
        if (foundStudents.Count > 0)
        {
            Console.WriteLine("Danh sach sinh vien co ten chua '" + name + "':");
            DisplayStudentList(foundStudents);
        }
        else
        {
            Console.WriteLine("Khong tim thay sinh vien co ten chua '" + name + "'");
        }
    }

    public void SortStudentsByGPA()
    {
        var sortedStudents = students.OrderByDescending(s => s.AverageScore).ToList();
        Console.WriteLine("Danh sach sinh vien duoc sap xep theo GPA:");
        DisplayStudentList(sortedStudents);
    }

    public void SortStudentsByName()
    {
        var sortedStudents = students.OrderBy(s => s.Name).ToList();
        Console.WriteLine("Danh sach sinh vien duoc sap xep theo ten:");
        DisplayStudentList(sortedStudents);
    }

    public void SortStudentsById()
    {
        var sortedStudents = students.OrderBy(s => s.Id).ToList();
        Console.WriteLine("Danh sach sinh vien duoc sap xep theo ID:");
        DisplayStudentList(sortedStudents);
    }

    public void DisplayAllStudents()
    {
        Console.WriteLine("Danh sach sinh vien:");
        DisplayStudentList(students);
    }

    private void DisplayStudentList(List<Student> studentList)
    {
        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-5} {4,-10} {5,-10} {6,-10} {7,-10} {8,-15}",
            "ID", "Ten", "Gioi tinh", "Tuoi", "Toan", "Ly", "Hoa", "Diem TB", "Hoc luc");
        foreach (var student in studentList)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-5} {4,-10} {5,-10} {6,-10} {7,-10:0.##} {8,-15}",
                student.Id, student.Name, student.Gender, student.Age, student.MathScore, student.PhysicsScore,
                student.ChemistryScore, student.AverageScore, student.AcademicPerformance);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        StudentManager studentManager = new StudentManager();
        string choice;
        do
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Cap nhat thong tin sinh vien");
            Console.WriteLine("3. Xoa sinh vien");
            Console.WriteLine("4. Tim kiem sinh vien theo ten");
            Console.WriteLine("5. Sap xep sinh vien theo GPA");
            Console.WriteLine("6. Sap xep sinh vien theo ten");
            Console.WriteLine("7. Sap xep sinh vien theo ID");
            Console.WriteLine("8. Hien thi danh sach sinh vien");
            Console.WriteLine("9. Thoat");
            Console.Write("Nhap lua chon cua ban: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Nhap ten sinh vien: ");
                    string name = Console.ReadLine();
                    Console.Write("Nhap gioi tinh sinh vien: ");
                    string gender = Console.ReadLine();
                    Console.Write("Nhap tuoi sinh vien: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Nhap diem toan: ");
                    double mathScore = double.Parse(Console.ReadLine());
                    Console.Write("Nhap diem ly: ");
                    double physicsScore = double.Parse(Console.ReadLine());
                    Console.Write("Nhap diem hoa: ");
                    double chemistryScore = double.Parse(Console.ReadLine());
                    studentManager.AddStudent(name, gender, age, mathScore, physicsScore, chemistryScore);
                    Console.WriteLine("Them sinh vien thanh cong!");
                    break;
                case "2":
                    Console.Write("Nhap ID cua sinh vien can cap nhat: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Nhap ten moi: ");
                    string updateName = Console.ReadLine();
                    Console.Write("Nhap gioi tinh moi: ");
                    string updateGender = Console.ReadLine();
                    Console.Write("Nhap tuoi moi: ");
                    int updateAge = int.Parse(Console.ReadLine());
                    Console.Write("Nhap diem toan moi: ");
                    double updateMathScore = double.Parse(Console.ReadLine());
                    Console.Write("Nhap diem ly moi: ");
                    double updatePhysicsScore = double.Parse(Console.ReadLine());
                    Console.Write("Nhap diem hoa moi: ");
                    double updateChemistryScore = double.Parse(Console.ReadLine());
                    studentManager.UpdateStudent(updateId, updateName, updateGender, updateAge, updateMathScore, updatePhysicsScore, updateChemistryScore);
                    break;
                case "3":
                    Console.Write("Nhap ID cua sinh vien can xoa: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    studentManager.DeleteStudent(deleteId);
                    break;
                case "4":
                    Console.Write("Nhap ten sinh vien can tim: ");
                    string searchName = Console.ReadLine();
                    studentManager.SearchStudent(searchName);
                    break;
                case "5":
                    studentManager.SortStudentsByGPA();
                    break;
                case "6":
                    studentManager.SortStudentsByName();
                    break;
                case "7":
                    studentManager.SortStudentsById();
                    break;
                case "8":
                    studentManager.DisplayAllStudents();
                    break;
                case "9":
                    Console.WriteLine("Thoat chuong trinh.");
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                    break;
            }
        } while (choice != "9");
    }
}
