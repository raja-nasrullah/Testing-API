using ClassLibrary;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetStudentsAsync();

    Task DeleteStudentAsync(int id);

    Task<Student> GetStudentByIdAsync(int id);
    Task UpdateStudentAsync(Student student);

}
