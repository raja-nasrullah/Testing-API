using Xunit;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System;
using ClassLibrary;
using Moq.Protected;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;

public class StudentServiceTests
{
    public object Id { get; private set; }

    //[Fact]
    //public async Task GetStudentsAsync_ReturnsStudentList()
    //{
    //    // Arrange
    //    var studentList = new List<Student>();
    //    for (int i = 1; i <= 1000000; i++)
    //    {
    //        studentList.Add(new Student
    //        {
    //            Id = i,
    //            Name = $"Student {i}",
    //            Description = $"Description {i}",
    //            Age = 10 + i
    //        });
    //    }

    //    var handlerMock = new Mock<HttpMessageHandler>();
    //    handlerMock.Protected()
    //        .Setup<Task<HttpResponseMessage>>("SendAsync",
    //            ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
    //        .ReturnsAsync(new HttpResponseMessage()
    //        {
    //            StatusCode = HttpStatusCode.OK,
    //            Content = JsonContent.Create(studentList)
    //        });

    //    var httpClient = new HttpClient(handlerMock.Object);
    //    var service = new StudentService(httpClient);

    //    // Act
    //    var result = (await service.GetStudentsAsync()).ToList();

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(1000000, result.Count);
    //    Assert.Contains(result, s => s.Name == "Student 1");
    //    Assert.Contains(result, s => s.Name == "Student 1000000");
    //}

    //[Fact]
    //public async Task GetStudent_ReturnsStudent_WhenIdExists()
    //{
    //    // Arrange
    //    var options = new DbContextOptionsBuilder<AddDbContext>()
    //        .UseInMemoryDatabase(databaseName: "Project1")
    //        .Options;

    //    using (var context = new AddDbContext(options))
    //    {
    //        var student = new Student
    //        {
    //            Id = 1,
    //            Name = "Test",
    //            Description = "Test Desc",
    //            Age = 15
    //        };

    //        context.tbl_Students.Add(student);
    //        await context.SaveChangesAsync();

    //        var controller = new StudentsController(context);

    //        // Act
    //        var result = await controller.GetStudent(1);

    //        // Assert
    //        var actionResult = Assert.IsType<ActionResult<Student>>(result);
    //        var returnValue = Assert.IsType<Student>(actionResult.Value);
    //        Assert.Equal("Test", returnValue.Name);
    //    }
    //}


    //[Fact]
    //public async Task DeleteStudent_RemovesStudentAndReturnsNoContent()
    //{
    //    // Arrange
    //    var options = new DbContextOptionsBuilder<AddDbContext>()
    //        .UseInMemoryDatabase(databaseName: "Project1")//is jga real  db ka name likhna h
    //        .Options;

    //    using (var context = new AddDbContext(options))
    //    {
    //        var student = new Student
    //        {
    //            Id = 10,
    //            Name = "Test Student",
    //            Description = "Test Description",
    //            Age = 20
    //        };

    //        context.tbl_Students.Add(student);
    //        await context.SaveChangesAsync();

    //        var controller = new StudentsController(context);

    //        // Act
    //        var result = await controller.DeleteStudent(10);

    //        // Assert
    //        Assert.IsType<NoContentResult>(result);

    //        var deletedStudent = await context.tbl_Students.FindAsync(10);
    //        Assert.Null(deletedStudent); // check student deleted
    //    }
    //}

    //[Fact]
    //public async Task PostStudent_AddsStudentAndReturnsCreatedResult()
    //{
    //    // Arrange
    //    var options = new DbContextOptionsBuilder<AddDbContext>()
    //        .UseInMemoryDatabase(databaseName: "Project1") // Unique name to avoid key conflict
    //        .Options;

    //    using var context = new AddDbContext(options);
    //    var controller = new StudentsController(context);

    //    var newStudent = new Student
    //    {
    //        Id = 1, // Id as string now
    //        Name = "Ali",
    //        Description = "Hardworking",
    //        Age = 15
    //    };

    //    // Act
    //    var result = await controller.PostStudent(newStudent);

    //    // Assert
    //    var actionResult = Assert.IsType<ActionResult<Student>>(result);
    //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
    //    var returnValue = Assert.IsType<Student>(createdAtActionResult.Value);

    //    Assert.Equal(newStudent.Id, returnValue.Id);
    //    Assert.Equal(newStudent.Name, returnValue.Name);
    //    Assert.Equal(newStudent.Description, returnValue.Description);
    //    Assert.Equal(newStudent.Age, returnValue.Age);
    //}




    [Fact]
    public async Task PutStudent_UpdatesStudent_WhenIdMatches()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AddDbContext>()
            .UseInMemoryDatabase(databaseName: "Project1") // fresh DB each run
            .Options;

        using var context = new AddDbContext(options);

        // Add initial student
        var student = new Student { Id = 1, Name = "Old Name", Description = "Old", Age = 14 };
        context.tbl_Students.Add(student);
        await context.SaveChangesAsync();

        var controller = new StudentsController(context);

        // Updated student object with same Id
        var updatedStudent = new Student { Id = 1, Name = "New Name", Description = "Updated", Age = 15 };

        // Act
        var result = await controller.PutStudent(1, updatedStudent);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var updated = await context.tbl_Students.FindAsync(1);
        Assert.Equal("New Name", updated.Name);
        Assert.Equal("Updated", updated.Description);
        Assert.Equal(15, updated.Age);
    }




}
