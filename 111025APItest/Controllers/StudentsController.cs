using _111025APItest.DataContext;
using _111025APItest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _111025APItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public StudentsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _dbContext.Students.ToList();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Student updatedStudent)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            student.Name = updatedStudent.Name;
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return NoContent();
        }

        #region test1
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var students = new[]
        //    {
        //        new { Id=1, Name="Alice"},
        //        new { Id=2, Name="Bob"},
        //        new { Id=3, Name="Charlie"},
        //    };

        //    return Ok(students);
        //}


        ////parametr olaraq yazmaq
        //[HttpGet("{id}")]
        //public IActionResult GetAll(int id)
        //{
        //    var students = new[]
        //    {
        //        new { Id=1, Name="Alice"},
        //        new { Id=2, Name="Bob"},
        //        new { Id=3, Name="Charlie"},
        //    };

        //    return Ok(students);
        //}

        ////endpoint adini deyismek
        //[HttpGet("v2")]
        //public IActionResult GetAll(double d)
        //{
        //    var students = new[]
        //    {
        //        new { Id=1, Name="Alice"},
        //        new { Id=2, Name="Bob"},
        //        new { Id=3, Name="Charlie"},
        //    };

        //    return Ok(students);
        //}

        //[HttpGet]
        //[Route("test")]
        //public IActionResult GetAll(string s)
        //{
        //    var students = new[]
        //    {
        //        new { Id=1, Name="Alice"},
        //        new { Id=2, Name="Bob"},
        //        new { Id=3, Name="Charlie"},
        //    };

        //    return Ok(students);
        //}
        #endregion

        #region test2
        //private static List<Student> students = new List<Student>
        //{
        //    new Student { Id = 1, Name="Alice" },
        //    new Student { Id = 2,Name="Bob" } 
        //};

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    return Ok(students);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var student = students.FirstOrDefault(s=>s.Id == id);
        //    if (student == null)
        //        return NotFound();

        //    return Ok(student);
        //}

        //[HttpPost]
        //public IActionResult Create(Student student) {
        //    student.Id = students.Max(s => s.Id) + 1;
        //    students.Add(student);

        //    return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, Student updatedStudent)
        //{
        //    var student = students.FirstOrDefault(s=>s.Id==id);
        //    if(student == null)
        //        return NotFound();
        //    student.Name = updatedStudent.Name;

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var student = students.FirstOrDefault(s => s.Id == id);
        //    if (student == null)
        //        return NotFound();
        //    students.Remove(student);

        //    return NoContent();
        //}

        #endregion

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }

        [HttpPost]
        [Route("download")]
        public IActionResult DownloadFile([FromQuery] string fileName)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadsFolder, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var bytes = System.IO.File.ReadAllBytes(filePath);
            var mimeType = "application/octet-stream";
            return File(bytes, mimeType, fileName);
        }
    }
}
