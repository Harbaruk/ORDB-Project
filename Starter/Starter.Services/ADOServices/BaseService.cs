using System;
using System.Collections.Generic;
using System.Text;
using Starter.DAL.Infrastructure.ADOUnitOfWork;

namespace Starter.Services.ADOServices
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly IADOUnitOfWork _unitOfWork;

        public BaseService(IADOUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id)
        {
            _unitOfWork.Repository<T>().DeleteById(id);
        }

        public T Get(int id)
        {
            return _unitOfWork.Repository<T>().GetById(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _unitOfWork.Repository<T>().GetAll();
        }

        public void Insert(T item)
        {
            _unitOfWork.Repository<T>().Insert(item);
        }

        public void Update(T item)
        {
            _unitOfWork.Repository<T>().Update(item);
        }
    }
}