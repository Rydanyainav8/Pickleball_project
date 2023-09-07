using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Pickleball_project.Models;

namespace Pickleball_project.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApiClientsController : Controller
    {
        private PickleballDBContext _context;

        public ApiClientsController(PickleballDBContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var clients = _context.Clients.Select(i => new {
                i.id,
                i.Firstname,
                i.Lastname,
                i.Gender,
                i.Birthdate,
                i.Email,
                i.Adress,
                i.City,
                i.Zip,
                i.Country,
                i.PhoneNumber,
                i.FirstNameEmergencyContact,
                i.LastNameEmergencyContact,
                i.EmergencyPhone,
                i.PlayedCategory,
                i.Group
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(clients, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Client();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Clients.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Clients.FirstOrDefaultAsync(item => item.id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Clients.FirstOrDefaultAsync(item => item.id == key);

            _context.Clients.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Client model, IDictionary values) {
            string ID = nameof(Client.id);
            string FIRSTNAME = nameof(Client.Firstname);
            string LASTNAME = nameof(Client.Lastname);
            string GENDER = nameof(Client.Gender);
            string BIRTHDATE = nameof(Client.Birthdate);
            string EMAIL = nameof(Client.Email);
            string ADRESS = nameof(Client.Adress);
            string CITY = nameof(Client.City);
            string ZIP = nameof(Client.Zip);
            string COUNTRY = nameof(Client.Country);
            string PHONE_NUMBER = nameof(Client.PhoneNumber);
            string FIRST_NAME_EMERGENCY_CONTACT = nameof(Client.FirstNameEmergencyContact);
            string LAST_NAME_EMERGENCY_CONTACT = nameof(Client.LastNameEmergencyContact);
            string EMERGENCY_PHONE = nameof(Client.EmergencyPhone);
            string PLAYED_CATEGORY = nameof(Client.PlayedCategory);
            string GROUP = nameof(Client.Group);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(FIRSTNAME)) {
                model.Firstname = Convert.ToString(values[FIRSTNAME]);
            }

            if(values.Contains(LASTNAME)) {
                model.Lastname = Convert.ToString(values[LASTNAME]);
            }

            if(values.Contains(GENDER)) {
                model.Gender = Convert.ToString(values[GENDER]);
            }

            if(values.Contains(BIRTHDATE)) {
                model.Birthdate = Convert.ToDateTime(values[BIRTHDATE]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(ADRESS)) {
                model.Adress = Convert.ToString(values[ADRESS]);
            }

            if(values.Contains(CITY)) {
                model.City = Convert.ToString(values[CITY]);
            }

            if(values.Contains(ZIP)) {
                model.Zip = Convert.ToString(values[ZIP]);
            }

            if(values.Contains(COUNTRY)) {
                model.Country = Convert.ToString(values[COUNTRY]);
            }

            if(values.Contains(PHONE_NUMBER)) {
                model.PhoneNumber = Convert.ToString(values[PHONE_NUMBER]);
            }

            if(values.Contains(FIRST_NAME_EMERGENCY_CONTACT)) {
                model.FirstNameEmergencyContact = Convert.ToString(values[FIRST_NAME_EMERGENCY_CONTACT]);
            }

            if(values.Contains(LAST_NAME_EMERGENCY_CONTACT)) {
                model.LastNameEmergencyContact = Convert.ToString(values[LAST_NAME_EMERGENCY_CONTACT]);
            }

            if(values.Contains(EMERGENCY_PHONE)) {
                model.EmergencyPhone = Convert.ToString(values[EMERGENCY_PHONE]);
            }

            if(values.Contains(PLAYED_CATEGORY)) {
                model.PlayedCategory = Convert.ToString(values[PLAYED_CATEGORY]);
            }

            if(values.Contains(GROUP)) {
                model.Group = Convert.ToString(values[GROUP]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}