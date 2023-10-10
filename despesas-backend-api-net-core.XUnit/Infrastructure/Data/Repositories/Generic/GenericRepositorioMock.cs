using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Infrastructure.Data.Repositories.Generic
{
    public class GenericRepositorioMock<T> : IRepositorio<T> where T : BaseModel
    {
        private static RegisterContext _context;
        private static Mock<DbSet<T>> _dataSet;
        private static Mock<IRepositorio<T>> _mock;        
        private static GenericRepositorio<T> _genericRepositorioMock;
        
        private GenericRepositorioMock()
        {
        }

        public static GenericRepositorio<T> Build(RegisterContext registerContext, Mock<DbSet<T>> dataSet)
        {
            
            _context = registerContext;
            _dataSet = dataSet;
            
            _mock = new Mock<IRepositorio<T>>();
            _mock.Setup(repo => repo.Get(It.IsAny<int>())).Returns((int id) =>  {  return dataSet.Object.SingleOrDefault(item => item.Id == id); });
            _mock.Setup(repo => repo.GetAll()).Returns(() => dataSet.Object.ToList());
            _mock.Setup(repo => repo.Insert(It.IsAny<T>())).Returns((T item) => item);
            _mock.Setup(repo => repo.Update(It.IsAny<T>())).Returns((T updatedItem) => 
            {
                var existingItem = dataSet.Object.FirstOrDefault(item => item.Id == updatedItem.Id);
                if (existingItem != null)
                {
                    existingItem = updatedItem;

                }
                return updatedItem;
            });
            _mock.Setup(repo => repo.Delete(It.IsAny<BaseModel>())).Returns((int id) =>
            {
                var itemToRemove = _dataSet.Object.FirstOrDefault(item => item.Id == id);
                if (itemToRemove != null)
                {
                    _dataSet.Object.Remove(itemToRemove);
                    return true;
                }

                return false;
            });
            _genericRepositorioMock = new GenericRepositorio<T>(registerContext);
            return _genericRepositorioMock;
        }

        public T Get(int id)
        {
            return _mock.Object.Get(id);
        }

        public List<T> GetAll()
        {
            return _mock.Object.GetAll();
        }

        public T Insert(T item)
        {
            var result = _mock.Object.Insert(item);            
            _context.SaveChanges();
            return result;
        }


        public T Update(T item)
        {
            var result = _mock.Object.Update(item);
            _context.Update(result);
            _context.SaveChanges();

            return result;
        }

        public bool Delete(BaseModel item)
        {
            var result = _mock.Object.Delete(new BaseModel { Id = item.Id });
            _context.Remove(result);
            _context.SaveChanges();
            return result;
        }


        public bool Exists(int? id)
        {
            var entities = new List<T>(); 
            return entities.Any(e => e.Id == id);
        }

    }
}
