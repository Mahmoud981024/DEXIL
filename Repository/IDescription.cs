using GraduationProject.Models;


namespace GraduationProject.Repository
{
    public interface IDescription
    {
        List<Description> Details(int id);
        void Add(Description description);
        void Delete(Description description);
        void Update(int id,Description description);
        Description GetID(int id);
    }
}
