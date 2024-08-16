using UserService.Model;

namespace UserService.Repository
{
    /// <summary>
    /// This is user interface which contains all definations
    /// </summary>
    public interface Iuser
    {
        Task<List<UserDetails>> GetAll();
        Task<UserDetails> GetUserById(int id);
        Task<bool> AddUser(UserDetails userDetails);
        Task<bool> DeleteUser(int id);
    }
}
