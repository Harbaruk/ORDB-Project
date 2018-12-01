using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starter.DAL.Entities;
using Starter.DAL.Infrastructure.ADORepository;
using Starter.DAL.Infrastructure.ADOUnitOfWork;

namespace Starter.Services.Test
{
    public interface ITestService
    {
        void TestAll();
        ManagerEntity GetManagerById(int id);
        List<ManagerEntity> GetAllManagers();
    }

    public class TestService : ITestService
    {
        private readonly IADOUnitOfWork _unitOfWork;

        public TestService(IADOUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ManagerEntity> GetAllManagers()
        {
            return _unitOfWork.Repository<ManagerEntity>().GetList(null).ToList();
        }

        public ManagerEntity GetManagerById(int id)
        {
            return _unitOfWork.Repository<ManagerEntity>().GetById(id);
        }

        public void TestAll()
        {
            _unitOfWork.Repository<TestEntity>().Update(new TestEntity { Id = 1, Text = "hello" });
            var result = _unitOfWork.Repository<DispatcherEntity>().GetById(2);
            var result1 = _unitOfWork.Repository<TestEntity>().GetList(null);
        }
    }
}