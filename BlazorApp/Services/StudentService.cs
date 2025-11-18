using ClassLibrary;

public class StudentService : IStudentService
{
    private readonly HttpClient _http;

    public StudentService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<Student>>("https://localhost:7246/api/Students");
    }

    public async Task DeleteStudentAsync(int id)
    {
        await _http.DeleteAsync($"https://localhost:7246/api/Students/{id}");
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<Student>($"https://localhost:7246/api/Students/{id}");
    }

    public async Task UpdateStudentAsync(Student student)
    {
        await _http.PutAsJsonAsync($"https://localhost:7246/api/Students/{student.Id}", student);
    }


}

