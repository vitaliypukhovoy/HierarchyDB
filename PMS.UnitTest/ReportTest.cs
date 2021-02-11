using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PMS.Infrastructure.DataAccess.Export;
using PMS.Infrastructure.DataAccess.Model;
using PMS.Infrastructure.DataAccess.Repo;
using PMS.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UnitTest
{
    [TestFixture]
    public class ReportTest : ControllerBase
    {
        [Test]
        public async Task ReportControllerAsync()
        {

            var mockRepo = new Mock<IRepository<MemoryReportTable>>();
            var mockExcel = new Mock<IExportToExcel>();

           // mockRepo.Setup(repo => repo.GetAsync(null, null, CommandType.StoredProcedure))
           //                             .Throws<InvalidOperationException>();
            mockRepo.Setup(repo => repo.GetAsync(null, null, CommandType.StoredProcedure))
                    .Returns(Task.Factory.StartNew(() => GetTestMemoryReportTable()));

            mockExcel.Setup(ex => ex.Export(GetTestMemoryReportTable()))
                     .Returns(Task.FromResult(It.IsAny<FileContentResult>()));


            mockExcel.Setup(ex => ex.CalculateMode(GetTestMemoryReportTable()))
                      .Returns(It.IsAny<FileContentResult>());

            mockExcel.Setup(ex => ex.Export(GetTestMemoryReportTable()))
                    .Returns(Task.FromResult(FileContentResult()));


            var controller = new ReportController(mockRepo.Object, mockExcel.Object);

            ProjectReport query = new ProjectReport { startDate = "1 - 12 - 2000", finishDate = "12 - 12 - 2012" };
            var result = await controller.GetAsync(query);


            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<ActionResult>>(result);
            Assert.IsAssignableFrom<Task<ActionResult>>(result);
            Assert.IsInstanceOf<Task<FileContentResult>>(Task.FromResult(FileContentResult()), "Expected a FileResult");

        }

        public FileContentResult FileContentResult()
        {
            var buffer = new byte[] { 1, 2, 3, 4, 5 };

            return File(
                        new UTF8Encoding().GetBytes(buffer.ToString()),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
        }

        private IEnumerable<MemoryReportTable> GetTestMemoryReportTable()
        {
            var tables = new List<MemoryReportTable>()
                  {
                      new MemoryReportTable{ p_id=1,p_code= 123,p_startdate = DateTime.MinValue ,p_finishdate =  DateTime.Now},
                      new MemoryReportTable{ p_id=2,p_code= 423,p_startdate = DateTime.MinValue ,p_finishdate =  DateTime.Now},
                      new MemoryReportTable{ p_id=3,p_code= 154,p_startdate = DateTime.MinValue ,p_finishdate =  DateTime.Now},
                      new MemoryReportTable{ p_id=4,p_code= 327,p_startdate = DateTime.MinValue ,p_finishdate =  DateTime.Now},
                      new MemoryReportTable{ p_id=5,p_code= 753,p_startdate = DateTime.MinValue ,p_finishdate =  DateTime.Now},
                  };
            return tables;
        }
    }
}
