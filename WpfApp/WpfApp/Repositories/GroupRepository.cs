using CsvHelper;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.DAL;
using SautinSoft.PdfVision;

namespace WpfApp.Repositories
{
    public class GroupRepository
    {
        private readonly AppDbContext _context;
        public GroupRepository()
        {
            _context = new AppDbContext();
        }
        public ObservableCollection<GroupModel> GetGroups(int courseId)
        {
            return new ObservableCollection<GroupModel>(_context.Groups.Where(g => g.COURSE_ID == courseId).ToList());
        }

        public ObservableCollection<GroupModel> GetAllGroups() 
        {
            return new ObservableCollection<GroupModel>(_context.Groups.ToList());
        }

        public GroupModel FindGroupById(int groupId)
        {
            return _context.Groups.FirstOrDefault(c => c.GROUP_ID == groupId);
        }

        public void CreateGroup(string groupName, int courseId)
        {
            GroupModel newGroup = new GroupModel
            {
                NAME = groupName, 
                COURSE_ID = courseId            
            };

            _context.Groups.Add(newGroup);   
            _context.SaveChanges();
        }

        public void ChangeGroupName(int groupId, string newGroupName)
        {
            GroupModel group = _context.Groups.FirstOrDefault(g => g.GROUP_ID == groupId);
            if (group != null)
            {
                group.NAME = newGroupName;
                _context.SaveChanges();
            }
        }

        public bool HasStudents(int groupId)
        {
            return _context.Students.Any(s => s.GROUP_ID == groupId);
        }

        public void DeleteGroup(int groupId)
        {
            GroupModel group = _context.Groups.FirstOrDefault(g => g.GROUP_ID == groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }
        }

        public CourseModel FindCourseById(int courseId)
        {
            return _context.Courses.FirstOrDefault(c => c.COURSE_ID == courseId);
        }

        public void ExportStudents(int groupId, string filePath)
        {
            var students = _context.Students.Where(s => s.GROUP_ID == groupId).ToList();

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(students.Select(s => new { s.GROUP_ID, s.FIRST_NAME, s.LAST_NAME }));
            }
        }

        public void ImportStudents(int groupId, string filePath)
        {
            try
            {
                // Clear the group first
                var studentsInGroup = _context.Students.Where(s => s.GROUP_ID == groupId);
                _context.Students.RemoveRange(studentsInGroup);

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>();
                    foreach (var record in records)
                    {
                        var student = new StudentModel
                        {
                            GROUP_ID = groupId,
                            FIRST_NAME = record.FIRST_NAME,
                            LAST_NAME = record.LAST_NAME
                        };
                        _context.Students.Add(student);
                        _context.SaveChanges();
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception and show an error message to the user
                MessageBox.Show($"An error occurred while importing students: {ex.Message}");
            }
        }

        public void ExportGroupToDocx(string folderPath, GroupModel group, CourseModel course, List<StudentModel> students)
        {
            // Create a new DOCX document
            DocumentCore docx = new DocumentCore();
            Section section = new Section(docx);
            docx.Sections.Add(section);

            docx.Content.End.Insert("Course Name: " + course.NAME + "\nGroup Name: " + group.NAME + "\n", new CharacterFormat() { Size = 14, Bold = true });

            // Add a numbered list of students
            for (int i = 0; i < students.Count; i++)
            {
                docx.Content.End.Insert((i + 1).ToString() + ". " + students[i].FIRST_NAME + " " + students[i].LAST_NAME + "\n", new CharacterFormat() { Size = 12 });
            }

            // Save the document to the specified file path
            docx.Save(folderPath, new DocxSaveOptions());
        }

        public void ExportGroupToPdf(string folderPath, GroupModel group, CourseModel course, List<StudentModel> students)
        {
            // Create a new PDF document
            DocumentCore pdf = new DocumentCore();

            // Create a new section
            Section section = new Section(pdf);
            pdf.Sections.Add(section);

            // Add the course name and group name to the document
            section.Blocks.Add(new Paragraph(pdf, new Run(pdf, "Course Name: " + course.NAME, new CharacterFormat() { Size = 14, Bold = true })));
            section.Blocks.Add(new Paragraph(pdf, new Run(pdf, "Group Name: " + group.NAME, new CharacterFormat() { Size = 14, Bold = true })));
            section.Blocks.Add(new Paragraph(pdf, new SpecialCharacter(pdf, SpecialCharacterType.LineBreak)));

            // Add a numbered list of students
            for (int i = 0; i < students.Count; i++)
            {
                section.Blocks.Add(new Paragraph(pdf, new Run(pdf, (i + 1).ToString() + ". " + students[i].FIRST_NAME + " " + students[i].LAST_NAME, new CharacterFormat() { Size = 12 })));
            }

            // Save the PDF document to the specified file path
            
            pdf.Save(folderPath, new PdfSaveOptions());
        }
    }
}
