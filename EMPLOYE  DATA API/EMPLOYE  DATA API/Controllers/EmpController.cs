using EMPLOYE__DATA_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace EMPLOYE__DATA_API.Controllers
{
    [Route("emp")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly Empcls db;

        public EmpController(Empcls contetxt)
        {
            db = contetxt;
        }

        [HttpGet]
        [Route("GetByID")]
        public IEnumerable<Emp> Get([FromQuery] int id)
        {

            var x = db.emps.Where(x => x.id == id);
            db.SaveChanges();
            return x;
        }

        [HttpGet]
        public IEnumerable<Emp> Get()
        {
            return db.emps.ToList();
        }



        [HttpPost]
        public IActionResult post([FromBody] Emp emp)
        {
            if (emp == null)
            {
                return BadRequest("Invlid data");
            }
            else
            {
                try
                {

                    db.emps.Add(emp);
                    db.SaveChanges();

                    return Ok(new { Message = "Employee Data is Add....." });
                }
                catch (Exception )
                {
                    return Ok("Error!!");
                }
            }

        }

        [HttpPut]

        public IActionResult Put(int id,[FromBody] Emp emp)
        {
            if(emp == null)
            {
                return BadRequest("inlvalid Data");
            }
            else
            {
                try
                {
                    var exite = db.emps.FirstOrDefault(x => x.id == id);
                    exite.emp_name = emp.emp_name;
                    exite.emp_post = emp.emp_post;
                    exite.emp_sal = emp.emp_sal;            
                    db.emps.Update(exite);
                    db.SaveChanges();
                    return Ok(new { Message = "Employee Data is Updating....." });
                }
                catch(Exception )
                {
                    return Ok("Error");
                }
            }
            
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var empdel = db.emps.Where((x) => x.id == id).FirstOrDefault();
            if (empdel == null)
            {
                return BadRequest("Invalid Data");
            }
            else
            {
                try
                {
                    db.emps.Remove(empdel);
                    db.SaveChanges();
                    return Ok(new { Message = "Employee Data is Deleting..." });
                }
                catch(Exception )
                {
                    return Ok("Error!!");
                }
            }
            
        }

    }
}
