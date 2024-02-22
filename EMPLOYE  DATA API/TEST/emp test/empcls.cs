using Microsoft.EntityFrameworkCore;
using static Xunit.Assert;
using EMPLOYE__DATA_API.Model;
using EMPLOYE__DATA_API.Controllers;

namespace TEST.emp_test
{
    public class empcls
    {
        public readonly DbContextOptions<Empcls> _Options;
        public Empcls db;
        public EmpController Controller;

        public empcls()
        {
            _Options = new DbContextOptionsBuilder<Empcls>().UseInMemoryDatabase(databaseName: "empdata").Options;

            db = new Empcls(_Options);
            Controller = new EmpController(db);
        }

    
    private static Emp insertempdata()
        {
            return new Emp()
            {
                id = 11,
                emp_name = "SCOTT",
                emp_post = "MANAGER",
                emp_sal = 20000,

            };
        }

        //Get Data
        [Fact]
        public void getempdata()
        {
            //setup
            var empinsertdata = insertempdata();
            db.emps.Add(empinsertdata);
            db.SaveChanges();
            

            //Execute
            var res = Controller.Get(empinsertdata.id);

            //Assert
            NotEmpty(res);
            Equal("SCOTT", res.FirstOrDefault().emp_name);
        }

        // ------------------------------------------------ Insert Data -----------------------------------------------------------------
        [Fact]
        public void empinsert()
        {
            //setup
            var empinsertdata = insertempdata();
          

            //Exicute
            var res = Controller.post(empinsertdata);
            var find = db.emps.FirstOrDefault(x => x.id == empinsertdata.id);
            db.SaveChanges();
            
            //Assert
            NotNull(res);
         
        }
        //-------------------------------------------------------- Update Data -------------------------------------------------------------
        [Fact]
        public void UpdateEmp()
        {
            //SetUp
            var insertemp = insertempdata();
            db.emps.Add(insertemp); 
            db.SaveChanges();

            var updateemployes = new Emp
            {
                id = 11,
                emp_name = "RAM",
                emp_post = "CEO",
                emp_sal = 100000,
            };

            //Exicute
            var res = Controller.Put(updateemployes.id, updateemployes);
            var emp_res = db.emps.FirstOrDefault();

            //Assert
            Equal("RAM", emp_res.emp_name);
            Equal(1,db.emps.Count());   
            NotNull(res);
        }

        // ------------------------------------------------------------ Delete Data ---------------------------------------------------------
        [Fact]
        public void empdelete()
        {
            //setup
            var deleteemp = insertempdata();

            //Exicute
            var res = Controller.Delete(deleteemp.id);
            var Findresult = db.emps.FirstOrDefault(x => x.id == deleteemp.id); 

            //Assert
            Null(Findresult);

        }

    }
}


