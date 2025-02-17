using System.Globalization;
using A2TEMPLATE.Dtos;
using A2TEMPLATE.Models;
using Microsoft.EntityFrameworkCore;

namespace A2TEMPLATE.Data
{
    public class A2Repo : IA2Repo{

        public readonly A2DbContext _dbcontext;

        public A2Repo(A2DbContext context)
        {
            _dbcontext = context;
        }
        public bool ValidLogin(string userName, string password){
            User u = _dbcontext.Users.FirstOrDefault(e => e.UserName == userName && e.Password == password);
            return u != null;
        }

        public bool ValidOrganizer(string Name, string password){
            Organizer o = _dbcontext.Organizers.FirstOrDefault(e => e.Name == Name && e.Password == password);
            return o != null;
        }

        public bool Register(User user){

            User u = _dbcontext.Users.FirstOrDefault(e => e.UserName == user.UserName);
            if (u == null){
                _dbcontext.Add(user);
                _dbcontext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool PurchaseSign(string SignID){
            Sign sign = _dbcontext.Signs.FirstOrDefault(e => e.Id == SignID);
            if (sign != null){
                return true;
            }
            return false;
        }

        public string AddEvent(EventInput eventInput){
            string format = "yyyyMMddTHHmmssZ";

            string st = eventInput.Start;
            string ed = eventInput.End;

            bool valid_st = DateTime.TryParseExact(st, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out _);
            bool valid_ed = DateTime.TryParseExact(ed, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out _);

            if (!valid_st && !valid_ed) return "The format of Start and End should be yyyyMMddTHHmmssZ.";
            else if (!valid_st) return "The format of Start should be yyyyMMddTHHmmssZ.";
            else if (!valid_ed) return "The format of End should be yyyyMMddTHHmmssZ.";
            else 
            {
                _dbcontext.Add(new Event{
                    Start = eventInput.Start,
                    End = eventInput.End,
                    Summary = eventInput.Summary,
                    Description = eventInput.Description,
                    Location = eventInput.Location,
                });
                _dbcontext.SaveChanges();
                return "Sucess";
            }
        }

        public int EventCount(){
            return _dbcontext.Events.Count();
        }
        public Event GetEvent(int id){
            return _dbcontext.Events.FirstOrDefault(e => e.Id == id);
        }
    }

}