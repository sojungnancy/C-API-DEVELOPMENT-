using A2TEMPLATE.Dtos;
using A2TEMPLATE.Models;
namespace A2TEMPLATE.Data
{
    public interface IA2Repo
    {

        public bool ValidLogin(string userName, string password);
        public bool ValidOrganizer(string Name, string password);
        public bool Register(User user);
        public bool PurchaseSign(string SignID);
        public string AddEvent(EventInput eventInput);
        public int EventCount();
        public Event GetEvent(int id);
    }   
}