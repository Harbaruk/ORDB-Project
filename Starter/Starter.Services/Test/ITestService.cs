using System;
using System.Collections.Generic;
using System.Text;
using Starter.DAL.Entities;
using Starter.DAL.Infrastructure.ADORepository;
using Starter.DAL.Infrastructure.ADOUnitOfWork;

namespace Starter.Services.Test
{
    public interface ITestService
    {
        void TestAll();
    }

    public class TestService : ITestService
    {
        private readonly IADOUnitOfWork _unitOfWork;

        public TestService(IADOUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void TestAll()
        {
            var result1 = _unitOfWork.Repository<TestEntity>().GetList(null);
        }
    }
}